namespace ECommerceApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderID { get; set; }

        [Required]
        [StringLength(50)]
        public string CustomerName { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShipDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
