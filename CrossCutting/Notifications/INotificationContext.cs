using System.Collections.Generic;

namespace CrossCutting.Notifications
{
    public interface INotificationContext
    {
        IEnumerable<Notification> Notifications { get; }
        bool HasNotifications { get; }
        void AddNotifications(IEnumerable<string> messages);
    }
}