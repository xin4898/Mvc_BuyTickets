using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMvcDmeo.Models
{
    public class CCustomer
    {
        public int fId { get; set; }
        public string fName { get; set; }
        public string fPhone { get; set; }
        public string fEmail { get; set; }
        public string fAddress { get; set; }
        public string fPassword { get; set; }
    }
}