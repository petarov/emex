﻿using System;
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
                settings["backend_auth_user"],
                settings["backend_auth_pass"],
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
                "email", this.settings.AccountAddress,
                "pass", this.settings.AccountPassword );
            return this.session.JSON2Result(json);
        }

        public Result EditContact(Hashtable hashInfo)
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
                "email", settings.AccountAddress);
            return this.session.JSON2Result(json);
        }

        public Result ListMails()
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "list_mails",
                "email", settings.AccountAddress);
            return this.session.JSON2Result(json);
        }

        public Result ListMailsBySearchTag(string tagID)
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "list_mails_by_searchtag",
                "email", settings.AccountAddress,
                "tagid", tagID );
            return this.session.JSON2Result(json);
        }

        public Result GetMail(string mailID)
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "get_email",
                "email", settings.AccountAddress,
                "id", mailID,
                "pass", this.settings.AccountPassword);
            return this.session.JSON2Result(json);
        }

        public Result SendMail(string To, string Cc, string Bcc, string Subject, string Body, string smtpPassword)
        {
            string json = this.session.request(
                Session.RequestType.RT_POST,
                "send_email",
                "email", settings.AccountAddress,
                "to", To,
                "cc", Cc,
                "bcc", Bcc,
                "subject", Subject,
                "body", Body,
                "pass", smtpPassword);
            return this.session.JSON2Result(json);
        }

        public Result ListContactMails(string contactId)
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "list_contact_mails",
                "email", settings.AccountAddress,
                "id", contactId );
            return this.session.JSON2Result(json);
        }

        public Result ListAttacments()
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "list_attachments",
                "email", settings.AccountAddress);
            return this.session.JSON2Result(json);
        }

        public Result ListContactAttachments(string contactId)
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "list_contact_attachments",
                "email", settings.AccountAddress,
                "id", contactId);
            return this.session.JSON2Result(json);
        }

        public Result GetAttachment(string id)
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "get_attachment",
                "email", settings.AccountAddress,
                "id", id,
                "pass", this.settings.AccountPassword
                );
            return this.session.JSON2Result(json);
        }

        public Result ListTags()
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "list_tags",
                "email", settings.AccountAddress);
            return this.session.JSON2Result(json);
        }

        public Result SearchEMail(string word)
        {
            string json = this.session.request(
                Session.RequestType.RT_GET,
                "search_email",
                "email", settings.AccountAddress,
                "word", word
                );
            return this.session.JSON2Result(json);
        }

    }
}
