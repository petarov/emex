using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using log4net;
using biztalk;
using frontend_3_5.BizTalk;
using frontend_3_5.Proc;
using frontend_3_5.Utils;

namespace frontend_3_5
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // startup

            try
            {
                Bootstrap.Instance().configure();

                // configure backend & frontend

                if ( ! Bootstrap.Instance().Settings.IsConfigured )
                {
                    frmWizGeneral wizGeneral = new frmWizGeneral();
                    if (DialogResult.Cancel == wizGeneral.ShowDialog())
                        throw new Exception("EmEx cannot start without configuration!");
                }

                // create/configure account (if not existing)

                if ( ! Bootstrap.Instance().Settings.IsAccountConfigured )
                {
                    frmWizAccount wizAccount = new frmWizAccount();
                    if (DialogResult.Cancel == wizAccount.ShowDialog(this))
                        throw new Exception("EmEx cannot start without configuring an account!");

                    Bootstrap.Instance().Settings.Reload();
                    Bootstrap.Instance().start();

                    Hashtable bizSettings = wizAccount.AccountInfo;
                    Result res = Bootstrap.Instance().Talker.RegisterUser(bizSettings);
                    ErrorHandler.checkBizResult(res);
                }
                else
                {
                    Bootstrap.Instance().start();
                }
                
            }
            catch (Exception ex)
            {
                new ErrorHandler(ex).Error();
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmWizAccount wiz1 = new frmWizAccount();
            wiz1.ShowDialog(this);
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Result res = Bootstrap.Instance().Talker.GetContacts();
            foreach (Hashtable t in res.return_)
            {
                this.listView1.Items.Add(new ListViewItem( (string)t["email"] ));
            }
        }
    }
}
