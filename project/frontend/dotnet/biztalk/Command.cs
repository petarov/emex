using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace biztalk
{
    public class Command
    {
        Session session;

        public Command(Session session)
        {
            this.session = session;
        }

        public Result GetContacts()
        {
            string res = session.request(
                Session.RequestType.RT_GET,
                "list_users",
                "email", session.UserMail
                );
            return session.JSON2Result(res);
        }
    }
}
