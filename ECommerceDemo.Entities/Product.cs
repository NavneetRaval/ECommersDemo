using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDemo.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public int ProdCatId { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
        public string CategoryName { get; set; }
        public int? TotalRowCount { get; set; }
        public List<ProductAttribute> lstProductAttribute { get; set; }

    }
}
