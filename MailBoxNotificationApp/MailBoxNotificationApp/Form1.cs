
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Windows.Forms;

namespace MailBoxNotificationApp
{
    public partial class Form1 : Form
    {
        readonly NotificationManager notificationManager;
        readonly NotificationAlertFactory notificationFactory;
        readonly NotifyIconManager notifyIconManager;

        string userMail;
        public Form1()
        {
            InitializeComponent();
            NotificationAPIConnectionData connectionData = new NotificationAPIConnectionData
            {
                HeadersKey = "x-api-key",
                HeadersVal = "78b06e67-bda7-48e5-a032-12132f76eca1",
                NotificationAPIUrl = "https://mini-notification-service.azurewebsites.net/notifications/"
            };
            notificationManager = new NotificationManager(connectionData);
            notifyIconManager = new NotifyIconManager(MailBoxNotifyIcon);
            notificationFactory = new NotificationAlertFactory(notifyIconManager);
        }

        public void Alert(NotificationAlert notificationAlert)
        {
            notificationAlert.ShowAlert();
        }

        private async void LogInButton_Click(object sender, EventArgs e)
        {
            try
            {
                var authorizationResult = await Program.MailBoxAzureB2CAuthorization.Login();
                if (authorizationResult != null)
                {
                    var jwt = new JwtSecurityToken(authorizationResult.IdToken);
                    notifyIconManager.NotifyIcon.BalloonTipText = "Aplication is waiting for notification from your mail box";
                    notifyIconManager.NotifyIcon.BalloonTipTitle = "MailBox Notification App";
                    WindowState = FormWindowState.Minimized;
                    ShowInTaskbar = false;
                    userMail = jwt.Claims.First(c => c.Type == "emails").Value;
                    NotificationTimer.Interval = 1;
                    NotificationTimer.Start();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch
            {
                ErrorMessageLabel.Text = "Authorization error";
            }
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            NotificationTimer.Interval = 60000;

            notificationFactory.CreateNotificationAlertsFromNotifications
                (notificationManager.GetUserNotifications(userMail)).ForEach((n) => n.ShowAlert());
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowIcon = false;
                notifyIconManager.Display();
            }
        }

        private async void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            await Program.MailBoxAzureB2CAuthorization.Logout();
            Close();
        }
    }
}
