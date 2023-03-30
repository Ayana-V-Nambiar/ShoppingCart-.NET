using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace ShoppingCart.ViewModel.Get
{
    public class CartViewModel 
    { 
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        // [ForeignKey("UserId")]
        // public User? User { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Range (5, 100000)]
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
        public string? SubCategory { get; set; }

        [MaxLength(200)]
        public string OwnerADObjectId { get; set; } = "Admin";

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }
    }
}