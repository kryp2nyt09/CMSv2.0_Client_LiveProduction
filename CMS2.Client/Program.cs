using System;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using CMS2.Client.Properties;
using CMS2_Client;
using System.Security.Principal;
using System.Collections.Generic;
using System.Net;

namespace CMS2.Client
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            CheckForUpdate();

            bool xBool = Convert.ToBoolean(ConfigurationManager.AppSettings["isSync"]);
            if (!xBool)
            {
                Extract_Database extract = new Extract_Database();
                Application.Run(extract);
                Application.Exit();
            }
            else
            {
                var cmsMainWindow = new Main();
                cmsMainWindow.Height = 755;
                cmsMainWindow.Width = 1266;
                Application.Run(cmsMainWindow);
            }

        }

        private static void CheckForUpdate()
        {
            //using (System.Net.HttpClient hh = new System.Net.HttpClient())
            //{
                
            //}
        }
    }
}
