using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceDemo.Models
{
    public class ProductAttributeModal
    {
        public int AttributeId { get; set; }
        [Display(Name = "Product Category")]
        [Required(ErrorMessage = "This Field Is Required")]
        public int ProdCatId { get; set; }
        public string AttributeName { get; set; }
        public string  AttributeValue { get; set; }
        public List<SelectListItem> ProductCategoryList { get; set; }
        public ProductAttributeModal()
        {

            ProductCategoryList = new List<SelectListItem>();
            
        }
    }
}