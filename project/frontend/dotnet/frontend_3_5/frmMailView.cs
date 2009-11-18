using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using frontend_3_5.BizTalk;
using frontend_3_5.Utils;
using frontend_3_5.Proc;

namespace frontend_3_5
{
    public partial class frmMailView : Form
    {
        private Hashtable hashMailInfo = null;
        public string from = string.Empty;
        public string to = string.Empty;

        public string Body
        {
            get
            {
                return this.txtBody.Text;
            }
        }

        public string To
        {
            get
            {
                return this.txtTo.Text.Trim();
            }
        }

        public string Cc
        {
            get
            {
                return this.txtCC.Text.Trim();
            }
        }

        public string Bcc
        {
            get
            {
                return this.txtBCC.Text.Trim();
            }
        }

        public string Subject
        {
            get
            {
                return this.txtSubject.Text;
            }
        }

        public frmMailView()
        {
            InitializeComponent();
        }

        public frmMailView(Hashtable t)
            : this()
        {
            this.hashMailInfo = t;
        }

        public frmMailView(string from, string to)
            : this()
        {
            this.from = from;
            this.to = to;
        }

        public frmMailView(string from)
            : this()
        {
            this.from = from;
        }

        private void frmMailView_Load(object sender, EventArgs e)
        {
            this.Text = "Mail View";
            if (hashMailInfo != null)
            {
                btnSend.Visible = btnCancel.Visible = false;
                toolStripButton1.Enabled = false;

                txtFrom.Text = Convert.ToString(hashMailInfo["FROM"]);
                txtTo.Text = Convert.ToString(hashMailInfo["TO"]);
                txtCC.Text = Convert.ToString(hashMailInfo["CC"]);
                txtBCC.Text = Convert.ToString(hashMailInfo["BCC"]);
                txtSubject.Text = Convert.ToString(hashMailInfo["SUBJECT"]);
                txtBody.Text = Convert.ToString(hashMailInfo["BODY"]);

                txtFrom.ReadOnly = txtTo.ReadOnly = txtCC.ReadOnly = txtBCC.ReadOnly = true;
                //txtSubject.Enabled = txtBody.Enabled = false;
            }
            if (this.from != string.Empty)
            {
                txtFrom.Text = this.from;
                txtFrom.ReadOnly = true;
            }
            if (this.to != string.Empty)
            {
                txtTo.Text = this.to;
            }
        }

        public bool validate()
        {
            if (txtTo.Text.Length < 1)
            {
                MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTo.Focus();
                return false;
            }
            if (txtSubject.Text.Length < 1)
            {
                MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSubject.Focus();
                return false;
            }
            if (txtBody.Text.Length < 1)
            {
                MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBody.Focus();
                return false;
            }
            return true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (validate())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
