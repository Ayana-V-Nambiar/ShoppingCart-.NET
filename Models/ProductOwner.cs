using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ShoppingCart.Models
{
    public class ProductOwner
    {
        public int Id { get; set; }
        [MaxLength(200)]
        public string? OwnerADObjectID { get; set; }
        [MaxLength(1000)]
        public string? OwnerName { get; set; }
    }
}