
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MailBoxNotificationApp
{
    class NotificationManager
    {
        private readonly string HeadersKey;
        private readonly string HeadersVal;
        private readonly string NotificationAPIUrl;
        public NotificationManager(NotificationAPIConnectionData connectionData)
        {
            HeadersKey = connectionData.HeadersKey;
            HeadersVal = connectionData.HeadersVal;
            NotificationAPIUrl = connectionData.NotificationAPIUrl;
        }

        public List<Notification> GetUserNotifications(string userEmail)
        {
            List<Notification> notifications = new List<Notification>();
            try
            {
                Task<List<int>> NotificationsIds = Task.Run(GetNotificationIds);
                NotificationsIds.Wait();

                notifications = GetUserNotifications(NotificationsIds.Result, userEmail);

                Task DeleteUserNotfication = Task.Run(() => DeleteUserNotifications(notifications, userEmail));
                DeleteUserNotfication.Wait();
            }
            catch (Exception)
            {
                // Wait for connection...
            }
            return notifications;
        }

        private async Task<List<int>> GetNotificationIds()
        {
            List<int> ids = new List<int>();
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HeadersKey, HeadersVal);
                string responseString = await
                    client.GetStringAsync(NotificationAPIUrl);

                responseString = GetNotificationsFromResponse(responseString);

                foreach (var n in JsonConvert.DeserializeObject<List<NotificationId>>(responseString))
                {
                    ids.Add(n.Id);
                }
            }
            return ids;
        }

        private List<Notification> GetUserNotifications(List<int> notificationIds, string userEmail)
        {
            List<Notification> notifications = new List<Notification>();
            foreach (int Id in notificationIds)
            {
                Task<Notification> EmailsList = Task.Run(() => GetNotification(Id));
                EmailsList.Wait();

                if (EmailsList.Result.RecipientsList.Contains(userEmail))
                {
                    notifications.Add(EmailsList.Result);
                }
            }
            return notifications;
        }

        private async Task<Notification> GetNotification(int Id)
        {
            Notification notification;
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HeadersKey, HeadersVal);
                string responseString = await
                    client.GetStringAsync(NotificationAPIUrl + $"{Id}");

                notification = JsonConvert.DeserializeObject<Notification>(responseString);
            }
            notification.Id = Id;
            return notification;
        }

        private async void DeleteUserNotifications(List<Notification> notifications, string userEmail)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add(HeadersKey, HeadersVal);
                foreach (Notification notification in notifications)
                {
                    await client.DeleteAsync(NotificationAPIUrl + $"{notification.Id}");
                    List<string> emails = new List<string>(notification.RecipientsList);
                    emails.Remove(userEmail);
                    if (emails.Count > 0)
                    {
                        NewNotification newNotification = new NewNotification
                        {
                            Content = "test",
                            RecipientsList = emails.ToArray(),
                            WithAttachments = false
                        };
                        string json = await Task.Run(() => JsonConvert.SerializeObject(newNotification));
                        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                        var response = await client.PostAsync(NotificationAPIUrl, content);

                        var responseString = await response.Content.ReadAsStringAsync();
                    }
                }
            }
        }

        private string GetNotificationsFromResponse(string response)
        {
            return "[" + response.Split('[', ']')[1] + "]";
        }
    }
}
