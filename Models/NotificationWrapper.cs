using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAIFMVC.Models
{
    public enum NotificationType
    {
        Alert = 1,
        DeviceBug = 2,
        User = 3
    }

    public enum NotficationStatus
    {
        UnRead = 1,
        Read = 2
    }
    public class NotificationWrapper
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public NotificationType Type { get; set; }
        public string Text { get; set; }
        public NotficationStatus Status { get; set; } 
    }
}