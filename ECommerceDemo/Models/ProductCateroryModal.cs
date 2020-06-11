using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerceDemo.Models
{
    public class ProductCateroryModal
    {
        public int ProdCatId { get; set; }
        [Display(Name = "Product Category")]
       
        [Required(ErrorMessage = "Product Category field is required.")]
        [StringLength(250, ErrorMessage = "Product Category field maximum length is 250 characters.")]
        public string CategoryName { get; set; }
        
    }
}