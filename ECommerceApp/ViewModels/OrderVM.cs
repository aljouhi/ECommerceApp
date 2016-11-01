﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.ViewModels
{
    public class OrderVM
    {
        public int OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShipDate { get; set; }

        public int BookID { get; set; }

        public int Quantity { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }
    }
}