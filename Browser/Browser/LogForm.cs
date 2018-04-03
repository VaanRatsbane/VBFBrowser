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
    public partial class LogForm : Form
    {
        public LogForm(Form parent)
        {
            InitializeComponent();
            this.Owner = parent;
            logBox.Lines = Program.consoleLog.ToArray();
            logBox.SelectionLength = 0;
        }
    }
}
