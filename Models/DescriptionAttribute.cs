using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EAIFMVC.Models
{
    public class DescriptionAttribute : System.Attribute
    {
        private string description;

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public DescriptionAttribute(string value)
        {
            description = value;
        }
    }
}