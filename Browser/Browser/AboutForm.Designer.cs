namespace Browser
{
    partial class AboutForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.topherLink = new System.Windows.Forms.LinkLabel();
            this.ffgrieverLink = new System.Windows.Forms.LinkLabel();
            this.vaanLink = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "VBF interactivity technologies by";
            // 
            // topherLink
            // 
            this.topherLink.AutoSize = true;
            this.topherLink.Location = new System.Drawing.Point(169, 24);
            this.topherLink.Name = "topherLink";
            this.topherLink.Size = new System.Drawing.Size(41, 13);
            this.topherLink.TabIndex = 1;
            this.topherLink.TabStop = true;
            this.topherLink.Text = "Topher";
            this.topherLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.topherLink_LinkClicked);
            // 
            // ffgrieverLink
            // 
            this.ffgrieverLink.AutoSize = true;
            this.ffgrieverLink.Location = new System.Drawing.Point(231, 24);
            this.ffgrieverLink.Name = "ffgrieverLink";
            this.ffgrieverLink.Size = new System.Drawing.Size(45, 13);
            this.ffgrieverLink.TabIndex = 2;
            this.ffgrieverLink.TabStop = true;
            this.ffgrieverLink.Text = "ffgriever";
            this.ffgrieverLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ffgrieverLink_LinkClicked);
            // 
            // vaanLink
            // 
            this.vaanLink.AutoSize = true;
            this.vaanLink.Location = new System.Drawing.Point(96, 47);
            this.vaanLink.Name = "vaanLink";
            this.vaanLink.Size = new System.Drawing.Size(32, 13);
            this.vaanLink.TabIndex = 3;
            this.vaanLink.TabStop = true;
            this.vaanLink.Text = "Vaan";
            this.vaanLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.vaanLink_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "and";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "GUI wrapping by";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Last version as of 2018/04/05";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(115, 89);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 7;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // AboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 124);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.vaanLink);
            this.Controls.Add(this.ffgrieverLink);
            this.Controls.Add(this.topherLink);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AboutForm";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel topherLink;
        private System.Windows.Forms.LinkLabel ffgrieverLink;
        private System.Windows.Forms.LinkLabel vaanLink;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button closeButton;
    }
}