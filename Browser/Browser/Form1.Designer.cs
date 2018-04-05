namespace Browser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.masterContainer = new System.Windows.Forms.SplitContainer();
            this.prevBtn = new System.Windows.Forms.Button();
            this.exitPicButton = new System.Windows.Forms.PictureBox();
            this.loadPicButton = new System.Windows.Forms.PictureBox();
            this.secondContainer = new System.Windows.Forms.SplitContainer();
            this.browserContainer = new System.Windows.Forms.SplitContainer();
            this.collapseButton = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.injectButton = new System.Windows.Forms.Button();
            this.extractButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.nameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.origSizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.howToUseLink = new System.Windows.Forms.LinkLabel();
            this.aboutLinkLabel = new System.Windows.Forms.LinkLabel();
            this.logButton = new System.Windows.Forms.Button();
            this.progressLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openVBFDialog = new System.Windows.Forms.OpenFileDialog();
            this.injectFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.injectFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.extractLocationDialog = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.masterContainer)).BeginInit();
            this.masterContainer.Panel1.SuspendLayout();
            this.masterContainer.Panel2.SuspendLayout();
            this.masterContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.exitPicButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadPicButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.secondContainer)).BeginInit();
            this.secondContainer.Panel1.SuspendLayout();
            this.secondContainer.Panel2.SuspendLayout();
            this.secondContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.browserContainer)).BeginInit();
            this.browserContainer.Panel1.SuspendLayout();
            this.browserContainer.Panel2.SuspendLayout();
            this.browserContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "file.png");
            // 
            // masterContainer
            // 
            this.masterContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterContainer.Location = new System.Drawing.Point(0, 0);
            this.masterContainer.Name = "masterContainer";
            this.masterContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // masterContainer.Panel1
            // 
            this.masterContainer.Panel1.Controls.Add(this.prevBtn);
            this.masterContainer.Panel1.Controls.Add(this.exitPicButton);
            this.masterContainer.Panel1.Controls.Add(this.loadPicButton);
            // 
            // masterContainer.Panel2
            // 
            this.masterContainer.Panel2.Controls.Add(this.secondContainer);
            this.masterContainer.Size = new System.Drawing.Size(784, 561);
            this.masterContainer.SplitterDistance = 46;
            this.masterContainer.TabIndex = 0;
            // 
            // prevBtn
            // 
            this.prevBtn.Enabled = false;
            this.prevBtn.Location = new System.Drawing.Point(395, 16);
            this.prevBtn.Name = "prevBtn";
            this.prevBtn.Size = new System.Drawing.Size(75, 23);
            this.prevBtn.TabIndex = 2;
            this.prevBtn.Text = "Previous";
            this.prevBtn.UseVisualStyleBackColor = true;
            this.prevBtn.Click += new System.EventHandler(this.prevBtn_Click);
            // 
            // exitPicButton
            // 
            this.exitPicButton.Image = global::Browser.Properties.Resources.exitButton;
            this.exitPicButton.Location = new System.Drawing.Point(131, 3);
            this.exitPicButton.Name = "exitPicButton";
            this.exitPicButton.Size = new System.Drawing.Size(102, 37);
            this.exitPicButton.TabIndex = 1;
            this.exitPicButton.TabStop = false;
            this.exitPicButton.Click += new System.EventHandler(this.exitPicButton_Click);
            // 
            // loadPicButton
            // 
            this.loadPicButton.Image = global::Browser.Properties.Resources.openButton;
            this.loadPicButton.Location = new System.Drawing.Point(12, 3);
            this.loadPicButton.Name = "loadPicButton";
            this.loadPicButton.Size = new System.Drawing.Size(102, 37);
            this.loadPicButton.TabIndex = 0;
            this.loadPicButton.TabStop = false;
            this.loadPicButton.Click += new System.EventHandler(this.loadPicButton_Click);
            // 
            // secondContainer
            // 
            this.secondContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.secondContainer.Location = new System.Drawing.Point(0, 0);
            this.secondContainer.Name = "secondContainer";
            this.secondContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // secondContainer.Panel1
            // 
            this.secondContainer.Panel1.Controls.Add(this.browserContainer);
            // 
            // secondContainer.Panel2
            // 
            this.secondContainer.Panel2.Controls.Add(this.howToUseLink);
            this.secondContainer.Panel2.Controls.Add(this.aboutLinkLabel);
            this.secondContainer.Panel2.Controls.Add(this.logButton);
            this.secondContainer.Panel2.Controls.Add(this.progressLabel);
            this.secondContainer.Panel2.Controls.Add(this.label1);
            this.secondContainer.Size = new System.Drawing.Size(784, 511);
            this.secondContainer.SplitterDistance = 478;
            this.secondContainer.TabIndex = 0;
            // 
            // browserContainer
            // 
            this.browserContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.browserContainer.Location = new System.Drawing.Point(0, 0);
            this.browserContainer.Name = "browserContainer";
            // 
            // browserContainer.Panel1
            // 
            this.browserContainer.Panel1.Controls.Add(this.collapseButton);
            this.browserContainer.Panel1.Controls.Add(this.treeView1);
            this.browserContainer.Panel1.Controls.Add(this.injectButton);
            // 
            // browserContainer.Panel2
            // 
            this.browserContainer.Panel2.Controls.Add(this.extractButton);
            this.browserContainer.Panel2.Controls.Add(this.listView1);
            this.browserContainer.Size = new System.Drawing.Size(784, 478);
            this.browserContainer.SplitterDistance = 391;
            this.browserContainer.TabIndex = 1;
            // 
            // collapseButton
            // 
            this.collapseButton.Enabled = false;
            this.collapseButton.Location = new System.Drawing.Point(313, 3);
            this.collapseButton.Name = "collapseButton";
            this.collapseButton.Size = new System.Drawing.Size(75, 23);
            this.collapseButton.TabIndex = 1;
            this.collapseButton.Text = "Collapse All";
            this.collapseButton.UseVisualStyleBackColor = true;
            this.collapseButton.Click += new System.EventHandler(this.collapseButton_Click);
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(391, 450);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            // 
            // injectButton
            // 
            this.injectButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.injectButton.Enabled = false;
            this.injectButton.Location = new System.Drawing.Point(0, 450);
            this.injectButton.Name = "injectButton";
            this.injectButton.Size = new System.Drawing.Size(391, 28);
            this.injectButton.TabIndex = 0;
            this.injectButton.Text = "Inject";
            this.injectButton.UseVisualStyleBackColor = true;
            this.injectButton.Click += new System.EventHandler(this.injectButton_Click);
            // 
            // extractButton
            // 
            this.extractButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.extractButton.Enabled = false;
            this.extractButton.Location = new System.Drawing.Point(0, 450);
            this.extractButton.Name = "extractButton";
            this.extractButton.Size = new System.Drawing.Size(389, 28);
            this.extractButton.TabIndex = 1;
            this.extractButton.Text = "Extract";
            this.extractButton.UseVisualStyleBackColor = true;
            this.extractButton.Click += new System.EventHandler(this.extractButton_Click);
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader,
            this.origSizeHeader});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(389, 450);
            this.listView1.SmallImageList = this.imageList1;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 226;
            // 
            // origSizeHeader
            // 
            this.origSizeHeader.Text = "Original Size";
            this.origSizeHeader.Width = 158;
            // 
            // howToUseLink
            // 
            this.howToUseLink.ActiveLinkColor = System.Drawing.Color.Blue;
            this.howToUseLink.AutoSize = true;
            this.howToUseLink.Location = new System.Drawing.Point(128, 7);
            this.howToUseLink.Name = "howToUseLink";
            this.howToUseLink.Size = new System.Drawing.Size(67, 13);
            this.howToUseLink.TabIndex = 4;
            this.howToUseLink.TabStop = true;
            this.howToUseLink.Text = "How To Use";
            this.howToUseLink.VisitedLinkColor = System.Drawing.Color.Blue;
            this.howToUseLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.howToUseLink_LinkClicked);
            // 
            // aboutLinkLabel
            // 
            this.aboutLinkLabel.ActiveLinkColor = System.Drawing.Color.Blue;
            this.aboutLinkLabel.AutoSize = true;
            this.aboutLinkLabel.Location = new System.Drawing.Point(74, 7);
            this.aboutLinkLabel.Name = "aboutLinkLabel";
            this.aboutLinkLabel.Size = new System.Drawing.Size(35, 13);
            this.aboutLinkLabel.TabIndex = 3;
            this.aboutLinkLabel.TabStop = true;
            this.aboutLinkLabel.Text = "About";
            this.aboutLinkLabel.VisitedLinkColor = System.Drawing.Color.Blue;
            this.aboutLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.aboutLinkLabel_LinkClicked);
            // 
            // logButton
            // 
            this.logButton.Location = new System.Drawing.Point(743, 2);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(38, 23);
            this.logButton.TabIndex = 2;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // progressLabel
            // 
            this.progressLabel.Location = new System.Drawing.Point(392, 4);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(368, 16);
            this.progressLabel.TabIndex = 1;
            this.progressLabel.Text = "...";
            this.progressLabel.Click += new System.EventHandler(this.progressLabel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "VBFBrowser";
            // 
            // openVBFDialog
            // 
            this.openVBFDialog.FileName = "file.vbf";
            // 
            // injectFileDialog
            // 
            this.injectFileDialog.FileName = "openFileDialog1";
            // 
            // extractLocationDialog
            // 
            this.extractLocationDialog.Description = "Select extract location:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.masterContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "VBF Browser";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.masterContainer.Panel1.ResumeLayout(false);
            this.masterContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.masterContainer)).EndInit();
            this.masterContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.exitPicButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.loadPicButton)).EndInit();
            this.secondContainer.Panel1.ResumeLayout(false);
            this.secondContainer.Panel2.ResumeLayout(false);
            this.secondContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.secondContainer)).EndInit();
            this.secondContainer.ResumeLayout(false);
            this.browserContainer.Panel1.ResumeLayout(false);
            this.browserContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.browserContainer)).EndInit();
            this.browserContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer masterContainer;
        private System.Windows.Forms.SplitContainer secondContainer;
        private System.Windows.Forms.SplitContainer browserContainer;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.ColumnHeader origSizeHeader;
        private System.Windows.Forms.PictureBox exitPicButton;
        private System.Windows.Forms.PictureBox loadPicButton;
        private System.Windows.Forms.Button injectButton;
        private System.Windows.Forms.Button extractButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openVBFDialog;
        private System.Windows.Forms.OpenFileDialog injectFileDialog;
        private System.Windows.Forms.FolderBrowserDialog injectFolderDialog;
        private System.Windows.Forms.FolderBrowserDialog extractLocationDialog;
        private System.Windows.Forms.Button prevBtn;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Button collapseButton;
        private System.Windows.Forms.LinkLabel aboutLinkLabel;
        private System.Windows.Forms.LinkLabel howToUseLink;
    }
}

