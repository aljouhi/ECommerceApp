using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApp.ViewModels
{
    public class BookVM
    {
        public int BookID { get; set; }

        [Required]
        public int AuthorID { get; set; }
        [Required]
        public int CategoryID { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Column(TypeName = "date")]
        [DisplayName("Publication Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? PublicationDate { get; set; }

        [StringLength(50)]
        public string ISBN { get; set; }

        [StringLength(50)]

        [DisplayName("Cover Image")]
        public string CoverImage { get; set; }

        [Column(TypeName = "money")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [StringLength(50)]
        [Required]
        public string Publisher { get; set; }

        [Required(ErrorMessage = "First Name Required")]
        [StringLength(50)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Required")]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
    }
}