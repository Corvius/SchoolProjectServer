using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SchoolProjectGui
{
    public partial class StylePopupForm : Form
    {
        public string StyleName { get; private set; }

        public StylePopupForm()
        {
            InitializeComponent();
            StyleName = "";
        }

        private void btNewStyleOk_Click(object sender, EventArgs e)
        {
            if (txtNewStyleName.Text == string.Empty)
                MessageBox.Show("Name cannot be empty!");
            else
            {
                StyleName = txtNewStyleName.Text;
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btNewStyleCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
