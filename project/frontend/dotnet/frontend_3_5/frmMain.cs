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

namespace frontend_3_5
{
    public partial class frmMain : Form
    {
        private BizTalk.Settings settings;
        private BizTalk.Talker talker;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {


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
            frmAccountWiz1 wiz1 = new frmAccountWiz1();
            wiz1.ShowDialog(this);
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO:
            settings = new Settings("emex-options.xml");
            talker = new Talker(this.settings);

            Result res = this.talker.GetContacts();
            foreach (Hashtable t in res.return_)
            {
                this.listView1.Items.Add(new ListViewItem( (string)t["email"] ));
            }
        }
    }
}
