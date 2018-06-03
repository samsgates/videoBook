using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace videobook
{
    public partial class oscreen : ComponentFactory.Krypton.Toolkit.KryptonForm

    {
        public oscreen()
        {
            InitializeComponent();
        }

        private void oscreen_Load(object sender, EventArgs e)
        {
            lbtitle.Text = Application.ProductName + " " + Application.ProductVersion.ToString();
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
