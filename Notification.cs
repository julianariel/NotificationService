using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge
{
    public class Notification
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
    }

    public class NotificationType
    {
        public string Name { get; set; }

        public NotificationType(string Name)
        { 
            this.Name = Name;
        }


    }

    public class RateLimitedNotificationType : NotificationType {

        
        public RateLimitedNotificationType(string Name) : base(Name) 
        {
            RateLimitRules = new List<RateLimitRule>();
        }
        public List<RateLimitRule> RateLimitRules { get; set; } 
    }


    public class NotificationLog
    {

        public NotificationType Type { get; set; }
        public DateTime Date { get; set; }
    }

}
