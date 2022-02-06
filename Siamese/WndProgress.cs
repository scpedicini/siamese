using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Siamese
{
    public partial class WndProgress : Form
    {

        public string Title => this.Text;

        public void CloseWindow()
        {
            this.DialogResult = DialogResult.OK;
            
        }

        public WndProgress(string title)
        {
            InitializeComponent();
            this.Text = title;
        }
    }
}
