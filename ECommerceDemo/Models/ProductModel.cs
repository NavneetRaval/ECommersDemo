using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceDemo.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        [Display(Name = "Product Category")]
        public int ProdCatId { get; set; }
        [Required(ErrorMessage = "Product name field is required.")]
        [StringLength(250, ErrorMessage = "Product name field maximum length is 250 characters.")]
        public string ProdName { get; set; }
        [Required(ErrorMessage = "Name field is required.")]
        [StringLength(8000, ErrorMessage = "Name field maximum length is 8000 characters.")]
        public string ProdDescription { get; set; }

        public int ProductCategoryIds { get; set; }
        public List<SelectListItem> lstProductCategory { get; set; }
        public List<ProductAttributeModal> lstProductAttribute { get; set; }
        public ProductModel()
        {
           
            lstProductCategory = new List<SelectListItem>();
            lstProductAttribute = new List<ProductAttributeModal>();
        }
    }
}