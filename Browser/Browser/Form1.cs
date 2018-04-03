using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }
        
        // LOAD VBF
        private void loadPicButton_Click(object sender, EventArgs e)
        {
            if (openVBFDialog.ShowDialog() == DialogResult.OK)
            {
                if (Program.LoadVBF(openVBFDialog.FileName))
                {
                    Log("VBF loaded.");

                    PopulateTreeView();
                    Log("File system initialized.");

                    prevBtn.Enabled = true;
                    collapseButton.Enabled = true;
                    injectButton.Enabled = true;
                    extractButton.Enabled = true;
                }
                else
                {
                    treeView1.Nodes.Clear();
                    listView1.Clear();
                    prevBtn.Enabled = false;
                    collapseButton.Enabled = false;
                    injectButton.Enabled = false;
                    extractButton.Enabled = false;
                    MessageBox.Show("Failed to open the file.");
                    Log("Failed to open the file.");
                }
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
            var ev = new TreeViewEventArgs(e.Node);
            treeView1_AfterSelect(sender, ev);
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
            TreeNode newSelected = e.Node;
            listView1.Items.Clear();
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
            //inject files TODO
        }

        private void extractButton_Click(object sender, EventArgs e)
        {
            if (extractLocationDialog.ShowDialog() == DialogResult.OK)
            {
                var path = extractLocationDialog.SelectedPath;
                var items = listView1.SelectedItems;
                var tags = new List<VUnit>();
                for(int i = 0; i < listView1.SelectedItems.Count; i++)
                {
                    tags.Add(listView1.SelectedItems[i].Tag as VUnit);
                }
                extract(tags, path, out int total, out int succeeded);
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
                TreeNodeMouseClickEventArgs args = new TreeNodeMouseClickEventArgs(currentSelection.Parent, MouseButtons.Left, 1, 0, 0);
                currentSelection = rootNode;
                treeView1.SelectedNode = currentSelection;
                treeView1_NodeMouseClick(sender, args);
            }
        }
    }
}
