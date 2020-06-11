using Dapper;
using ECommerceDemo.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace ECommerceDemo.Services
{
    public interface IProductRepository
    {
        List<Product> GetProductInfo(string FilterText, string sortColumnName, string sortDirection, int PageIndex, int pageSize);
        bool AddUpdateProduct(Product productModel);
        bool Delete(int id);
        Product getProductDetilsById(int id);
    }
}