
using System;
using System.Windows.Forms;

namespace MailBoxNotificationApp
{
    static class Program
    {
        public static MailBoxAzureB2CAuthorization MailBoxAzureB2CAuthorization { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            MailBoxAzureB2CAuthorization = new MailBoxAzureB2CAuthorization();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}