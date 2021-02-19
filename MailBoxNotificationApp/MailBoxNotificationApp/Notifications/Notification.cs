
using System.Collections.Generic;

namespace MailBoxNotificationApp
{
    class NotificationId
    {
        public int Id { get; set; }
    }

    class NotificationRecipientsList
    {
        public List<string> RecipientsList { get; set; }
    }

    class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public List<string> RecipientsList { get; set; }
    }

    public class NewNotification
    {
        public string Content { get; set; }
        public string ContentType { get; set; } = "string";
        public string[] RecipientsList { get; set; }
        public bool WithAttachments { get; set; }
    }
}
