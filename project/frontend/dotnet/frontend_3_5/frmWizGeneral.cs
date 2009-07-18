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
    public partial class frmWizGeneral : Form
    {
        private enum SetupStates
        {
            SS_General = 0,
            SS_Review
        }
        private SetupStates currentState = SetupStates.SS_General;
        private Hashtable hashAccountInfo = new Hashtable(10);

        public Hashtable AccountInfo
        {
            get
            {
                return this.hashAccountInfo;
            }
        }

        public frmWizGeneral()
        {
            InitializeComponent();
        }

        private void frmAccountWiz1_Load(object sender, EventArgs e)
        {
            this.Text = "EmEx General Configuration";
            SetupPanels();
            setState(SetupStates.SS_General);
        }

        private void resetPanels()
        {
            panelGeneral.Location = new Point(2, 62);
            panelGeneral.Hide();
            panelReview.Location = new Point(2, 62);
            panelReview.Hide();
        }

        private void SetupPanels()
        {
            //SS_lblGeneral Panel
            lblGeneral.Text = "Welcome ! This is the first time you run Email Extended (EmEx) client. ";
            lblGeneral.Text += Environment.NewLine;
            lblGeneral.Text += "You need to setup the application settings before we start. This is only done once.";

            //SS_Overview Panel
            lblReview.Text = "Please verify that the information below is correct.";
            lblReview2.Text = "Click Finish to save these settings and exit the Account Wizzard.";

            txtBackendServer.Text = "127.0.0.1";
            txtBackendPort.Text = "8080";
            txtBackendPath.Text = Environment.CurrentDirectory;

            resetPanels();
        }

        private void setState(SetupStates state)
        {
            switch (state)
            {
                case SetupStates.SS_General:
                    lblTitle.Text = "EmEx General Configuration";
                    btnNext.Text = "Next >";
                    resetPanels();
                    panelGeneral.Show();
                    break;

                case SetupStates.SS_Review:
                    lblTitle.Text = "Congratulations!";
                    btnNext.Text = "Finish";
                    resetPanels();
                    panelReview.Show();

                    this.hashAccountInfo["backend_server"] = txtBackendServer.Text.Trim();
                    this.hashAccountInfo["backend_port"] = txtBackendPort.Text.Trim();
                    this.hashAccountInfo["backend_path"] = txtBackendPath.Text.Trim();

                    // show info
                    lblReviewAll.Text = "Backend Server: " + this.hashAccountInfo["backend_server"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Backend Port: " + this.hashAccountInfo["backend_port"];
                    lblReviewAll.Text += Environment.NewLine;
                    lblReviewAll.Text += "Backend Path: " + this.hashAccountInfo["backend_path"];
                    lblReviewAll.Text += Environment.NewLine;
                    break;
            }
            currentState = state;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if ( (int)currentState - 1 >= 0 )
                currentState = (SetupStates)((int)currentState - 1);
            setState(currentState);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case SetupStates.SS_General:
                    if (txtBackendServer.Text.Length < 1)
                    {
                        MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBackendServer.Focus();
                        return;
                    }
                    if (txtBackendPort.Text.Length < 1)
                    {
                        MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBackendPort.Focus();
                        return;
                    }
                    if (txtBackendPath.Text.Length < 1)
                    {
                        MessageBox.Show("Please fill in the input box!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtBackendPath.Focus();
                        return;
                    }
                    setState(SetupStates.SS_Review);
                    break;

                case SetupStates.SS_Review:
                    // FINISH !

                    try
                    {
                        Bootstrap.Instance().Settings.Save(this.hashAccountInfo);
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

    }
}
