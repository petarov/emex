namespace frontend_3_5
{
    partial class frmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.picBoxBackground = new System.Windows.Forms.PictureBox();
            this.picBoxTitle = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTitle)).BeginInit();
            this.SuspendLayout();
            // 
            // picBoxBackground
            // 
            this.picBoxBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxBackground.Image = global::frontend_3_5.Properties.Resources.header_img;
            this.picBoxBackground.InitialImage = null;
            this.picBoxBackground.Location = new System.Drawing.Point(0, 0);
            this.picBoxBackground.Name = "picBoxBackground";
            this.picBoxBackground.Size = new System.Drawing.Size(484, 249);
            this.picBoxBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxBackground.TabIndex = 0;
            this.picBoxBackground.TabStop = false;
            this.picBoxBackground.WaitOnLoad = true;
            // 
            // picBoxTitle
            // 
            this.picBoxTitle.Image = ((System.Drawing.Image)(resources.GetObject("picBoxTitle.Image")));
            this.picBoxTitle.Location = new System.Drawing.Point(55, 0);
            this.picBoxTitle.Name = "picBoxTitle";
            this.picBoxTitle.Size = new System.Drawing.Size(393, 86);
            this.picBoxTitle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picBoxTitle.TabIndex = 1;
            this.picBoxTitle.TabStop = false;
            this.picBoxTitle.WaitOnLoad = true;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStatus.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.lblStatus.Location = new System.Drawing.Point(51, 99);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(100, 23);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Loading ...";
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 249);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.picBoxTitle);
            this.Controls.Add(this.picBoxBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplash";
            this.Load += new System.EventHandler(this.frmSplash_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxBackground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTitle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picBoxBackground;
        private System.Windows.Forms.PictureBox picBoxTitle;
        private System.Windows.Forms.Label lblStatus;
    }
}