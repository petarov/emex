using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using biztalk;
using log4net;

namespace frontend_3_5.BizTalk
{
    class Talker
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Settings settings;
        private Session session;

        public Talker(Settings settings)
        {
            // connect to Backend
            this.settings = settings;
            this.session = new Session(
                settings["backend_server"],
                Convert.ToInt32( settings["backend_port"] ),
                settings["account_address"]
                );
        }

        public Result RegisterUser( Hashtable hashAccountInfo )
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "register_user",
                hashAccountInfo);
            return this.session.JSON2Result(json);
        }

        public Result BuildMbox()
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "build_mbox",
                "email", this.session.UserMail );
            return this.session.JSON2Result(json);
        }

        public Result EditContact(Hashtable hashInfo )
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "edit_contact",
                hashInfo);
            return this.session.JSON2Result(json);
        }

        public Result ListContacts()
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "list_users",
                "email", session.UserMail);
            return this.session.JSON2Result(json);
        }

        public Result ListMails()
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "list_mails",
                "email", session.UserMail);
            return this.session.JSON2Result(json);
        }

    }
}
