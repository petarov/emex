using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using frontend_3_5.Utils;
using frontend_3_5.BizTalk;
using frontend_3_5.Proc;
using biztalk;

namespace frontend_3_5
{
    public partial class frmContactInfo : Form
    {
        private Hashtable hashUserInfo;

        public frmContactInfo( Hashtable hashUserInfo )
        {
            InitializeComponent();

            this.hashUserInfo = hashUserInfo;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // save to BACKEND
            try
            {
                Hashtable hashRes = new Hashtable(12);
                hashRes["email"] = Bootstrap.Instance().Settings.AccountAddress; // ME
                hashRes["id"] = hashUserInfo["id"];
                hashRes["other_email"] = txtOtherEMail.Text.Trim();
                hashRes["fullname"] = txtFullName.Text.Trim();
                hashRes["nickname"] = txtAlias.Text.Trim();
                //TODO: birthday
                hashRes["tel_home"] = txtPhoneHome.Text.Trim();
                hashRes["tel_work"] = txtPhoneWork.Text.Trim();
                hashRes["tel_fax"] = txtFax.Text.Trim();
                hashRes["tel_pager"] = txtPager.Text.Trim();
                hashRes["tel_mobile"] = txtMobile.Text.Trim();
                hashRes["AIM"] = txtAIM.Text.Trim();

                Result res = Bootstrap.Instance().Talker.EditContact(hashRes);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                new ErrorHandler(ex).Error();
            }
        }

        private void frmContactInfo_Load(object sender, EventArgs e)
        {
            this.Text = "Contact Visit Card";
            txtEmail.Text = (string)hashUserInfo["email"];
            txtOtherEMail.Text = (string)hashUserInfo["other_email"];
            txtFullName.Text = (string)hashUserInfo["fullname"];
            txtAlias.Text = (string)hashUserInfo["nickname"];
            //TODO: birthday
            txtPhoneHome.Text = (string)hashUserInfo["tel_home"];
            txtPhoneWork.Text = (string)hashUserInfo["tel_work"];
            txtFax.Text = (string)hashUserInfo["tel_fax"];
            txtPager.Text = (string)hashUserInfo["tel_pager"];
            txtMobile.Text = (string)hashUserInfo["tel_mobile"];
            txtAIM.Text = (string)hashUserInfo["AIM"];

        }
    }
}
