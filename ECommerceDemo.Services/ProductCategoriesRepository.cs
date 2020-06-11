using Dapper;
using ECommerceDemo.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ECommerceDemo.Services
{
   public class ProductCategoriesRepository : CommonBaseEntity, IProductCategoriesRepository
    {
        
        public bool AddUpdateProductCategory(ProductCategory productModel)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProdCatId", productModel.ProdCatId);
                param.Add("@CategoryName", productModel.CategoryName);
                connection();
                con.Open();
                con.Execute("AddupdateCategoriesdetails", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProductId", id);
                connection();
                con.Open();
                con.Execute("DeleteproductCategorybyid", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteProductAttribute(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AttributeId", id);
                connection();
                con.Open();
                con.Execute("DeleteProductAttributebyid", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ProductCategory getProductCategoryDetilsById(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProdCatId", id);
                connection();
                con.Open();
                var ProductCategory = SqlMapper.Query<ProductCategory>(con, "getProductCategoryDetilsById", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                con.Close();
                return ProductCategory;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<ProductAttribute> getProductAttributeLookupById(int ProdCatId,int ProductId)
        {
            try
            {
                connection();
                con.Open();
                DynamicParameters param = new DynamicParameters();
                param.Add("@ProdCatId", ProdCatId);
                param.Add("@ProductId", ProductId);
                IList<ProductAttribute> lst = SqlMapper.Query<ProductAttribute>(con, "getProductAttributeLookupById", param, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ProductCategory> GetProductCategoryInfo(string FilterText, string sortColumnName, string sortDirection, int PageIndex, int pageSize)
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

                IList<ProductCategory> lst = SqlMapper.Query<ProductCategory>(con, "GetProductCategoriesData", param, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductAttribute> GetProductAttributeInfo(string FilterText, string sortColumnName, string sortDirection, int PageIndex, int pageSize)
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

                IList<ProductAttribute> lst = SqlMapper.Query<ProductAttribute>(con, "GetProductAttributeInfo", param, commandType: CommandType.StoredProcedure).ToList();
                con.Close();
                return lst.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductAttribute getProductAttributeById(int id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AttributeId", id);
                connection();
                con.Open();
                var ProductCategory = SqlMapper.Query<ProductAttribute>(con, "getProductAttributeById", param, commandType: CommandType.StoredProcedure).SingleOrDefault();
                con.Close();
                return ProductCategory;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool InsertUpdateProductAttribute(ProductAttribute productModel)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@AttributeId", productModel.AttributeId);
                param.Add("@AttributeName", productModel.AttributeName);
                param.Add("@ProdCatId", productModel.ProdCatId);
                connection();
                con.Open();
                con.Execute("AddUpdateProductAttributeLookup", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
