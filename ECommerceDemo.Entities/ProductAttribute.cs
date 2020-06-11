using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDemo.Entities
{
    public class ProductAttribute
    {
        public int ProductId { get; set; }
        public int ProdCatId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }
        public string AttributeName { get; set; }
        public string CategoryName { get; set; }
        public int? TotalRowCount { get; set; }


    }
}
