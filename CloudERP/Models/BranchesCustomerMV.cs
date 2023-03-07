using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudERP.Models
{
    public class BranchesCustomerMV
    {
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string CustomerName { get; set; }
        public string CustomerContact { get; set; }
        public string CustomerArea { get; set; }
        public string CustomerAddress { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
    }
}