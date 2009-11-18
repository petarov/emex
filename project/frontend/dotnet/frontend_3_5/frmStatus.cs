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
    public partial class frmStatus : Form
    {
        private string title = string.Empty;
        public frmStatus( string title )
        {
            this.title = title;
            InitializeComponent();
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {
            this.lblInfo.Text = title;
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = 100;
        }

        public void update(int value)
        {
            this.progressBar1.Increment(value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
