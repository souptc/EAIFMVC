using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAIFMVC.Models
{
    public enum AlertStatus
    {
        [Description("未处理")]
        Unhandled = 0,
        [Description("误报")]
        Incorrect = 1,
        [Description("已确认正在处理")]
        Processing = 2,
        [Description("处理结束")]
        Complete = 3
    }

    public class AlertWrapper
    {
        public Guid ID { get; set; }

        public Guid CompanyID { get; set; }

        public DateTime AlertTime { get; set; }

        public string DangerSource { get; set; }

        public AlertStatus Status { get; set; }

        public string StatusDescription { get; set; }

        public string CompanyName { get; set; }

        public string Phone { get; set; }
    }
}