namespace frontend_3_5
{
    partial class frmWizAccount
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelSetAccount = new System.Windows.Forms.Panel();
            this.radioGMail = new System.Windows.Forms.RadioButton();
            this.radioEmail = new System.Windows.Forms.RadioButton();
            this.lblSetAccount = new System.Windows.Forms.Label();
            this.panelIdentity = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmailAddress = new System.Windows.Forms.TextBox();
            this.txtYourName = new System.Windows.Forms.TextBox();
            this.lblIdentity2 = new System.Windows.Forms.Label();
            this.lblIdentity = new System.Windows.Forms.Label();
            this.panelServerInformation = new System.Windows.Forms.Panel();
            this.radioIMAP = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.radioPOP = new System.Windows.Forms.RadioButton();
            this.lblServerInfo2 = new System.Windows.Forms.Label();
            this.lblServerInfo = new System.Windows.Forms.Label();
            this.txtIncomingServer = new System.Windows.Forms.TextBox();
            this.panelUserNames = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.lblUserNames2 = new System.Windows.Forms.Label();
            this.lblUserNames = new System.Windows.Forms.Label();
            this.panelReview = new System.Windows.Forms.Panel();
            this.lblReviewAll = new System.Windows.Forms.Label();
            this.lblReview2 = new System.Windows.Forms.Label();
            this.lblReview = new System.Windows.Forms.Label();
            this.panelSMTP = new System.Windows.Forms.Panel();
            this.radioSSL = new System.Windows.Forms.RadioButton();
            this.radioTLS = new System.Windows.Forms.RadioButton();
            this.radioSecTLSIf = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.radioSecNo = new System.Windows.Forms.RadioButton();
            this.lblSMTP2 = new System.Windows.Forms.Label();
            this.lblSMTP = new System.Windows.Forms.Label();
            this.txtSMTPUsername = new System.Windows.Forms.TextBox();
            this.txtSMTPPort = new System.Windows.Forms.TextBox();
            this.txtSMTP = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtIncomingServerPort = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panelSetAccount.SuspendLayout();
            this.panelIdentity.SuspendLayout();
            this.panelServerInformation.SuspendLayout();
            this.panelUserNames.SuspendLayout();
            this.panelReview.SuspendLayout();
            this.panelSMTP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(141, 16);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "New Account Setup";
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBack.Location = new System.Drawing.Point(126, 324);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(76, 28);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "< &Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnNext
            // 
            this.btnNext.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNext.Location = new System.Drawing.Point(208, 324);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(76, 28);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "&Next >";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCancel.Location = new System.Drawing.Point(299, 324);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelSetAccount
            // 
            this.panelSetAccount.Controls.Add(this.radioGMail);
            this.panelSetAccount.Controls.Add(this.radioEmail);
            this.panelSetAccount.Controls.Add(this.lblSetAccount);
            this.panelSetAccount.Location = new System.Drawing.Point(565, 0);
            this.panelSetAccount.Name = "panelSetAccount";
            this.panelSetAccount.Size = new System.Drawing.Size(373, 237);
            this.panelSetAccount.TabIndex = 3;
            // 
            // radioGMail
            // 
            this.radioGMail.AutoSize = true;
            this.radioGMail.Location = new System.Drawing.Point(31, 122);
            this.radioGMail.Name = "radioGMail";
            this.radioGMail.Size = new System.Drawing.Size(51, 17);
            this.radioGMail.TabIndex = 2;
            this.radioGMail.TabStop = true;
            this.radioGMail.Text = "Gmail";
            this.radioGMail.UseVisualStyleBackColor = true;
            // 
            // radioEmail
            // 
            this.radioEmail.AutoSize = true;
            this.radioEmail.Location = new System.Drawing.Point(31, 99);
            this.radioEmail.Name = "radioEmail";
            this.radioEmail.Size = new System.Drawing.Size(92, 17);
            this.radioEmail.TabIndex = 2;
            this.radioEmail.TabStop = true;
            this.radioEmail.Text = "E&mail account";
            this.radioEmail.UseVisualStyleBackColor = true;
            // 
            // lblSetAccount
            // 
            this.lblSetAccount.Location = new System.Drawing.Point(28, 14);
            this.lblSetAccount.Name = "lblSetAccount";
            this.lblSetAccount.Size = new System.Drawing.Size(328, 82);
            this.lblSetAccount.TabIndex = 1;
            this.lblSetAccount.Text = "label2";
            // 
            // panelIdentity
            // 
            this.panelIdentity.Controls.Add(this.label2);
            this.panelIdentity.Controls.Add(this.label1);
            this.panelIdentity.Controls.Add(this.txtEmailAddress);
            this.panelIdentity.Controls.Add(this.txtYourName);
            this.panelIdentity.Controls.Add(this.lblIdentity2);
            this.panelIdentity.Controls.Add(this.lblIdentity);
            this.panelIdentity.Location = new System.Drawing.Point(425, 14);
            this.panelIdentity.Name = "panelIdentity";
            this.panelIdentity.Size = new System.Drawing.Size(373, 256);
            this.panelIdentity.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(10, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "&Email Address";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(10, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "&Your Name";
            // 
            // txtEmailAddress
            // 
            this.txtEmailAddress.Location = new System.Drawing.Point(107, 167);
            this.txtEmailAddress.Name = "txtEmailAddress";
            this.txtEmailAddress.Size = new System.Drawing.Size(231, 20);
            this.txtEmailAddress.TabIndex = 2;
            // 
            // txtYourName
            // 
            this.txtYourName.Location = new System.Drawing.Point(107, 95);
            this.txtYourName.Name = "txtYourName";
            this.txtYourName.Size = new System.Drawing.Size(231, 20);
            this.txtYourName.TabIndex = 2;
            // 
            // lblIdentity2
            // 
            this.lblIdentity2.Location = new System.Drawing.Point(10, 128);
            this.lblIdentity2.Name = "lblIdentity2";
            this.lblIdentity2.Size = new System.Drawing.Size(328, 36);
            this.lblIdentity2.TabIndex = 1;
            this.lblIdentity2.Text = "label2";
            // 
            // lblIdentity
            // 
            this.lblIdentity.Location = new System.Drawing.Point(10, 10);
            this.lblIdentity.Name = "lblIdentity";
            this.lblIdentity.Size = new System.Drawing.Size(328, 82);
            this.lblIdentity.TabIndex = 1;
            this.lblIdentity.Text = "label2";
            // 
            // panelServerInformation
            // 
            this.panelServerInformation.Controls.Add(this.radioIMAP);
            this.panelServerInformation.Controls.Add(this.label3);
            this.panelServerInformation.Controls.Add(this.radioPOP);
            this.panelServerInformation.Controls.Add(this.lblServerInfo2);
            this.panelServerInformation.Controls.Add(this.label8);
            this.panelServerInformation.Controls.Add(this.lblServerInfo);
            this.panelServerInformation.Controls.Add(this.txtIncomingServer);
            this.panelServerInformation.Controls.Add(this.txtIncomingServerPort);
            this.panelServerInformation.Location = new System.Drawing.Point(425, 276);
            this.panelServerInformation.Name = "panelServerInformation";
            this.panelServerInformation.Size = new System.Drawing.Size(373, 237);
            this.panelServerInformation.TabIndex = 3;
            // 
            // radioIMAP
            // 
            this.radioIMAP.AutoSize = true;
            this.radioIMAP.Location = new System.Drawing.Point(31, 60);
            this.radioIMAP.Name = "radioIMAP";
            this.radioIMAP.Size = new System.Drawing.Size(51, 17);
            this.radioIMAP.TabIndex = 2;
            this.radioIMAP.TabStop = true;
            this.radioIMAP.Text = "IMAP";
            this.radioIMAP.UseVisualStyleBackColor = true;
            this.radioIMAP.CheckedChanged += new System.EventHandler(this.radioIMAP_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(28, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 19);
            this.label3.TabIndex = 3;
            this.label3.Text = "Incoming &Server:";
            // 
            // radioPOP
            // 
            this.radioPOP.AutoSize = true;
            this.radioPOP.Location = new System.Drawing.Point(31, 37);
            this.radioPOP.Name = "radioPOP";
            this.radioPOP.Size = new System.Drawing.Size(47, 17);
            this.radioPOP.TabIndex = 2;
            this.radioPOP.TabStop = true;
            this.radioPOP.Text = "POP";
            this.radioPOP.UseVisualStyleBackColor = true;
            this.radioPOP.CheckedChanged += new System.EventHandler(this.radioPOP_CheckedChanged);
            // 
            // lblServerInfo2
            // 
            this.lblServerInfo2.Location = new System.Drawing.Point(28, 99);
            this.lblServerInfo2.Name = "lblServerInfo2";
            this.lblServerInfo2.Size = new System.Drawing.Size(328, 25);
            this.lblServerInfo2.TabIndex = 1;
            this.lblServerInfo2.Text = "label2";
            // 
            // lblServerInfo
            // 
            this.lblServerInfo.Location = new System.Drawing.Point(28, 14);
            this.lblServerInfo.Name = "lblServerInfo";
            this.lblServerInfo.Size = new System.Drawing.Size(328, 20);
            this.lblServerInfo.TabIndex = 1;
            this.lblServerInfo.Text = "label2";
            // 
            // txtIncomingServer
            // 
            this.txtIncomingServer.Location = new System.Drawing.Point(131, 127);
            this.txtIncomingServer.Name = "txtIncomingServer";
            this.txtIncomingServer.Size = new System.Drawing.Size(225, 20);
            this.txtIncomingServer.TabIndex = 2;
            // 
            // panelUserNames
            // 
            this.panelUserNames.Controls.Add(this.label5);
            this.panelUserNames.Controls.Add(this.txtUserName);
            this.panelUserNames.Controls.Add(this.lblUserNames2);
            this.panelUserNames.Controls.Add(this.lblUserNames);
            this.panelUserNames.Location = new System.Drawing.Point(408, 17);
            this.panelUserNames.Name = "panelUserNames";
            this.panelUserNames.Size = new System.Drawing.Size(373, 256);
            this.panelUserNames.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(10, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 19);
            this.label5.TabIndex = 3;
            this.label5.Text = "&Incoming User Name:";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(147, 57);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(191, 20);
            this.txtUserName.TabIndex = 2;
            // 
            // lblUserNames2
            // 
            this.lblUserNames2.Location = new System.Drawing.Point(10, 92);
            this.lblUserNames2.Name = "lblUserNames2";
            this.lblUserNames2.Size = new System.Drawing.Size(328, 61);
            this.lblUserNames2.TabIndex = 1;
            this.lblUserNames2.Text = "label2";
            // 
            // lblUserNames
            // 
            this.lblUserNames.Location = new System.Drawing.Point(10, 10);
            this.lblUserNames.Name = "lblUserNames";
            this.lblUserNames.Size = new System.Drawing.Size(328, 44);
            this.lblUserNames.TabIndex = 1;
            this.lblUserNames.Text = "label2";
            // 
            // panelReview
            // 
            this.panelReview.Controls.Add(this.lblReviewAll);
            this.panelReview.Controls.Add(this.lblReview2);
            this.panelReview.Controls.Add(this.lblReview);
            this.panelReview.Location = new System.Drawing.Point(408, 324);
            this.panelReview.Name = "panelReview";
            this.panelReview.Size = new System.Drawing.Size(373, 256);
            this.panelReview.TabIndex = 4;
            // 
            // lblReviewAll
            // 
            this.lblReviewAll.Location = new System.Drawing.Point(10, 34);
            this.lblReviewAll.Name = "lblReviewAll";
            this.lblReviewAll.Size = new System.Drawing.Size(328, 174);
            this.lblReviewAll.TabIndex = 1;
            this.lblReviewAll.Text = "label2";
            // 
            // lblReview2
            // 
            this.lblReview2.Location = new System.Drawing.Point(10, 224);
            this.lblReview2.Name = "lblReview2";
            this.lblReview2.Size = new System.Drawing.Size(328, 24);
            this.lblReview2.TabIndex = 1;
            this.lblReview2.Text = "label2";
            // 
            // lblReview
            // 
            this.lblReview.Location = new System.Drawing.Point(10, 10);
            this.lblReview.Name = "lblReview";
            this.lblReview.Size = new System.Drawing.Size(328, 24);
            this.lblReview.TabIndex = 1;
            this.lblReview.Text = "label2";
            // 
            // panelSMTP
            // 
            this.panelSMTP.Controls.Add(this.radioSSL);
            this.panelSMTP.Controls.Add(this.radioTLS);
            this.panelSMTP.Controls.Add(this.radioSecTLSIf);
            this.panelSMTP.Controls.Add(this.label6);
            this.panelSMTP.Controls.Add(this.label7);
            this.panelSMTP.Controls.Add(this.label4);
            this.panelSMTP.Controls.Add(this.radioSecNo);
            this.panelSMTP.Controls.Add(this.lblSMTP2);
            this.panelSMTP.Controls.Add(this.lblSMTP);
            this.panelSMTP.Controls.Add(this.txtSMTPUsername);
            this.panelSMTP.Controls.Add(this.txtSMTPPort);
            this.panelSMTP.Controls.Add(this.txtSMTP);
            this.panelSMTP.Location = new System.Drawing.Point(2, 62);
            this.panelSMTP.Name = "panelSMTP";
            this.panelSMTP.Size = new System.Drawing.Size(373, 237);
            this.panelSMTP.TabIndex = 3;
            // 
            // radioSSL
            // 
            this.radioSSL.AutoSize = true;
            this.radioSSL.Location = new System.Drawing.Point(239, 164);
            this.radioSSL.Name = "radioSSL";
            this.radioSSL.Size = new System.Drawing.Size(45, 17);
            this.radioSSL.TabIndex = 2;
            this.radioSSL.TabStop = true;
            this.radioSSL.Text = "SSL";
            this.radioSSL.UseVisualStyleBackColor = true;
            this.radioSSL.CheckedChanged += new System.EventHandler(this.radioSSL_CheckedChanged);
            // 
            // radioTLS
            // 
            this.radioTLS.AutoSize = true;
            this.radioTLS.Location = new System.Drawing.Point(188, 164);
            this.radioTLS.Name = "radioTLS";
            this.radioTLS.Size = new System.Drawing.Size(45, 17);
            this.radioTLS.TabIndex = 2;
            this.radioTLS.TabStop = true;
            this.radioTLS.Text = "TLS";
            this.radioTLS.UseVisualStyleBackColor = true;
            this.radioTLS.CheckedChanged += new System.EventHandler(this.radioTLS_CheckedChanged);
            // 
            // radioSecTLSIf
            // 
            this.radioSecTLSIf.AutoSize = true;
            this.radioSecTLSIf.Location = new System.Drawing.Point(81, 164);
            this.radioSecTLSIf.Name = "radioSecTLSIf";
            this.radioSecTLSIf.Size = new System.Drawing.Size(101, 17);
            this.radioSecTLSIf.TabIndex = 2;
            this.radioSecTLSIf.TabStop = true;
            this.radioSecTLSIf.Text = "TLS, if available";
            this.radioSecTLSIf.UseVisualStyleBackColor = true;
            this.radioSecTLSIf.CheckedChanged += new System.EventHandler(this.radioSecTLSIf_CheckedChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(28, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 19);
            this.label6.TabIndex = 3;
            this.label6.Text = "User Name:";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(28, 63);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 19);
            this.label7.TabIndex = 3;
            this.label7.Text = "Port:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(28, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Server:";
            // 
            // radioSecNo
            // 
            this.radioSecNo.AutoSize = true;
            this.radioSecNo.Location = new System.Drawing.Point(31, 164);
            this.radioSecNo.Name = "radioSecNo";
            this.radioSecNo.Size = new System.Drawing.Size(39, 17);
            this.radioSecNo.TabIndex = 2;
            this.radioSecNo.TabStop = true;
            this.radioSecNo.Text = "No";
            this.radioSecNo.UseVisualStyleBackColor = true;
            this.radioSecNo.CheckedChanged += new System.EventHandler(this.radioSecNo_CheckedChanged);
            // 
            // lblSMTP2
            // 
            this.lblSMTP2.Location = new System.Drawing.Point(28, 112);
            this.lblSMTP2.Name = "lblSMTP2";
            this.lblSMTP2.Size = new System.Drawing.Size(328, 26);
            this.lblSMTP2.TabIndex = 1;
            this.lblSMTP2.Text = "label2";
            // 
            // lblSMTP
            // 
            this.lblSMTP.Location = new System.Drawing.Point(28, 14);
            this.lblSMTP.Name = "lblSMTP";
            this.lblSMTP.Size = new System.Drawing.Size(328, 20);
            this.lblSMTP.TabIndex = 1;
            this.lblSMTP.Text = "label2";
            // 
            // txtSMTPUsername
            // 
            this.txtSMTPUsername.Location = new System.Drawing.Point(109, 138);
            this.txtSMTPUsername.MaxLength = 255;
            this.txtSMTPUsername.Name = "txtSMTPUsername";
            this.txtSMTPUsername.Size = new System.Drawing.Size(247, 20);
            this.txtSMTPUsername.TabIndex = 2;
            // 
            // txtSMTPPort
            // 
            this.txtSMTPPort.Location = new System.Drawing.Point(81, 63);
            this.txtSMTPPort.MaxLength = 5;
            this.txtSMTPPort.Name = "txtSMTPPort";
            this.txtSMTPPort.Size = new System.Drawing.Size(84, 20);
            this.txtSMTPPort.TabIndex = 2;
            // 
            // txtSMTP
            // 
            this.txtSMTP.Location = new System.Drawing.Point(81, 37);
            this.txtSMTP.Name = "txtSMTP";
            this.txtSMTP.Size = new System.Drawing.Size(275, 20);
            this.txtSMTP.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(391, 56);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // txtIncomingServerPort
            // 
            this.txtIncomingServerPort.Location = new System.Drawing.Point(130, 153);
            this.txtIncomingServerPort.MaxLength = 5;
            this.txtIncomingServerPort.Name = "txtIncomingServerPort";
            this.txtIncomingServerPort.Size = new System.Drawing.Size(84, 20);
            this.txtIncomingServerPort.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(31, 153);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 19);
            this.label8.TabIndex = 3;
            this.label8.Text = "Port:";
            // 
            // frmWizAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 366);
            this.Controls.Add(this.panelSMTP);
            this.Controls.Add(this.panelServerInformation);
            this.Controls.Add(this.panelSetAccount);
            this.Controls.Add(this.panelReview);
            this.Controls.Add(this.panelUserNames);
            this.Controls.Add(this.panelIdentity);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWizAccount";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmAccountWiz1";
            this.Load += new System.EventHandler(this.frmAccountWiz1_Load);
            this.panelSetAccount.ResumeLayout(false);
            this.panelSetAccount.PerformLayout();
            this.panelIdentity.ResumeLayout(false);
            this.panelIdentity.PerformLayout();
            this.panelServerInformation.ResumeLayout(false);
            this.panelServerInformation.PerformLayout();
            this.panelUserNames.ResumeLayout(false);
            this.panelUserNames.PerformLayout();
            this.panelReview.ResumeLayout(false);
            this.panelSMTP.ResumeLayout(false);
            this.panelSMTP.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelSetAccount;
        private System.Windows.Forms.RadioButton radioGMail;
        private System.Windows.Forms.RadioButton radioEmail;
        private System.Windows.Forms.Label lblSetAccount;
        private System.Windows.Forms.Panel panelIdentity;
        private System.Windows.Forms.Label lblIdentity;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtYourName;
        private System.Windows.Forms.Label lblIdentity2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmailAddress;
        private System.Windows.Forms.Panel panelServerInformation;
        private System.Windows.Forms.RadioButton radioIMAP;
        private System.Windows.Forms.RadioButton radioPOP;
        private System.Windows.Forms.Label lblServerInfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblServerInfo2;
        private System.Windows.Forms.TextBox txtIncomingServer;
        private System.Windows.Forms.Panel panelUserNames;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.Label lblUserNames2;
        private System.Windows.Forms.Label lblUserNames;
        private System.Windows.Forms.Panel panelReview;
        private System.Windows.Forms.Label lblReview;
        private System.Windows.Forms.Label lblReviewAll;
        private System.Windows.Forms.Label lblReview2;
        private System.Windows.Forms.Panel panelSMTP;
        private System.Windows.Forms.RadioButton radioSecTLSIf;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton radioSecNo;
        private System.Windows.Forms.Label lblSMTP2;
        private System.Windows.Forms.Label lblSMTP;
        private System.Windows.Forms.TextBox txtSMTP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSMTPPort;
        private System.Windows.Forms.RadioButton radioSSL;
        private System.Windows.Forms.RadioButton radioTLS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSMTPUsername;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtIncomingServerPort;

    }
}