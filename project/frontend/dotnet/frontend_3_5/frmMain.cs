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
        #region Variables

        private Result resContacts = null;
        private Result resMessages = null;
        private Result resAttachments = null;
        private ListViewItem livContactCurrent = null;
        public string MailBoxPassword = "123";

        #endregion
        #region Methods

        private string Status
        {
            set
            {
                lblStatus.Text = value;
            }
        }

        private void Tag_Click(object sender, EventArgs e)
        {
            //-- Tag was clicked (TagsView)
            Label lbl = (Label)sender;
            refreshMailsForTag( Convert.ToString( lbl.Tag ));
        }

        public void drawTags(Result resultTags)
        {
            if (resultTags.return_ == null)
                return;

            int x = 5;
            int y = 5;
            double max = 100;

            panelTags.Controls.Clear(); // remove old labels !
            foreach (Hashtable t in resultTags.return_)
            {
                int value = Convert.ToInt32( t["depth"] );
                double weightPercent = ((double)value / max) * 100.0;

                // setup view
                Label lbl = new Label();
                lbl.Parent = panelTags;
                lbl.Text = Convert.ToString(t["data"]);
                lbl.AutoSize = true;
                lbl.Tag = t["id"]; // remember ID !
                lbl.Cursor = Cursors.Hand;
                if (weightPercent >= 92)
                {
                    //heaviest
                    lbl.Font = CorporateIdentity.fntTag5;
                    lbl.ForeColor = CorporateIdentity.color5;
                }
                else if (weightPercent >= 70)
                {
                    lbl.Font = CorporateIdentity.fntTag4;
                    lbl.ForeColor = CorporateIdentity.color4;
                }
                else if (weightPercent >= 40)
                {
                    lbl.Font = CorporateIdentity.fntTag3;
                    lbl.ForeColor = CorporateIdentity.color3;
                }
                else if (weightPercent >= 20)
                {
                    lbl.Font = CorporateIdentity.fntTag2;
                    lbl.ForeColor = CorporateIdentity.color2;
                }
                else if (weightPercent >= 3)
                {
                    //weakest
                    lbl.Font = CorporateIdentity.fntTag1;
                    lbl.ForeColor = CorporateIdentity.color1;
                }
                else
                {
                    // use this to filter out all low hitters
                    lbl.Font = CorporateIdentity.fntTag0;
                    lbl.ForeColor = CorporateIdentity.color0;
                }

                // setup position

                if (x + lbl.Size.Width >= panelTags.Width - 4)
                {
                    x = 5;
                    y += 28;
                }

                lbl.Location = new Point(x, y);

                x += lbl.Size.Width + 2;

                // setup acction
                lbl.Click += new System.EventHandler(this.Tag_Click);

                // place limits
                if (y + 50 > panelTags.Height)
                    break;

            }
        }

        public void refreshTags(string query)
        {
            //--- Refresh Attachments
            try
            {
                Status = "Retrieving tags list ...";

                Result resTags;
                if (query == null || query.Length < 1)
                {
                    resTags = Bootstrap.Instance().Talker.ListTags();
                }
                else
                {
                    resTags = Bootstrap.Instance().Talker.SearchEMail(query);
                }
                
                ErrorHandler.checkBizResult(resTags);
                drawTags(resTags);
                Status = "Done.";
            }
            catch (Exception ex)
            {
                Status = "Error while getting tags!";
                new ErrorHandler(ex).Error();
            }
        }

        public void refreshAttachments()
        {
            //--- Refresh Attachments
            try
            {
                Status = "Retrieving all attachments list ...";
                resAttachments = Bootstrap.Instance().Talker.ListAttacments();
                ErrorHandler.checkBizResult(resAttachments);

                listViewAttachments.Items.Clear();
                foreach (Hashtable t in resAttachments.return_)
                {
                    ListViewItem liv = new ListViewItem(Convert.ToString(t["name"]), 6);
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["type"])));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["email"])));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["id"])));
                    this.listViewAttachments.Items.Add(liv);
                }
                Status = "Done.";
            }
            catch (Exception ex)
            {
                Status = "Error while getting attachments!";
                new ErrorHandler(ex).Error();
            }
        }

        public void refreshMailsForTag(string tagID)
        {
            //--- Refresh EMails
            try
            {
                tabControlMain.SelectTab("tabPageMails");

                Status = "Retrieving all messages for tag  ...";
                resMessages = Bootstrap.Instance().Talker.ListMailsBySearchTag(tagID);
                ErrorHandler.checkBizResult(resMessages);

                listViewMessages.Items.Clear();
                foreach (Hashtable t in resMessages.return_)
                {
                    ListViewItem liv = new ListViewItem((string)t["subject"], 3);
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["ALAA"])));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["dateRecieved"])));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["priority"])));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["id"])));
                    this.listViewMessages.Items.Add(liv);
                }
                Status = "Done.";
            }
            catch (Exception ex)
            {
                Status = "Error while getting messages for tag!";
                new ErrorHandler(ex).Error();
            }
        }

        public void refreshMailsForContact(string email)
        {
            //--- Refresh EMails
            try
            {
                //find user
                string userId = string.Empty;
                foreach (Hashtable t in resContacts.return_)
                {
                    if (email == Convert.ToString(t["email"]))
                    {
                        userId = Convert.ToString(t["id"]);
                        break;
                    }
                }
                if (userId == string.Empty)
                    throw new Exception("Failed to find user " + email + " !");

                tabControlMain.SelectTab("tabPageMails");
                //tabPageMails.Show();

                Status = "Retrieving all messages for user  ...";
                resMessages = Bootstrap.Instance().Talker.ListContactMails(userId);
                ErrorHandler.checkBizResult(resMessages);

                listViewMessages.Items.Clear();
                foreach (Hashtable t in resMessages.return_)
                {
                    //ListViewItem liv = new ListViewItem(Convert.ToString(t["id"]), 0);
                    ListViewItem liv = new ListViewItem((string)t["subject"], 3);
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["ALAA"])));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["dateRecieved"])));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["priority"])));
                    liv.SubItems.Add(new ListViewItem.ListViewSubItem(liv, Convert.ToString(t["id"])));
                    this.listViewMessages.Items.Add(liv);
                }
                Status = "Done.";
            }
            catch (Exception ex)
            {
                Status = "Error while getting messages for user!";
                new ErrorHandler(ex).Error();
            }
        }

        public void refreshMails()
        {
            //--- Refresh EMails
            try
            {
                Status = "Retrieving all messages  ...";
                resMessages = Bootstrap.Instance().Talker.ListMails();
                ErrorHandler.checkBizResult(resMessages);

                listViewMessages.Items.Clear();
                foreach (Hashtable t in resMessages.return_)
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
                ErrorHandler.checkBizResult(resContacts);

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

        public void ShowDownloadAttachment(string name, string id)
        {
            //--- Show Download Attachment dialog
            try
            {
                foreach (Hashtable t in resAttachments.return_)
                {
                    if (id == Convert.ToString(t["id"]))
                    {
                        Status = "Choose where to save attachment";
                        dlgSaveFile.FileName = name;
                        if (DialogResult.OK == dlgSaveFile.ShowDialog(this))
                        {
                            Status = "Retrieving attachment ...";
                            Result resAttachment = Bootstrap.Instance().Talker.GetAttachment(id);
                            ErrorHandler.checkBizResult(resAttachment);

                            // we wait ...
                            this.Cursor = Cursors.WaitCursor;

                            string file = dlgSaveFile.FileName;
                            byte[] filedata = Convert.FromBase64String( Convert.ToString(resAttachment.return_[0]["b64_content"]) );
                            System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.CreateNew);
                            fs.Write(filedata, 0, filedata.Length);
                            fs.Close();
                        }
                        break;
                    }
                }
                Status = "Done.";
            }
            catch (Exception ex)
            {
                new ErrorHandler(ex).Error();
            }
            
            this.Cursor = Cursors.Arrow;
        }

        public void ShowEditMail(string id)
        {
            //--- Show Mail Edit dialog
            try
            {
                foreach (Hashtable t in resMessages.return_)
                {
                    if (id == Convert.ToString(t["id"]) )
                    {
                        Result resMail = Bootstrap.Instance().Talker.GetMail(id);
                        ErrorHandler.checkBizResult(resMail);

                        frmMailView frmMail = new frmMailView(resMail.return_[0]);
                        frmMail.ShowDialog();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
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

        public void SendMail(frmMailView frmMail)
        {
            //--- Send Mail with collected data
            try
            {
                Status = "Sending email ...";
                Result resSendMail = Bootstrap.Instance().Talker.SendMail(
                    frmMail.To,
                    frmMail.Cc,
                    frmMail.Bcc,
                    frmMail.Subject,
                    frmMail.Body,
                    MailBoxPassword
                    );
                ErrorHandler.checkBizResult(resSendMail);

                Status = "Email sent.";
                if ( resSendMail.code == 0 )
                    MessageBox.Show("E-Mail sent successfully", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Status = "Sending email failed !";
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
            // [Refresh] button clicked
            if (tabControlMain.SelectedTab.Name == "tabPageContacts")
            {
                this.RefreshContacts();
            }
            else if (tabControlMain.SelectedTab.Name == "tabPageMails")
            {
                if (livContactCurrent != null)
                {
                    if (DialogResult.Yes == MessageBox.Show("Refresh message for specific user ?", "Display user messages", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        this.refreshMailsForContact(livContactCurrent.Text);
                        return;
                    }
                }
                this.refreshMails();
            }
            else if (tabControlMain.SelectedTab.Name == "tabPageAttachments")
            {
                this.refreshAttachments();
            }
            else if (tabControlMain.SelectedTab.Name == "tabPageTags")
            {
                this.refreshTags(string.Empty);
            }
        }

        private void btnNewMail_Click(object sender, EventArgs e)
        {
            //--- [New Mail] button clicked
            frmMailView frmMail = new frmMailView(Bootstrap.Instance().Settings.AccountAddress);
            if (DialogResult.OK == frmMail.ShowDialog())
            {
                this.SendMail(frmMail);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //--- Search Mails
            if (txtSearch.Text.Length > 0)
            {
                this.refreshTags(txtSearch.Text.Trim());
                tabControlMain.SelectTab("tabPageTags");
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Return )
                btnSearch_Click(txtSearch, e);
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
            frmMailView frmMail = new frmMailView(
                Bootstrap.Instance().Settings.AccountAddress,
                this.livContactCurrent.Text.Trim() );
            if (DialogResult.OK == frmMail.ShowDialog())
            {
                this.SendMail(frmMail);
            }
        }

        private void toolStripMenuAttachments_Click(object sender, EventArgs e)
        {
            //-- Context Menu (View Attachments)
            if (livContactCurrent != null)
                ShowEditUser(livContactCurrent.Text);
        }

        private void toolStripMenuMails_Click(object sender, EventArgs e)
        {
            //-- Context Menu (View Emails For User)
            if (livContactCurrent != null)
                refreshMailsForContact(livContactCurrent.Text);
        }

        private void listViewMessages_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //-- Open Mail Click
            ListViewItem liv = listViewMessages.GetItemAt(e.X, e.Y);
            ShowEditMail(liv.SubItems[4].Text);
        }

        private void tabControlMain_TabIndexChanged(object sender, EventArgs e)
        {
            // page changed, no sel contact
            livContactCurrent = null;
        }

        private void tabPageTags_Click(object sender, EventArgs e)
        {
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "[Search]")
                txtSearch.Clear();
        }

        private void listViewAttachments_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Download Attachment Click
            ListViewItem liv = listViewAttachments.GetItemAt(e.X, e.Y);
            ShowDownloadAttachment(liv.Text, liv.SubItems[3].Text);
        }

        #endregion
    }
}
