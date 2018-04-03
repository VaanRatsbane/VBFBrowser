using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Browser
{
    public partial class AboutForm : Form
    {
        public AboutForm(Form parent)
        {
            InitializeComponent();
            Owner = parent;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void topherLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/topher-au");
        }

        private void ffgrieverLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ffgriever");
        }

        private void vaanLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://steamcommunity.com/profiles/76561198009963015");
        }
    }
}
