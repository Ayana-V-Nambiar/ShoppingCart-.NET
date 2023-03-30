using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Models
{
    public class Category
    {
        public short Id { get; set; }
        [Required]
        [MinLength(5), MaxLength(500)]
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDelete { get; set; }
    }
}