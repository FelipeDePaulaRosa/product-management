using System.Linq;
using System.Collections.Generic;

namespace CrossCutting.Notifications
{
    public class NotificationContext: INotificationContext
    {
        public IEnumerable<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Count != 0;
        private readonly List<Notification> _notifications = new();
        
        public void AddNotifications(IEnumerable<string> messages)
        {
            var notifications = messages.Select(x => new Notification(x));
            _notifications.AddRange(notifications);
        }
    }
}