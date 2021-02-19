
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MailBoxNotificationApp
{
    public class NotifyIconManager
    {
        public NotifyIcon NotifyIcon { get; private set; }
        private readonly List<string> notifications = new List<string>();
        public NotifyIconManager(NotifyIcon notifyIcon)
        {
            NotifyIcon = notifyIcon;
        }

        public void Display()
        {
            NotifyIcon.Visible = true;
            NotifyIcon.ShowBalloonTip(1000);
        }

        public void AddNotify(string message)
        {
            NotifyIcon.Icon = new Icon("../../Icons/letterNotify.ico");
            notifications.Add(message);
            ToolStripButton notify = new ToolStripButton("Notify", null, RemoveNotify, message)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                ForeColor = Color.White
            };
            NotifyIcon.ContextMenuStrip.Items.Add(notify);
        }

        private void RemoveNotify(object sender, System.EventArgs e)
        {
            NotifyIcon.BalloonTipText = ((ToolStripButton)sender).Name;
            NotifyIcon.BalloonTipTitle = "MailBox Notification";
            NotifyIcon.ShowBalloonTip(1000);
            NotifyIcon.ContextMenuStrip.Items.Remove((ToolStripButton)sender);
            notifications.Remove(((ToolStripButton)sender).Name);
            if (notifications.Count == 0)
            {
                NotifyIcon.Icon = new Icon("../../Icons/letter.ico");
            }
        }
    }
}
