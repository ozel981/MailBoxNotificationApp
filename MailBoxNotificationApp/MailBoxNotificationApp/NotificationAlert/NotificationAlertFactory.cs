
using System.Collections.Generic;
using System.Drawing;

namespace MailBoxNotificationApp
{
    class NotificationAlertFactory
    {
        readonly NotifyIconManager notifyIconManager;

        public NotificationAlertFactory(NotifyIconManager notifyIconManager)
        {
            this.notifyIconManager = notifyIconManager;
        }

        public NotificationAlert CreateNewMailNotification(int unreadMailsCount)
        {
            string message;
            if (unreadMailsCount == 1)
            { message = $"Masz 1 nieprzeczytaną wiadomość"; }
            else if (unreadMailsCount > 1 && unreadMailsCount < 5)
            { message = $"Masz {unreadMailsCount} nieprzeczytane wiadomości"; }
            else
            { message = $"Masz {unreadMailsCount} nieprzeczytanych wiadomości"; }
            return new NotificationAlert(message, notifyIconManager);
        }

        public NotificationAlert CreateActivatedAccountNotification()
        {
            return new NotificationAlert("Twoje ktonto zostało już aktywowane", Color.DarkGreen, notifyIconManager);
        }

        public NotificationAlert CreateBannedAccountNotification()
        {
            return new NotificationAlert("Twoje ktonto zostało zbanowane", Color.DarkRed, notifyIconManager);
        }

        public List<NotificationAlert> CreateNotificationAlertsFromNotifications(List<Notification> notifications)
        {
            List<NotificationAlert> notificationAlerts = new List<NotificationAlert>();
            int newMailCount = 0;
            foreach (Notification notification in notifications)
            {
                switch (notification.Content)
                {
                    case "NewMail":
                        {
                            newMailCount++;
                        }
                        break;
                    case "ActivatedAccount":
                        {
                            notificationAlerts.Add(CreateActivatedAccountNotification());
                        }
                        break;
                    case "BannedAccount":
                        {
                            notificationAlerts.Add(CreateBannedAccountNotification());
                        }
                        break;
                }
            }
            if (newMailCount > 0)
            {
                notificationAlerts.Add(CreateNewMailNotification(newMailCount));
            }
            return notificationAlerts;
        }
    }
}
