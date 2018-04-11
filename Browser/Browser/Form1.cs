using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Browser
{
    public partial class Form1 : Form
    {

        TreeNode rootNode, currentSelection;

        public Form1()
        {
            InitializeComponent();
            if(!(Directory.Exists("ff12-vbf") && File.Exists("ff12-vbf\\ff12-vbf.exe") &&
                File.Exists("ff12-vbf\\libgcc_s_seh-1.dll") && File.Exists("ff12-vbf\\libstdc++-6.dll") &&
                File.Exists("ff12-vbf\\libwinpthread-1.dll") && File.Exists("ff12-vbf\\zlib1.dll")))
            {
                MessageBox.Show("ffgriever's tools are missing. Exiting...");
                Environment.Exit(1);
            }
        }
        
        // LOAD VBF
        private void loadPicButton_Click(object sender, EventArgs e)
        {
            if (openVBFDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                LoadVBF(openVBFDialog.FileName);
                Cursor.Current = Cursors.Default;
            }
        }

        private void LoadVBF(string filePath)
        {
            if (Program.LoadVBF(filePath))
            {
                Log("VBF loaded.");

                PopulateTreeView();
                Log("File system initialized.");

                prevBtn.Enabled = true;
                collapseButton.Enabled = true;
                injectButton.Enabled = true;
                extractButton.Enabled = true;
                treeView1_AfterSelect(null, new TreeViewEventArgs(currentSelection));
            }
            else
            {
                treeView1.Nodes.Clear();
                listView1.Items.Clear();
                prevBtn.Enabled = false;
                collapseButton.Enabled = false;
                injectButton.Enabled = false;
                extractButton.Enabled = false;
                MessageBox.Show("Failed to open the file.");
                Log("Failed to open the file.");
            }
        }

        private void Log(string message)
        {
            progressLabel.Text = message;
            Program.Log(message);
        }

        //Explorer

        private void PopulateTreeView()
        {
            rootNode = new TreeNode(Program.fileSystem.systemName);
            rootNode.Tag = Program.fileSystem.root;
            GetDirectories(Program.fileSystem.root.GetChildFolders(), rootNode);
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(rootNode);
            currentSelection = rootNode;
        }

        private void GetDirectories(VFolder[] subDirs, TreeNode nodeToAddTo)
        {
            TreeNode aNode;
            VFolder[] subSubDirs;
            foreach (VFolder subDir in subDirs)
            {
                aNode = new TreeNode(subDir.name, 0, 0);
                aNode.Tag = subDir;
                aNode.ImageKey = "folder";
                subSubDirs = subDir.GetChildFolders();
                if (subSubDirs.Length != 0)
                {
                    GetDirectories(subSubDirs, aNode);
                }
                nodeToAddTo.Nodes.Add(aNode);
            }
        }

        //Navigate using the tree
        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeMouseClick(sender, e);
        }
        private void NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e, bool wasUpdated = false)
        {
            var ev = new TreeViewEventArgs(e.Node);
            AfterSelect(sender, ev, wasUpdated);
            Program.fileSystem.currentFolder = e.Node.Tag as VFolder;
        }

        //Same as treeView1_NodeMouseClick but by double clicking the listview
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag is VFolder)
            {
                for(int i = 0; i < currentSelection.Nodes.Count; i++)
                {
                    var node = currentSelection.Nodes[i];
                    if(node.Tag == listView1.SelectedItems[0].Tag)
                    {
                        TreeNodeMouseClickEventArgs args = new TreeNodeMouseClickEventArgs(node, MouseButtons.Left, 1, 0, 0);
                        treeView1.SelectedNode = node;
                        treeView1_NodeMouseClick(sender, args);
                        break;
                    }
                }
            }
        }

        //Navigation through key presses
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToInt16(Keys.Enter))
            {
                if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag is VFolder)
                {
                    listView1_DoubleClick(null, null);
                }
                else if (treeView1.SelectedNode != null)
                    treeView1.SelectedNode.Expand();
            }
            else if(e.KeyChar == Convert.ToInt16(Keys.Back))
            {
                if(currentSelection.Parent != null || (currentSelection.Parent != null && listView1.SelectedItems.Count == 1))
                {
                    TreeNodeMouseClickEventArgs args = new TreeNodeMouseClickEventArgs(currentSelection.Parent, MouseButtons.Left, 1, 0, 0);
                    currentSelection = currentSelection.Parent;
                    treeView1.SelectedNode = currentSelection;
                    treeView1_NodeMouseClick(sender, args);
                }
            }
       }

        //update listview based on treeview selection
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            AfterSelect(sender, e);
        }
        private void AfterSelect(object sender, TreeViewEventArgs e, bool wasUpdated = false)
        {
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();

            if (newSelected == null) return;

            VFolder nodeDirInfo = (VFolder)newSelected.Tag;
            ListViewItem.ListViewSubItem[] subItems;
            ListViewItem item = null;

            foreach (VFolder dir in nodeDirInfo.GetChildFolders())
            {
                item = new ListViewItem(dir.name, 0);
                item.Tag = dir;
                listView1.Items.Add(item);
            }
            foreach (VFile file in nodeDirInfo.GetChildFiles())
            {
                if (wasUpdated)
                    file.UpdateSize();

                string size;
                if (file.originalSize < 1024)
                    size = $"{file.originalSize} bytes";
                else if (file.originalSize < 1024 * 1024)
                    size = $"{file.originalSize / 1024} KB";
                else if (file.originalSize < 1024 * 1024 * 1024)
                    size = $"{file.originalSize / 1024 / 1024} MB";
                else
                    size = $"{file.originalSize / 1024 / 1024 / 1024} GB";

                item = new ListViewItem(file.name, 1);
                subItems = new ListViewItem.ListViewSubItem[]
                          { new ListViewItem.ListViewSubItem(item, size)};

                item.SubItems.AddRange(subItems);
                item.Tag = file;
                listView1.Items.Add(item);
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            currentSelection = newSelected;
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode?.Parent != null)
            {
                TreeNodeMouseClickEventArgs args = new TreeNodeMouseClickEventArgs(treeView1.SelectedNode.Parent, MouseButtons.Left, 1, 0, 0);
                treeView1.SelectedNode = treeView1.SelectedNode.Parent;
                treeView1_NodeMouseClick(sender, args);
            }
        }

        //Open progress dialog
        private void progressLabel_Click(object sender, EventArgs e)
        {
            logButton_Click(sender, e);
        }

        private void logButton_Click(object sender, EventArgs e)
        {
            LogForm form = new LogForm(this);
            form.ShowDialog();
        }

        //File drag enter
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (currentSelection != null)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.All;
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        //File drag drop
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (currentSelection != null)
            {
                string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                string[] report;
                bool isValidInjection = Program.fileSystem.VerifyInjection(filePaths, out report);
                if (report != null && report.Length > 0)
                {
                    foreach (var rep in report)
                        Log(rep);
                }

                if (isValidInjection)
                {
                    Log("Injecting files...");
                    inject(filePaths);
                }
                else
                    MessageBox.Show("Files not injected. Check log.");
            }
        }

        private void inject(string[] filePaths)
        {
            Cursor.Current = Cursors.WaitCursor;
            string tempDirectory;
            do
            {
                tempDirectory = Path.Combine(Path.GetTempPath(), "VBFBROWSER" + Path.GetRandomFileName());
            } while (Directory.Exists(tempDirectory));
            Directory.CreateDirectory(tempDirectory);
            var prefixes = Program.fileSystem.currentFolder.GetPath().Substring(1);
            Directory.CreateDirectory(tempDirectory + prefixes);
            foreach (var path in filePaths)
            {
                if (Directory.Exists(path))
                    DirectoryCopy(path, tempDirectory + prefixes + "\\" + path.Split('\\').Last(), true);
                else if (File.Exists(path))
                    File.Copy(path, tempDirectory + prefixes + "\\" + Path.GetFileName(path));
            }

            var logList = new List<string>();

            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "ff12-vbf\\ff12-vbf.exe";
            process.StartInfo.Arguments = $"-r \"{tempDirectory}\" \"{Program.reader.mBigFilePath}\"";

            process.OutputDataReceived += (sender, args) => logList.Add(args.Data);
            process.ErrorDataReceived += (sender, args) => logList.Add(args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            Log("Initializing ff12-vbf.exe...");
            foreach(var msg in logList)
                Log(msg);
            Log("Finished injection.");

            Directory.Delete(tempDirectory, true);

            MessageBox.Show("Done.");

            Program.reader.UpdateAfterInjection();
            TreeNodeMouseClickEventArgs clickArgs = new TreeNodeMouseClickEventArgs(currentSelection, MouseButtons.Left, 1, 0, 0);
            treeView1.SelectedNode = currentSelection;
            NodeMouseClick(null, clickArgs, true);

            Cursor.Current = Cursors.Default;
        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            if (extractLocationDialog.ShowDialog() == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor;
                var path = extractLocationDialog.SelectedPath;
                var items = listView1.SelectedItems;
                var tags = new List<VUnit>();
                for(int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    tags.Add(listView1.SelectedItems[i].Tag as VUnit);
                }
                extract(tags, path, out int total, out int succeeded);
                Cursor.Current = Cursors.Default;
                MessageBox.Show($"Extracted {succeeded} out of the {total} files.");
            }
        }

        private void extract(List<VUnit> units, string path, out int total, out int succeeded)
        {
            total = 0;
            succeeded = 0;
            foreach(var unit in units)
            {
                if(unit is VFile)
                {
                    var virtualPath = unit.GetPath().TrimStart('/');
                    var outputPath = path + "\\" + unit.name;
                    if (Program.reader.ExtractFileContents(virtualPath, outputPath))
                        succeeded++;
                    else
                        Program.Log($"Failed to extract: {virtualPath}");
                    total++;
                }
                else if(unit is VFolder)
                {
                    var dir = path + "\\" + unit.name;
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    extract((unit as VFolder).GetChildren(), dir, out int t, out int s);
                    total += t;
                    succeeded += s;
                }
            }
        }

        private void exitPicButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void collapseButton_Click(object sender, EventArgs e)
        {
            if (currentSelection != null)
            {
                treeView1.CollapseAll();
                currentSelection = rootNode;
                treeView1.SelectedNode = currentSelection;
                TreeNodeMouseClickEventArgs args = new TreeNodeMouseClickEventArgs(currentSelection, MouseButtons.Left, 1, 0, 0);
                treeView1_NodeMouseClick(sender, args);
            }
        }

        private void injectButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you injecting a whole folder?", "File Injection", MessageBoxButtons.YesNo);
            var filePaths = new List<string>();
            if (dialogResult == DialogResult.Yes)
            {
                if (injectFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    var path = injectFolderDialog.SelectedPath;
                    filePaths.AddRange(Directory.GetDirectories(path));
                    filePaths.AddRange(Directory.GetFiles(path));
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                if(injectFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePaths.AddRange(injectFileDialog.FileNames);
                }
            }
            if(filePaths.Count > 0)
                inject(filePaths.ToArray());
        }

        private void aboutLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var about = new AboutForm(this);
            about.ShowDialog();
        }

        private void howToUseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("First, load the VBF file you wish to browse.\n" +
                "You can navigate the structure using the tree view on the left, or the list on the right.\n" +
                "You can extract folders/files by selecting them in the list on the right and then choosing \"Extract\"." +
                " You can select more than one item by holding Ctrl.\n" +
                "To inject a folder/file, browse to where you want to inject it, select \"Inject\" and" +
                " choose wether you are injecting files or a whole folder. You can also drag what you wish to inject to the list view on the right.");
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
