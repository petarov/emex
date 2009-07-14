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
            return this.session.JSON2Result(
                this.session.request(
                        Session.RequestType.RT_GET,
                        "register_user",
                        hashAccountInfo
                        )
                    );
        }

        public Result GetContacts()
        {
            Command cmd = new Command(this.session);
            return cmd.GetContacts();
        }

    }
}
