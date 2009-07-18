using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace frontend_3_5
{
    public partial class frmPassword : Form
    {
        private string pass = string.Empty;

        public string AccountPassword
        {
            get
            {
                return this.pass;
            }
        }

        public frmPassword()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.pass = txtPassword.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void frmPassword_Load(object sender, EventArgs e)
        {
            chkPass.Checked = false;

        }

        private void chkPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPass.Checked)
                txtPassword.PasswordChar = (char)0;
            else
                txtPassword.PasswordChar = '*';
        }
    }
}
