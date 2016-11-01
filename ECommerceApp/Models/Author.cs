namespace ECommerceApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public int AuthorID { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        
       
        public virtual ICollection<Book> Books { get; set; }
    }
}
