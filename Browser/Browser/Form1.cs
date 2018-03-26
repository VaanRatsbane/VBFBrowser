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
        public Form1()
        {
            InitializeComponent();
        }
        
        // LOAD VBF
        private void loadPicButton_Click(object sender, EventArgs e)
        {
            if (openVBFDialog.ShowDialog() == DialogResult.OK)
            {
                Program.LoadVBF(openVBFDialog.FileName);
                Log("VBF loaded.");
                PopulateTreeView();
                Log("File system initialized.");
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
            TreeNode rootNode;
            rootNode = new TreeNode(Program.fileSystem.systemName);
            rootNode.Tag = Program.fileSystem.root;
            GetDirectories(Program.fileSystem.root.GetChildFolders(), rootNode);
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(rootNode);
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

            Program.fileSystem.currentFolder = e.Node.Tag as VFolder;

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        //Same as treeView1_NodeMouseClick but by double clicking the listview
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if(listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag is VFolder)
            {
                for(int i = 0; i < treeView1.SelectedNode.Nodes.Count; i++)
                {
                    var n = treeView1.SelectedNode.Nodes[i];
                    if(n.Tag == listView1.SelectedItems[0].Tag)
                    {
                        TreeNodeMouseClickEventArgs args = new TreeNodeMouseClickEventArgs(n, MouseButtons.Left, 1, 0, 0);
                        treeView1.SelectedNode = n;
                        treeView1_NodeMouseClick(sender, args);
                    }
                }
            }
        }

        //Navigation through key presses
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToInt16(Keys.Enter))
            {
                if(listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Tag is VFolder)
                {
                    listView1_DoubleClick(null, null);
                }
            }
            else if(e.KeyChar == Convert.ToInt16(Keys.Return))
            {
                if(treeView1.SelectedNode.Parent != null)
                {
                    TreeNodeMouseClickEventArgs args = new TreeNodeMouseClickEventArgs(treeView1.SelectedNode.Parent, MouseButtons.Left, 1, 0, 0);
                    treeView1.SelectedNode = treeView1.SelectedNode.Parent;
                    treeView1_NodeMouseClick(sender, args);
                }
            }
        }

        private void prevBtn_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Parent != null)
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
            LogForm form = new LogForm();
        }

        //File drag enter
        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
        }

        //File drag drop
        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);

            string[] report;
            bool isValidInjection = Program.fileSystem.VerifyInjection(filePaths, out report);
            if (report != null)
            {
                Log("Some files do not have a match within the vbf:");
                foreach (var rep in report)
                    Log(rep);
            }

            if (isValidInjection)
                ;//inject files TODO
            else
                MessageBox.Show("Files not injected. Check log.");
        }
    }
}
