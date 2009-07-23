using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
namespace frontend_3_5
{
    public partial class frmSplash : Form
    {
        private bool aboutMode = false;
        
        public frmSplash()
        {
            InitializeComponent();
        }

        public frmSplash(bool aboutMode)
            : this()
        {
            this.aboutMode = aboutMode;
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            if (aboutMode)
            {
                this.Text = "Email Extended About";
                this.lblStatus.Hide();
            }
            else
            {
                this.Text = "EmEx Loading ...";
            }
        }

        private void frmSplash_Click(object sender, EventArgs e)
        {
            if (aboutMode)
                this.Close();
        }
    }
}
