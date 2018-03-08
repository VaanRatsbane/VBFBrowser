using Browser.VBFTool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browser
{
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

            var fileList = reader.ReadFileList();
            for(int i = 0; i < fileList.Count(); i++)
            {
                folderCount += root.AddFile(fileList[i], i, reader.mOriginalSizes[i]);
            }

            Console.WriteLine($"Added {folderCount} folders and {fileCount} files.");
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
            path = path.Remove(1); //Assuming every path starts at /
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
        
    }

    class VFile : VUnit
    {

        public ulong originalSize;
        public int fileIndex;

        public VFile(string name, ulong originalSize, int fileIndex, VFolder parent = null) : base(name, parent)
        {
            this.originalSize = originalSize;
        }
    }

}
