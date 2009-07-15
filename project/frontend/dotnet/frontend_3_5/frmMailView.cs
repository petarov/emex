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

        public frmMailView()
        {
            InitializeComponent();
        }

        public frmMailView(Hashtable t) 
        {
            this.hashMailInfo = t;
            InitializeComponent();
        }

        private void frmMailView_Load(object sender, EventArgs e)
        {
            this.Text = "Mail View";
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //TODO:
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            //TODO:
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
