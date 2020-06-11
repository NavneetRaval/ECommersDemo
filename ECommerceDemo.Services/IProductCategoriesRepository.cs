using ECommerceDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDemo.Services
{
    public interface IProductCategoriesRepository
    {
        List<ProductCategory> GetProductCategoryInfo(string FilterText, string sortColumnName, string sortDirection, int PageIndex, int pageSize);
        List<ProductAttribute> GetProductAttributeInfo(string FilterText, string sortColumnName, string sortDirection, int PageIndex, int pageSize);
        bool AddUpdateProductCategory(ProductCategory productModel);
        bool Delete(int id);
        ProductCategory getProductCategoryDetilsById(int id);
        List<ProductAttribute> getProductAttributeLookupById(int ProdCatId, int ProductId);
        ProductAttribute getProductAttributeById(int id);
        bool DeleteProductAttribute(int id);
        bool InsertUpdateProductAttribute(ProductAttribute productModel);
    }
}
