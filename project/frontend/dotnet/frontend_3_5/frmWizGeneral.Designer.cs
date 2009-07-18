namespace frontend_3_5
{
    partial class frmWizGeneral
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
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblGeneral = new System.Windows.Forms.Label();
            this.txtBackendPath = new System.Windows.Forms.TextBox();
            this.txtBackendPort = new System.Windows.Forms.TextBox();
            this.txtBackendServer = new System.Windows.Forms.TextBox();
            this.panelReview = new System.Windows.Forms.Panel();
            this.lblReviewAll = new System.Windows.Forms.Label();
            this.lblReview2 = new System.Windows.Forms.Label();
            this.lblReview = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelGeneral.SuspendLayout();
            this.panelReview.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(142, 16);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "EmEx Configuration";
            // 
            // btnBack
            // 
            this.btnBack.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBack.Location = new System.Drawing.Point(126, 362);
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
            this.btnNext.Location = new System.Drawing.Point(208, 362);
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
            this.btnCancel.Location = new System.Drawing.Point(299, 362);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelGeneral
            // 
            this.panelGeneral.Controls.Add(this.label7);
            this.panelGeneral.Controls.Add(this.label1);
            this.panelGeneral.Controls.Add(this.label4);
            this.panelGeneral.Controls.Add(this.lblGeneral);
            this.panelGeneral.Controls.Add(this.txtBackendPath);
            this.panelGeneral.Controls.Add(this.txtBackendPort);
            this.panelGeneral.Controls.Add(this.txtBackendServer);
            this.panelGeneral.Location = new System.Drawing.Point(2, 62);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Size = new System.Drawing.Size(373, 281);
            this.panelGeneral.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(22, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 19);
            this.label7.TabIndex = 3;
            this.label7.Text = "Backend Port:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(22, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "EmEx Path:";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(22, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Backend Server:";
            // 
            // lblGeneral
            // 
            this.lblGeneral.Location = new System.Drawing.Point(22, 13);
            this.lblGeneral.Name = "lblGeneral";
            this.lblGeneral.Size = new System.Drawing.Size(328, 60);
            this.lblGeneral.TabIndex = 1;
            this.lblGeneral.Text = "label2";
            // 
            // txtBackendPath
            // 
            this.txtBackendPath.Location = new System.Drawing.Point(124, 126);
            this.txtBackendPath.Name = "txtBackendPath";
            this.txtBackendPath.Size = new System.Drawing.Size(226, 20);
            this.txtBackendPath.TabIndex = 2;
            // 
            // txtBackendPort
            // 
            this.txtBackendPort.Location = new System.Drawing.Point(124, 101);
            this.txtBackendPort.MaxLength = 5;
            this.txtBackendPort.Name = "txtBackendPort";
            this.txtBackendPort.Size = new System.Drawing.Size(84, 20);
            this.txtBackendPort.TabIndex = 2;
            // 
            // txtBackendServer
            // 
            this.txtBackendServer.Location = new System.Drawing.Point(124, 76);
            this.txtBackendServer.Name = "txtBackendServer";
            this.txtBackendServer.Size = new System.Drawing.Size(226, 20);
            this.txtBackendServer.TabIndex = 2;
            // 
            // panelReview
            // 
            this.panelReview.Controls.Add(this.lblReviewAll);
            this.panelReview.Controls.Add(this.lblReview2);
            this.panelReview.Controls.Add(this.lblReview);
            this.panelReview.Location = new System.Drawing.Point(416, 12);
            this.panelReview.Name = "panelReview";
            this.panelReview.Size = new System.Drawing.Size(373, 256);
            this.panelReview.TabIndex = 5;
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
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.Location = new System.Drawing.Point(-2, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(391, 56);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // frmWizGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 406);
            this.Controls.Add(this.panelReview);
            this.Controls.Add(this.panelGeneral);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWizGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmWizGeneral";
            this.Load += new System.EventHandler(this.frmAccountWiz1_Load);
            this.panelGeneral.ResumeLayout(false);
            this.panelGeneral.PerformLayout();
            this.panelReview.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblGeneral;
        private System.Windows.Forms.TextBox txtBackendServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBackendPort;
        private System.Windows.Forms.Panel panelReview;
        private System.Windows.Forms.Label lblReviewAll;
        private System.Windows.Forms.Label lblReview2;
        private System.Windows.Forms.Label lblReview;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBackendPath;

    }
}