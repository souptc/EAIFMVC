using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAIFMVC.Models
{
    public enum CompanyCategory
    {
        Type1 = 1,
        Type2 = 2
    }
    public enum DangerCategory
    {
        Cat1 = 1,
        Cat2 = 2
    }

    public class CompanyWrapper
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string LawPersion { get; set; }
        public CompanyCategory Category { get; set; }
        public double Size { get; set; }
        public int ContactorID { get; set; }
        public string Phone { get; set; }
        public double Storage { get; set; }
        public string ContactorName { get; set; }

        public DangerCategory DangerCategory { get; set; }

    }
}