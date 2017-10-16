using System;
using System.Configuration;
using System.Windows.Forms;
using System.Xml;
using CMS2.Client.Properties;
using CMS2_Client;
using System.Security.Principal;
using System.Collections.Generic;
using System.Net;
using AutoUpdaterDotNET;
using System.Globalization;
using System.Reflection;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //CheckForUpdate();

            bool xBool = Convert.ToBoolean(ConfigurationManager.AppSettings["isSync"]);
            if (xBool)
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
            //TODO 1: Check the URI where the versioning xml file is located

            //TODO 2: Read the xml file. 
            //TODO 3: If version is greater than current version, download the Apcargo.msi from the location defined in versioning xml file.
            //TODO 4: Install the exe.
            //TODO 5: Reboot

            //Uncomment below line to see Russian version

            AutoUpdater.CurrentCulture = CultureInfo.CreateSpecificCulture("en-PH");

            //If you want to open download page when user click on download button uncomment below line.

            //AutoUpdater.OpenDownloadPage = true;

            //Don't want user to select remind later time in AutoUpdater notification window then uncomment 3 lines below so default remind later time will be set to 2 days.

            //AutoUpdater.LetUserSelectRemindLater = false;
            //AutoUpdater.RemindLaterTimeSpan = RemindLaterFormat.Days;
            //AutoUpdater.RemindLaterAt = 2;
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string ss = Application.ProductVersion.ToString();

            AutoUpdater.Start("http://localhost:53002/api/installerupdate/getappcast");
            
        }
    }
}
