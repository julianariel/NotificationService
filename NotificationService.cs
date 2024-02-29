using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge
{
    public class NotificationService : INotificationService
    {
        // Gateway. Sends the messages.
        private Gateway _gateway;

        // Stores notification types with the ratelimit rules.
        private List<RateLimitedNotificationType> _notificationTypes;

        // Dictionary to store every notification per user. We only need the type and date.
        private Dictionary<string, List<NotificationLog>> _notificationLog;

        public NotificationService(Gateway gateway)
        {
            _gateway = gateway;

            // Notification types with their limits
            _notificationTypes = new List<RateLimitedNotificationType>()
            {
                new RateLimitedNotificationType("Status")
                {
                    RateLimitRules = new List<RateLimitRule>
                    {
                        // not more than 2 per minute for each recipient
                        new RateLimitRule { NotificationsCount = 2, TimeSpan = TimeSpan.FromMinutes(1) }
                    }
                },
                new RateLimitedNotificationType("News")
                {
                    RateLimitRules = new List<RateLimitRule>
                    {
                        // not more than 1 per day for each recipient
                        new RateLimitRule { NotificationsCount = 1, TimeSpan = TimeSpan.FromDays(1) }
                    }
                },
                new RateLimitedNotificationType("Marketing")
                {
                    RateLimitRules = new List<RateLimitRule>
                    {
                        // not more than 3 per hour for each recipient
                        new RateLimitRule { NotificationsCount = 3, TimeSpan = TimeSpan.FromHours(1) }
                    }
                }
                // Can add more NotificationTypes with their respective RateLimitRules as needed
            };

            _notificationLog = new Dictionary<string, List<NotificationLog>>();
        }


        public void Send(string type, string userId, string message)
        {
            var notificationType = _notificationTypes.FirstOrDefault(t => t.Name.Equals(type, StringComparison.OrdinalIgnoreCase));

            // Invalid Notification Type.
            if (notificationType == null)
            {
                Console.WriteLine($"Error: Notification type '{type}' not found.");
                return;
            }

            // Checks rate limit rules
            if (IsRateLimitExceeded(notificationType, userId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Rate limit exceeded for notification type '{type}' for user '{userId}'.");
                Console.ResetColor(); // Reset color to default
                return;
            }

            // Sends message
            _gateway.Send(userId, message);

            // Updates log
            _updateNotificationLog(userId, notificationType);
        }



        private bool IsRateLimitExceeded(RateLimitedNotificationType notificationType, string userId)
        {
            if (!_notificationLog.ContainsKey(userId))
                return false;

            var userNotificationLogs = _notificationLog[userId];

            // Check for every rule
            foreach (var rule in notificationType.RateLimitRules)
            {
                // Calculate the start time based on the time span rule
                var startTime = DateTime.Now - rule.TimeSpan;

                // Count the notifications within the specified time span
                var notificationsWithinTimeSpan = userNotificationLogs
                    .Count(log => log.Type.Name == notificationType.Name && log.Date >= startTime);

                // Compare the count with the maximum notifications count allowed for this type/rule
                if (notificationsWithinTimeSpan >= rule.NotificationsCount)
                    return true; // rate limited
            }

            return false; // good to go
        }

        // Store every notification sent to an user. We only need the type and date.

        private void _updateNotificationLog(string userId, NotificationType notificationType)
        {
            if (!_notificationLog.ContainsKey(userId))
            {
                _notificationLog[userId] = new List<NotificationLog>();
            }

            _notificationLog[userId].Add(new NotificationLog
            {
                Type = notificationType,
                Date = DateTime.Now
            });
        }


    }
}
