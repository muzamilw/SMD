using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMD.MIS.Areas.Api.Models
{
    public class Product
    {
        public int ProductId { get; set; }
       
        public string ProductCode { get; set; }

        public Nullable<bool> IsActive { get; set; }
        public Nullable<double> SetupPrice { get; set; }
       
    }
}