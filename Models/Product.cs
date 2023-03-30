using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class  Product
    {
        //[Key]
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
        public DateTime CreatedDate { get; set; }
        [MaxLength(200)]
        public string? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        [MaxLength(200)]
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public string? ImageURL {get; set; }
        public bool IsDeleted { get; set; }
        public string? SubCategory { get; set; }
        public string? Gender { get; set; }
        //relatioship between the tables
        public short CategoryId { get; set; }        
        public virtual Category? Category { get; set; }//once we define  a property with a virthual and then named as category so what will happen is this product table have a categoryId as a column and which will point automatically to the category tables Id categoryId ie,it will automatically map
        public int ProductOwnerId { get; set; }
         
        public virtual ProductOwner? ProductOwner { get; set; }
        



    }
  
}