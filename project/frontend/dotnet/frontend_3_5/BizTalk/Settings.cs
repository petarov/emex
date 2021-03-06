﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;
using log4net;


namespace frontend_3_5.BizTalk
{
    class Settings
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Hashtable hashSettings = new Hashtable(5);
        private string xmlSettingsPath = string.Empty;

        public Settings(string xmlSettingsPath)
        {
            this.xmlSettingsPath = xmlSettingsPath;
            log.Info("EmEx Frontend settings file - " + xmlSettingsPath);
            this.Reload();
        }

        public void Reload()
        {
            if (xmlSettingsPath.Length > 0)
            {
                hashSettings.Clear();

                log.Info("Loading Xml settings ...");

                XmlDocument doc = new XmlDocument();
                doc.Load(this.xmlSettingsPath);

                // load settings
                hashSettings["configured"] = doc.SelectSingleNode("//configured").InnerText;
                hashSettings["backend_server"] = doc.SelectSingleNode("//backend/server").InnerText;
                hashSettings["backend_port"] = doc.SelectSingleNode("//backend/port").InnerText;
                hashSettings["backend_auth_user"] = doc.SelectSingleNode("//backend/authentication/username").InnerText;
                hashSettings["backend_auth_pass"] = doc.SelectSingleNode("//backend/authentication/password").InnerText;
                hashSettings["backend_path"] = doc.SelectSingleNode("//backend/path").InnerText;
                hashSettings["account_address"] = doc.SelectSingleNode("//account/address").InnerText;
                hashSettings["account_password"] = System.Text.Encoding.UTF8.GetString(
                    Convert.FromBase64String(doc.SelectSingleNode("//account/password").InnerText) );
            }
        }

        public void SaveAccountInfo(string account, string password)
        {
            log.Info("Saving Account Xml settings ...");

            XmlDocument doc = new XmlDocument();
            doc.Load(this.xmlSettingsPath);
            doc.SelectSingleNode("//account/address").InnerText = account;
            doc.SelectSingleNode("//account/password").InnerText = Convert.ToBase64String( 
                System.Text.Encoding.UTF8.GetBytes( password ) ); 
            doc.Save(this.xmlSettingsPath);
        }


        public void Save( Hashtable hashSettings )
        {
            log.Info("Saving Xml settings ...");

            XmlDocument doc = new XmlDocument();
            doc.Load(this.xmlSettingsPath);
            doc.SelectSingleNode("//backend/server").InnerText = Convert.ToString(hashSettings["backend_server"]);
            doc.SelectSingleNode("//backend/port").InnerText = Convert.ToString(hashSettings["backend_port"]);
            doc.SelectSingleNode("//backend/path").InnerText = Convert.ToString(hashSettings["backend_path"]);
            doc.SelectSingleNode("//backend/authentication/username").InnerText = Convert.ToString(hashSettings["backend_user"]);
            doc.SelectSingleNode("//backend/authentication/password").InnerText = Convert.ToString(hashSettings["backend_pass"]);
            //this.doc.SelectSingleNode("//account/address").Value = (string)hashSettings["backend_path"];
            doc.SelectSingleNode("//configured").InnerText = "true";
            
            doc.Save(this.xmlSettingsPath);
        }

        public string AccountAddress
        {
            get
            {
                return (string)hashSettings["account_address"]; 
            }
        }

        public string AccountPassword
        {
            get
            {
                return (string)hashSettings["account_password"]; 
            }
        }

        public bool IsConfigured
        {
            get
            {
                return hashSettings["configured"] != null && (string)hashSettings["configured"] == "true";
            }
        }

        public bool IsAccountConfigured
        {
            get
            {
                return hashSettings["account_address"] != null && ((string)hashSettings["account_address"]).Length > 0;
            }
        }

        public string this [string key]
        {
            get
            {
                object val = hashSettings[key];
                return val!=null ? (string)val : string.Empty;
            }
        }
    }
}
