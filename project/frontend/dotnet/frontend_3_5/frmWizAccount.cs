using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using frontend_3_5.Proc;
using frontend_3_5.Utils;

namespace frontend_3_5
{
    public partial class frmWizAccount : Form
    {
        private enum SetupStates
        {
            SS_AccountType,
            SS_Identity,
            SS_ServerInfo,
            SS_UserNames,
            SS_SMTP,
            SS_Review
        }
        private SetupStates currentState = SetupStates.SS_AccountType;
        private Hashtable hashAccountInfo = new Hashtable(10);

        public Hashtable AccountInfo
        {
            get
            {
                return this.hashAccountInfo;
            }
        }

        public frmWizAccount()
        {
            InitializeComponent();
        }

        private void frmAccountWiz1_Load(object sender, EventArgs e)
        {
            this.Text = "New Account Configuration";
            SetupPanels();
            setState(SetupStates.SS_AccountType);
            //setState(SetupStates.SS_UserNames);
        }

        private void resetPanels()
        {
            panelSetAccount.Location = new Point(2, 62);
            panelSetAccount.Hide();
            panelIdentity.Location = new Point(2, 62);
            panelIdentity.Hide();
            panelServerInformation.Location = new Point(2, 62);
            panelServerInformation.Hide();
            panelUserNames.Location = new Point(2, 62);
            panelUserNames.Hide();
            panelSMTP.Location = new Point(2, 62);
            panelSMTP.Hide();
            panelReview.Location = new Point(2, 62);
            panelReview.Hide();
        }

        private void SetupPanels()
        {
            //SS_AccountType Panel
            lblSetAccount.Text = "In order to recieve message you will first need to setup Mail account.";
            lblSetAccount.Text += Environment.NewLine;
            lblSetAccount.Text += "The wizzard will collect all information necessary to setup a new Mail account.";

            //SS_Identity Panel
            lblIdentity.Text = "Each account has an identity, which is the information that identifies you to others when they recieve your messages.";
            lblIdentity.Text += Environment.NewLine;
            lblIdentity.Text += Environment.NewLine;
            lblIdentity.Text += "Enter the name you would like to appear in the \"From\" field of your outgoing messages (i.e. Ivan Petrov).";
            lblIdentity2.Text = "Enter your email address. This is the address others will use to send email to you (i.e. \"ivan.petrov@example.net\").";
            
            //SS_ServerInfo Panel
            lblServerInfo.Text = "Select type of incoming server you are using.";
            lblServerInfo2.Text = "Enter the name of your incoming server (i.e. mail.example.net).";

            //SS_UserNames Panel
            lblUserNames.Text = "Enter the incoming user name given to you by your email provider (i.e. ivanpetrov).";
            lblUserNames2.Text = "Your existing outgoing (SMTP) username, \"" + txtEmailAddress.Text + "\" will be used.";
            lblUserNames2.Text += Environment.NewLine;
            lblUserNames2.Text += "You can modify outgoing server settings by choosing Account Settings from the Tools menu.";

            //SS_SMTP
            lblSMTP.Text = "Setup the outgoing SMTP server for your Mail.";
            lblSMTP2.Text = "Use secure connection:";

            //SS_Overview Panel
            lblReview.Text = "Please verify that the information below is correct.";
            lblReview2.Text = "Click Finish to save these settings and exit the Account Wizzard.";

            radioEmail.Checked = true;
            radioIMAP.Checked = true;
            radioPOP.Enabled = false;
            radioSecNo.Checked = true;
            txtSMTPPort.Text = "25";

            resetPanels();
        }

        private void doGmailSetup()
        {
            this.hashAccountInfo["full_name"] = txtYourName.Text.Trim();
            this.hashAccountInfo["email"] = txtEmailAddress.Text.Trim();
            this.hashAccountInfo["incoming_server"] = "imap.gmail.com";
            this.hashAccountInfo["incoming_port"] = "993";
            this.hashAccountInfo["username"] = txtUserName.Text.Trim();
            this.hashAccountInfo["server_type"] = "imap";
            this.hashAccountInfo["smtp_server"] = "smtp.gmail.com";
            this.hashAccountInfo["smtp_port"] = "587";
            this.hashAccountInfo["smtp_username"] = txtEmailAddress.Text.Trim();
            this.hashAccountInfo["smtp_security"] = "SSL";
        }

        private void setState(SetupStates state)
        {
            switch (state)
            {
                case SetupStates.SS_AccountType:
                    lblTitle.Text = "New Account Setup";
                    resetPanels();
                    panelSetAccount.Show();
                    break;

                case SetupStates.SS_Identity:
                    lblTitle.Text = "Identity";
                    resetPanels();
                    panelIdentity.Show();
                    break;

                case SetupStates.SS_ServerInfo:
                    lblTitle.Text = "Server Information";
                    resetPanels();

                    // auto fill-in info 
                    string[] creds = txtEmailAddress.Text.Trim().Split('@');
                    txtIncomingServer.Text = creds[1];
                    txtUserName.Text = creds[0];
                    txtSMTP.Text = creds[1];
                    txtSMTPUsername.Text = creds[0];

                    panelServerInformation.Show();
                    break;

                case SetupStates.SS_UserNames:
                    lblTitle.Text = "User Names";
                    resetPanels();
                    panelUserNames.Show();
                    break;

                case SetupStates.SS_SMTP:
                    lblTitle.Text = "SMTP Settings";
                    btnNext.Text = "&Next >";
                    resetPanels();
                    panelSMTP.Show();
                    break;

                case SetupStates.SS_Review:
                    lblTitle.Text = "Congratulations!";
                    btnNext.Text = "Finish";
                    resetPanels();
                    panelReview.Show();

                    if ( radioGMail.Checked )
                    {
                        // preconfigure hashtable with GMAIL settings
                        doGmailSetup();
                    }
                    else
                    {
                        this.hashAccountInfo["full_name"] = txtYourName.Text.Trim();
                        this.hashAccountInfo["email"] = txtEmailAddress.Text.Trim();
                        this.hashAccountInfo["incoming_server"] = txtIncomingServer.Text.Trim();
                        this.hashAccountInfo["incoming_port"] = txtIncomingServerPort.Text.Trim();
                        this.hashAccountInfo["incoming_security"] = "No"; //TODO: HARDCODED?
                        this.hashAccountInfo["username"] = txtUserName.Text.Trim();
                        this.hashAccountInfo["server_type"] =
                            radioIMAP.Checked ? "imap" : (radioPOP.Checked ? "pop" : "undefined");
                        this.hashAccountInfo["smtp_server"] = txtSMTP.Text.Trim();
                        this.hashAccountInfo["smtp_port"] = txtSMTPPort.Text.Trim();
                        this.hashAccountInfo["smtp_username"] = txtSMTPUsername.Text.Trim();
                        this.hashAccountInfo["smtp_security"] =
                            radioSecNo.Checked ? "No" :
                            radioSecTLSIf.Checked ? "TLSIF" :
                            radioTLS.Checked ? "TLS" :
                            radioSSL.Checked ? "SSL" : "No";
                    }

                    // show info
                    lblReviewAll.Text = "Full Name: " + this.hashAccountInfo["full_name"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Email Address: " + this.hashAccountInfo["email"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Incoming User Name: " + this.hashAccountInfo["username"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Incoming Server Name: " + this.hashAccountInfo["incoming_server"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Incoming Server Port: " + this.hashAccountInfo["incoming_port"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Incoming Server Type: " + this.hashAccountInfo["server_type"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Outgoing Server Name (SMTP): " + this.hashAccountInfo["smtp_server"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Outgoing Server Port: " + this.hashAccountInfo["smtp_port"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Outgoing Server User Name: " + this.hashAccountInfo["smtp_username"];
                    break;
            }
            currentState = state;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if ( (int)currentState - 1 >= 0 )
                currentState = (SetupStates)((int)currentState - 1);
            if (radioGMail.Checked && (int)currentState > 1)
                currentState = SetupStates.SS_Identity;
            setState(currentState);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case SetupStates.SS_AccountType:
                    setState(SetupStates.SS_Identity);
                    break;

                case SetupStates.SS_Identity:
                    if (txtYourName.Text.Length < 1)
                    {
                        MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtYourName.Focus();
                        return;
                    }
                    if (txtEmailAddress.Text.Length < 1)
                    {
                        MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtEmailAddress.Focus();
                        return;
                    }

                    // jump to Review if GMAIL selected
                    if (radioGMail.Checked)
                    {
                        setState(SetupStates.SS_Review);
                    }
                    else
                    {
                        setState(SetupStates.SS_ServerInfo);
                    }
                    break;

                case SetupStates.SS_ServerInfo:
                    if (txtIncomingServer.Text.Length < 1)
                    {
                        MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtIncomingServer.Focus();
                        return;
                    }
                    setState(SetupStates.SS_UserNames);
                    break;

                case SetupStates.SS_UserNames:
                    if (txtUserName.Text.Length < 1)
                    {
                        MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Focus();
                        return;
                    }
                    setState(SetupStates.SS_SMTP);
                    break;

                case SetupStates.SS_SMTP:
                    if (txtUserName.Text.Length < 1)
                    {
                        MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUserName.Focus();
                        return;
                    }
                    setState(SetupStates.SS_Review);
                    break;

                case SetupStates.SS_Review:
                    try
                    {
                        frmPassword frmPass = new frmPassword();
                        if (DialogResult.OK != frmPass.ShowDialog())
                            throw new Exception("The operation cannot continue without specifiing a password!");

                        // save to frontend
                        Bootstrap.Instance().Settings.SaveAccountInfo(
                            (string)this.hashAccountInfo["email"],
                            frmPass.AccountPassword );
                        // save to backend
                        //TODO: not here right now!

                        // exit
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        new ErrorHandler(ex).Error();
                    }
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void radioSecTLSIf_CheckedChanged(object sender, EventArgs e)
        {
            radioSecNo.Checked = radioTLS.Checked = radioSSL.Checked = false;
            txtSMTPPort.Text = "25";
        }

        private void radioSecNo_CheckedChanged(object sender, EventArgs e)
        {
            radioSecTLSIf.Checked = radioTLS.Checked = radioSSL.Checked = false;
            txtSMTPPort.Text = "25";
        }

        private void radioTLS_CheckedChanged(object sender, EventArgs e)
        {
            radioSecTLSIf.Checked = radioSecNo.Checked = radioSSL.Checked = false;
            txtSMTPPort.Text = "587";
        }

        private void radioSSL_CheckedChanged(object sender, EventArgs e)
        {
            radioSecTLSIf.Checked = radioSecNo.Checked = radioTLS.Checked = false;
            txtSMTPPort.Text = "465";
        }

        private void radioPOP_CheckedChanged(object sender, EventArgs e)
        {
            radioIMAP.Checked = false;
            txtIncomingServerPort.Text = "110";
        }

        private void radioIMAP_CheckedChanged(object sender, EventArgs e)
        {
            radioPOP.Checked = false;
            txtIncomingServerPort.Text = "143";
        }
    }
}
