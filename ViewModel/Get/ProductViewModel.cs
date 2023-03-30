using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
// using ShoppingCart.Validation;

namespace ShoppingCart.ViewModel.Get
{
    public class ProductViewModel  // : AbstractValidatableObject
    {
        // [Key]
        public int ProductId { get; set; }
        [Required]
        [MinLength(5), MaxLength(500)]
        public string? Name { get; set; }
        [Required]
        [MinLength(5), MaxLength(500)]
        public string? Description { get; set; }
        [Range (5, 9000)]
        public decimal Price { get; set; }
        public DateTime AvailableSince { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string? ImageURL { get; set; }

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
        public string? SubCategory { get; set; }
        public string? Gender { get; set; }
        public short CategoryId { get; set; }
        public int ProductOwnerId {get; set;}
        
        public DateTime ModifiedDate { get; set; }
    }
}