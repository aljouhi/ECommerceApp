namespace ECommerceApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }

        public int CategoryID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
