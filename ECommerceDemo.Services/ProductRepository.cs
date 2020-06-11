using Dapper;
using ECommerceDemo.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace ECommerceDemo.Services
{
    public class ProductRepository : CommonBaseEntity, IProductRepository
    {



        public bool Delete(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProductId", id);
                connection();
                con.Open();
                con.Execute("DeleteProductById", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddUpdateProduct(Product productModel)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProductId", productModel.ProductId);
                param.Add("@ProdName", productModel.ProdName);
                param.Add("@ProdDescription", productModel.ProdDescription);
                param.Add("@ProdCatId", productModel.ProdCatId);
                connection();
                con.Open();
                
                var product = SqlMapper.Query<Product>(con, "AddUpdateProductDetails", param, commandType: CommandType.StoredProcedure).SingleOrDefault();

                for (int i = 0; i < productModel.lstProductAttribute.Count; i++)
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@ProductId", product.ProductId);
                    para.Add("@AttributeValue", productModel.lstProductAttribute[i].AttributeValue);
                    para.Add("@AttributeId", productModel.lstProductAttribute[i].AttributeId);
                    con.Execute("AddUpdateProductAttribute", para, commandType: CommandType.StoredProcedure);
                }
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Product> GetProductInfo(string FilterText, string sortColumnName, string sortDirection, int PageIndex, int pageSize)
        {
            try
            {
                connection();
                con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@FilterText", FilterText);
                param.Add("@SortColumn", sortColumnName);
                param.Add("@SortDirection", sortDirection);
                param.Add("@PageIndex", PageIndex);
                param.Add("@PageSize", pageSize);
                IList<Product> EmpList = SqlMapper.Query<Product>(con, "GetProductData", param, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return EmpList.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product getProductDetilsById(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProductId", id);
                connection();
                con.Open();
                var product = SqlMapper.Query<Product>(con, "getProductDetilsById", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                con.Close();
                return product;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}