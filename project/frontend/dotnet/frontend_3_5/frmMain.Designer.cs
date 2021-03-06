﻿namespace frontend_3_5
{
    partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.emExToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageContacts = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader("(none)");
            this.columnHeader22 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.imgListContacts = new System.Windows.Forms.ImageList(this.components);
            this.tabPageMails = new System.Windows.Forms.TabPage();
            this.listViewMessages = new System.Windows.Forms.ListView();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader21 = new System.Windows.Forms.ColumnHeader();
            this.tabPageAttachments = new System.Windows.Forms.TabPage();
            this.listViewAttachments = new System.Windows.Forms.ListView();
            this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader19 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader20 = new System.Windows.Forms.ColumnHeader();
            this.tabPageTags = new System.Windows.Forms.TabPage();
            this.panelTags = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnNewMail = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.contextMenuContacts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuNewMail = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuMails = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAttachments = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuProfile = new System.Windows.Forms.ToolStripMenuItem();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
            this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.menuStripMain.SuspendLayout();
            this.tabControlMain.SuspendLayout();
            this.tabPageContacts.SuspendLayout();
            this.tabPageMails.SuspendLayout();
            this.tabPageAttachments.SuspendLayout();
            this.tabPageTags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.contextMenuContacts.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMain
            // 
            this.menuStripMain.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.emExToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(393, 24);
            this.menuStripMain.TabIndex = 0;
            this.menuStripMain.Text = "menuStrip1";
            // 
            // emExToolStripMenuItem
            // 
            this.emExToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.emExToolStripMenuItem.Name = "emExToolStripMenuItem";
            this.emExToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.emExToolStripMenuItem.Text = "EmEx";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountsToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // accountsToolStripMenuItem
            // 
            this.accountsToolStripMenuItem.AccessibleName = "Accounts";
            this.accountsToolStripMenuItem.Name = "accountsToolStripMenuItem";
            this.accountsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.accountsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.accountsToolStripMenuItem.Text = "&Accounts";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem1});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.aboutToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(103, 22);
            this.aboutToolStripMenuItem1.Text = "&About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.BackColor = global::frontend_3_5.Properties.Settings.Default.TabColor;
            this.tabControlMain.Controls.Add(this.tabPageContacts);
            this.tabControlMain.Controls.Add(this.tabPageMails);
            this.tabControlMain.Controls.Add(this.tabPageAttachments);
            this.tabControlMain.Controls.Add(this.tabPageTags);
            this.tabControlMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlMain.ImageList = this.imgListContacts;
            this.tabControlMain.Location = new System.Drawing.Point(1, 231);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(391, 409);
            this.tabControlMain.TabIndex = 6;
            this.tabControlMain.TabIndexChanged += new System.EventHandler(this.tabControlMain_TabIndexChanged);
            // 
            // tabPageContacts
            // 
            this.tabPageContacts.Controls.Add(this.listView1);
            this.tabPageContacts.ImageIndex = 2;
            this.tabPageContacts.Location = new System.Drawing.Point(4, 25);
            this.tabPageContacts.Name = "tabPageContacts";
            this.tabPageContacts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContacts.Size = new System.Drawing.Size(383, 380);
            this.tabPageContacts.TabIndex = 0;
            this.tabPageContacts.Text = "Contacts";
            this.tabPageContacts.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader22,
            this.columnHeader2});
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(383, 380);
            this.listView1.SmallImageList = this.imgListContacts;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            this.listView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Contact";
            this.columnHeader1.Width = 175;
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "Full Name";
            this.columnHeader22.Width = 125;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Messages";
            // 
            // imgListContacts
            // 
            this.imgListContacts.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgListContacts.ImageStream")));
            this.imgListContacts.TransparentColor = System.Drawing.Color.Transparent;
            this.imgListContacts.Images.SetKeyName(0, "user.png");
            this.imgListContacts.Images.SetKeyName(1, "wrench.png");
            this.imgListContacts.Images.SetKeyName(2, "group.png");
            this.imgListContacts.Images.SetKeyName(3, "email.png");
            this.imgListContacts.Images.SetKeyName(4, "flag_purple.png");
            this.imgListContacts.Images.SetKeyName(5, "email_open_image.png");
            this.imgListContacts.Images.SetKeyName(6, "email_attach.png");
            // 
            // tabPageMails
            // 
            this.tabPageMails.Controls.Add(this.listViewMessages);
            this.tabPageMails.ImageIndex = 3;
            this.tabPageMails.Location = new System.Drawing.Point(4, 25);
            this.tabPageMails.Name = "tabPageMails";
            this.tabPageMails.Size = new System.Drawing.Size(383, 380);
            this.tabPageMails.TabIndex = 2;
            this.tabPageMails.Text = "Messages";
            this.tabPageMails.UseVisualStyleBackColor = true;
            // 
            // listViewMessages
            // 
            this.listViewMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader21});
            this.listViewMessages.GridLines = true;
            this.listViewMessages.LargeImageList = this.imgListContacts;
            this.listViewMessages.Location = new System.Drawing.Point(0, 0);
            this.listViewMessages.Name = "listViewMessages";
            this.listViewMessages.Size = new System.Drawing.Size(382, 377);
            this.listViewMessages.SmallImageList = this.imgListContacts;
            this.listViewMessages.TabIndex = 1;
            this.listViewMessages.UseCompatibleStateImageBehavior = false;
            this.listViewMessages.View = System.Windows.Forms.View.Details;
            this.listViewMessages.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewMessages_MouseDoubleClick);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Subject";
            this.columnHeader3.Width = 152;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Sender";
            this.columnHeader4.Width = 75;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Date";
            this.columnHeader5.Width = 71;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Priority";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Id";
            this.columnHeader7.Width = 0;
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "Size";
            // 
            // tabPageAttachments
            // 
            this.tabPageAttachments.Controls.Add(this.listViewAttachments);
            this.tabPageAttachments.ImageIndex = 6;
            this.tabPageAttachments.Location = new System.Drawing.Point(4, 25);
            this.tabPageAttachments.Name = "tabPageAttachments";
            this.tabPageAttachments.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAttachments.Size = new System.Drawing.Size(383, 380);
            this.tabPageAttachments.TabIndex = 1;
            this.tabPageAttachments.Text = "Attachments";
            this.tabPageAttachments.UseVisualStyleBackColor = true;
            // 
            // listViewAttachments
            // 
            this.listViewAttachments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20});
            this.listViewAttachments.GridLines = true;
            this.listViewAttachments.Location = new System.Drawing.Point(0, 0);
            this.listViewAttachments.Name = "listViewAttachments";
            this.listViewAttachments.Size = new System.Drawing.Size(382, 377);
            this.listViewAttachments.SmallImageList = this.imgListContacts;
            this.listViewAttachments.TabIndex = 0;
            this.listViewAttachments.UseCompatibleStateImageBehavior = false;
            this.listViewAttachments.View = System.Windows.Forms.View.Details;
            this.listViewAttachments.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewAttachments_MouseDoubleClick);
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Name";
            this.columnHeader18.Width = 129;
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "Type";
            this.columnHeader19.Width = 103;
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "Sender";
            this.columnHeader20.Width = 124;
            // 
            // tabPageTags
            // 
            this.tabPageTags.Controls.Add(this.panelTags);
            this.tabPageTags.ImageIndex = 4;
            this.tabPageTags.Location = new System.Drawing.Point(4, 25);
            this.tabPageTags.Name = "tabPageTags";
            this.tabPageTags.Size = new System.Drawing.Size(383, 380);
            this.tabPageTags.TabIndex = 0;
            this.tabPageTags.Text = "Tags";
            this.tabPageTags.UseVisualStyleBackColor = true;
            this.tabPageTags.Click += new System.EventHandler(this.tabPageTags_Click);
            // 
            // panelTags
            // 
            this.panelTags.Location = new System.Drawing.Point(0, 0);
            this.panelTags.Name = "panelTags";
            this.panelTags.Size = new System.Drawing.Size(383, 384);
            this.panelTags.TabIndex = 0;
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(45)))), ((int)(((byte)(145)))));
            this.txtSearch.Location = new System.Drawing.Point(12, 127);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(328, 26);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.Text = "[Search]";
            this.txtSearch.Click += new System.EventHandler(this.txtSearch_Click);
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackgroundImage = global::frontend_3_5.Properties.Resources.footer_bg1;
            this.pictureBox4.Location = new System.Drawing.Point(-1, 646);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(393, 42);
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = global::frontend_3_5.Properties.Resources.go_btn;
            this.btnSearch.Location = new System.Drawing.Point(346, 126);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(35, 27);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(45)))), ((int)(((byte)(145)))));
            this.btnRefresh.Image = global::frontend_3_5.Properties.Resources.refresh_btn;
            this.btnRefresh.Location = new System.Drawing.Point(106, 174);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(88, 27);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnNewMail
            // 
            this.btnNewMail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnNewMail.ForeColor = System.Drawing.Color.White;
            this.btnNewMail.Image = global::frontend_3_5.Properties.Resources.new_mail_btn;
            this.btnNewMail.Location = new System.Drawing.Point(12, 174);
            this.btnNewMail.Name = "btnNewMail";
            this.btnNewMail.Size = new System.Drawing.Size(88, 27);
            this.btnNewMail.TabIndex = 3;
            this.btnNewMail.Text = "New Mail";
            this.btnNewMail.UseVisualStyleBackColor = true;
            this.btnNewMail.Click += new System.EventHandler(this.btnNewMail_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::frontend_3_5.Properties.Resources.header_bg;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 24);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(393, 86);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.Location = new System.Drawing.Point(8, 655);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(45, 15);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Ready.";
            // 
            // contextMenuContacts
            // 
            this.contextMenuContacts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuNewMail,
            this.toolStripMenuMails,
            this.toolStripMenuAttachments,
            this.toolStripMenuProfile});
            this.contextMenuContacts.Name = "contextMenuContacts";
            this.contextMenuContacts.Size = new System.Drawing.Size(136, 92);
            // 
            // toolStripMenuNewMail
            // 
            this.toolStripMenuNewMail.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuNewMail.Image")));
            this.toolStripMenuNewMail.Name = "toolStripMenuNewMail";
            this.toolStripMenuNewMail.ShortcutKeyDisplayString = "";
            this.toolStripMenuNewMail.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuNewMail.Text = "New E-Mail";
            this.toolStripMenuNewMail.Click += new System.EventHandler(this.toolStripMenuNewMail_Click);
            // 
            // toolStripMenuMails
            // 
            this.toolStripMenuMails.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuMails.Image")));
            this.toolStripMenuMails.Name = "toolStripMenuMails";
            this.toolStripMenuMails.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuMails.Text = "Messages";
            this.toolStripMenuMails.Click += new System.EventHandler(this.toolStripMenuMails_Click);
            // 
            // toolStripMenuAttachments
            // 
            this.toolStripMenuAttachments.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuAttachments.Image")));
            this.toolStripMenuAttachments.Name = "toolStripMenuAttachments";
            this.toolStripMenuAttachments.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuAttachments.Text = "Attachments";
            this.toolStripMenuAttachments.Click += new System.EventHandler(this.toolStripMenuAttachments_Click);
            // 
            // toolStripMenuProfile
            // 
            this.toolStripMenuProfile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuProfile.Image")));
            this.toolStripMenuProfile.Name = "toolStripMenuProfile";
            this.toolStripMenuProfile.Size = new System.Drawing.Size(135, 22);
            this.toolStripMenuProfile.Text = "Profile";
            this.toolStripMenuProfile.Click += new System.EventHandler(this.toolStripMenuProfile_Click);
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Subject";
            this.columnHeader8.Width = 158;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Sender";
            this.columnHeader9.Width = 80;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Date";
            this.columnHeader10.Width = 71;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Priority";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Id";
            this.columnHeader12.Width = 0;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Subject";
            this.columnHeader13.Width = 158;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Sender";
            this.columnHeader14.Width = 80;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Date";
            this.columnHeader15.Width = 71;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Priority";
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Id";
            this.columnHeader17.Width = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.ClientSize = new System.Drawing.Size(393, 688);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnNewMail);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.menuStripMain);
            this.Controls.Add(this.pictureBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStripMain;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmEx";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageContacts.ResumeLayout(false);
            this.tabPageMails.ResumeLayout(false);
            this.tabPageAttachments.ResumeLayout(false);
            this.tabPageTags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.contextMenuContacts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem emExToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageContacts;
        private System.Windows.Forms.TabPage tabPageAttachments;
        private System.Windows.Forms.TabPage tabPageTags;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnNewMail;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ImageList imgListContacts;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ListView listViewAttachments;
        private System.Windows.Forms.ContextMenuStrip contextMenuContacts;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuNewMail;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAttachments;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuProfile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuMails;
        private System.Windows.Forms.TabPage tabPageMails;
        private System.Windows.Forms.ListView listViewMessages;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader18;
        private System.Windows.Forms.ColumnHeader columnHeader19;
        private System.Windows.Forms.ColumnHeader columnHeader20;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.ColumnHeader columnHeader17;
        private System.Windows.Forms.Panel panelTags;
        private System.Windows.Forms.SaveFileDialog dlgSaveFile;
        private System.Windows.Forms.ColumnHeader columnHeader21;
        private System.Windows.Forms.ColumnHeader columnHeader22;
        private System.Windows.Forms.ToolStripMenuItem accountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
    }
}

