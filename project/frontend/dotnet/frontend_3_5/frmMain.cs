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
        private Result resContacts = null;
        private Result resMessages = null;
        private Result resAttachments = null;
        private ListViewItem livContactCurrent = null;

        private string Status
        {
            set
            {
                lblStatus.Text = value;
            }
        }

        public void refreshAttachments()
        {
            //--- Refresh Attachments
            try
            {
                Status = "Retrieving all attachments list ...";

                Status = "Done.";
            }
            catch (Exception ex)
            {
                Status = "Error while getting contacts!";
                new ErrorHandler(ex).Error();
            }
        }

        public void refreshMails()
        {
            //--- Refresh EMails
            try
            {
                Status = "Retrieving all messages  ...";
                resContacts = Bootstrap.Instance().Talker.ListMails();
                listView1.Items.Clear();
                foreach (Hashtable t in resContacts.return_)
                {
                    //ListViewItem liv = new ListViewItem(Convert.ToString(t["id"]), 0);
                    ListViewItem liv = new ListViewItem((string)t["subject"], 3);
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["ALAA"]) ));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["dateRecieved"]) ));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["priority"]) ));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["id"])));
                    this.listViewMessages.Items.Add(liv);
                }
                Status = "Done.";
            }
            catch (Exception ex)
            {
                Status = "Error while getting messages!";
                new ErrorHandler(ex).Error();
            }
        }

        public void RefreshContacts()
        {
            //--- Refresh Contacts
            try
            {
                Status = "Retrieving contacts list ...";
                resContacts = Bootstrap.Instance().Talker.ListContacts();
                listView1.Items.Clear();
                foreach (Hashtable t in resContacts.return_)
                {
                    ListViewItem liv = new ListViewItem((string)t["email"], 0);
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, "0"));
                    this.listView1.Items.Add(liv);
                }
                Status = "Done.";
            }
            catch (Exception ex)
            {
                Status = "Error while getting contacts!";
                new ErrorHandler(ex).Error();
            }
        }

        public void ShowEditUser(string email)
        {
            //--- Show User VCard dialog
            try
            {
                foreach (Hashtable t in resContacts.return_)
                {
                    if (email == (string)t["email"])
                    {
                        frmContactInfo frmContact = new frmContactInfo(t);
                        frmContact.ShowDialog();
                        this.RefreshContacts(); // NOT HERE!
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                new ErrorHandler(ex).Error();
            }
        }

        // -----------

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

                    // register user
                    Hashtable bizSettings = wizAccount.AccountInfo;
                    Result res = Bootstrap.Instance().Talker.RegisterUser(bizSettings);
                    ErrorHandler.checkBizResult(res);

                    // rebuild mailbox
                    res = Bootstrap.Instance().Talker.BuildMbox();
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

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            //TODO: confirm
            this.Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if ( tabControlMain.SelectedTab.Name == "tabPageContacts")
                this.RefreshContacts();
            else if (tabControlMain.SelectedTab.Name == "tabPageMails")
                this.refreshMails();
            else if (tabControlMain.SelectedTab.Name == "tabPageAttachments")
                this.refreshAttachments();
        }

        private void btnNewMail_Click(object sender, EventArgs e)
        {
            //--- New Mail
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //--- Search Mails
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem liv = listView1.GetItemAt(e.X, e.Y);
            ShowEditUser(liv.Text);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            //-- Show Context Menu on Contacts
            if (e.Button == MouseButtons.Right)
            {
                ListViewItem liv = listView1.GetItemAt(e.X, e.Y);
                livContactCurrent = liv;
                if (liv != null)
                    contextMenuContacts.Show(listView1, e.X, e.Y);
            }
        }

        private void toolStripMenuProfile_Click(object sender, EventArgs e)
        {
            //-- Context Menu (Profile)
            if (livContactCurrent!=null)
                ShowEditUser(livContactCurrent.Text);
        }

        private void toolStripMenuNewMail_Click(object sender, EventArgs e)
        {
            //-- Context Menu (New Mail)

        }

        private void toolStripMenuAttachments_Click(object sender, EventArgs e)
        {
            //-- Context Menu (View Attachments)

        }

        private void toolStripMenuMails_Click(object sender, EventArgs e)
        {
            //-- Context Menu (View Emails)

        }

        private void listViewMessages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //-- Open Mail
            ListViewItem liv = listViewMessages.GetItemAt(e.X, e.Y);
            MessageBox.Show(liv.SubItems[4].Text);
            
            //ShowEditMail(liv.Text);
        }


    }
}
