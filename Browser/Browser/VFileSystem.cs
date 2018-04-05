using Browser.VBFTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser
{
    /// <summary>
    /// Virtual file system. Represents the VBF file structure for GUI representation.
    /// </summary>
    class VFileSystem
    {

        public string systemName;
        public VFolder root, currentFolder;
        public ulong folderCount, fileCount;

        public VFileSystem(string systemName, VirtuosBigFileReader reader)
        {
            this.systemName = systemName;
            fileCount = reader.mNumFiles;
            folderCount = 0;
            root = new VFolder("");
            currentFolder = root;

            var fileList = reader.ReadFileList();
            for(int i = 0; i < fileList.Count(); i++)
            {
                folderCount += root.AddFile(fileList[i], i, reader.mOriginalSizes[i]);
            }

            Console.WriteLine($"Added {folderCount} folders and {fileCount} files.");
        }

        internal bool VerifyInjection(string[] filePaths, out string[] report)
        {
            //first get prefix path (all path until filename, including \)
            var prefix = filePaths[0].Substring(0, filePaths[0].LastIndexOf('\\') + 1);
            var currentPath = currentFolder.GetPath();

            //parse dir names into multiple child filenames
            var files = parseFilePaths(filePaths);
            var reports = new List<string>();

            if (files.Count == 0) //only folders
            {
                reports.Add("No files found in the injection folder.");
                report = reports.ToArray();
                return false;
            }


            //then remove prefix path from all file paths, and prefix them with currentFolder instead
            for (int i = 0; i < files.Count; i++)
            {
                var virtualPath = currentPath.TrimStart('/') + "/" + files[i].Replace(prefix, "");
                //verify their existence
                if (!root.HasFile(virtualPath))
                {
                    if (reports.Count == 0)
                        reports.Add("Some files do not have a match within the vbf:");
                    reports.Add(files[i]);
                }
            }
            report = reports.ToArray();
            return reports.Count == 0;

        }

        private List<string> parseFilePaths(string[] filePaths)
        {
            var files = new List<string>();
            foreach (var p in filePaths)
            {
                if (!Directory.Exists(p))
                    files.Add(p);
                else
                {
                    files.AddRange(parseFilePaths(Directory.EnumerateFiles(p,"*.*",SearchOption.AllDirectories).ToArray()).ToArray());
                }
            }
            return files;
        }

    }

    class VUnit
    {
        public string name;
        public VFolder parent;

        public VUnit(string name, VFolder parent)
        {
            this.name = name;
            this.parent = parent;
        }

        public string GetPath()
        {
            return (parent == null ? "" : parent.GetPath()) + "/" + $"{name}";
        }

    }

    class VFolder : VUnit
    {

        Dictionary<string, VUnit> children;

        public VFolder(string name, VFolder parent = null) : base(name, parent)
        {
            children = new Dictionary<string, VUnit>();
        }

        public ulong AddFile(string path, int fileIndex, ulong originalSize)
        {
            if (path[0] == '/')
                path = path.TrimStart('/');
            var splices = path.Split('/');
            if(splices.Length == 1) //it's the file!
            {
                AddChild(new VFile(splices[0],originalSize,fileIndex,this));
                return 0;
            }
            else //it's a folder!
            {
                string restOfPath = "";
                for(int i = 1; i < splices.Length; i++)
                {
                    restOfPath += '/' + splices[i];
                }

                if(HasFolder(splices[0]))
                {
                    return (GetChild(splices[0]) as VFolder).AddFile(restOfPath, fileIndex, originalSize);
                }
                else
                {
                    VFolder folder = new VFolder(splices[0], this);
                    AddChild(folder);
                    return folder.AddFile(restOfPath, fileIndex, originalSize) + 1;
                }
            }
        }

        public void AddChild(VUnit child)
        {
            children.Add(child.name, child);
        }

        public VUnit GetChild(string childName)
        {
            return children.ContainsKey(childName) ? children[childName] : null;
        }

        public List<VUnit> GetChildren()
        {
            return new List<VUnit>(children.Values);
        }

        public VFolder[] GetChildFolders()
        {
            List<VFolder> folders = new List<VFolder>();
            foreach (var child in children)
                if (child.Value is VFolder)
                    folders.Add(child.Value as VFolder);
            return folders.ToArray();
        }

        public VFile[] GetChildFiles()
        {
            List<VFile> folders = new List<VFile>();
            foreach (var child in children)
                if (child.Value is VFile)
                    folders.Add(child.Value as VFile);
            return folders.ToArray();
        }

        public bool HasFolder(string folderName)
        {
            return ((children.ContainsKey(folderName)) && (children[folderName] is VFolder));
        }

        public bool HasFile(string path)
        {
            if(path.StartsWith("\\"))
                path = path.Substring(1); //remove the backslash
            var fragments = path.Split('/'); //gets each element
            if (fragments.Length > 1) //if there's more than 1 element
            {
                if (HasFolder(fragments[0])) //checks if first folder exists
                    return (GetChild(fragments[0]) as VFolder).HasFile(String.Join("/", fragments, 1, fragments.Length - 1)); //skip the first element and recurse in that folder
                else
                    return false; //folder doesn't exist, so false
            }
            //if only one fragment, return true if contain file
            else return children.ContainsKey(fragments[0]) && children[fragments[0]] is VFile;
                
        }
        
    }

    class VFile : VUnit
    {

        public ulong originalSize;
        public int fileIndex;

        public VFile(string name, ulong originalSize, int fileIndex, VFolder parent = null) : base(name, parent)
        {
            this.originalSize = originalSize;
            this.fileIndex = fileIndex;
        }

        public void UpdateSize()
        {
            originalSize = Program.reader.mOriginalSizes[fileIndex];
        }
    }

}
