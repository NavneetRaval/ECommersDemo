using ECommerceDemo.Entities;
using ECommerceDemo.Models;
using ECommerceDemo.Services;
using ECommerceDemo.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceDemo.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository productRepository;
        private IProductCategoriesRepository productCategoriesRepository;
        public ProductController(IProductRepository _productRepository, IProductCategoriesRepository _productCategoriesRepository)
        {
            productRepository = _productRepository;
            productCategoriesRepository = _productCategoriesRepository;

        }
        // GET: Product
        /// <summary>
        /// Get Product Summary Screen View call
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get Product Summary Screen  to call ajax call return product information
        /// </summary>

        [HttpGet]
        public ActionResult GetProducts(JqueryDatatableParam param)
        {
            var filterText = !string.IsNullOrEmpty(param.sSearch) ? param.sSearch.ToLower() : null;
            var currentPage = Convert.ToInt32(param.sEcho);
            var pageSize = param.iDisplayLength;
            var sortDirection = Request.QueryString["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request.QueryString["iSortCol_0"]);
            var sortColumnName = sortColumnIndex == 0
                ? "ProductId"
                : (sortColumnIndex == 1
                ? "ProdName"
                : (sortColumnIndex == 2
                ? "CategoryName"
                : (sortColumnIndex == 3
                ? "ProdDescription"
                : "")));
            currentPage = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1;

            var productData = productRepository.GetProductInfo(filterText, sortColumnName, sortDirection, currentPage, pageSize);
            var resultSet = productData
                .Select(x => new String[] {
                    Convert.ToString(x.ProductId),
                   x.ProdName,
                    x.CategoryName,
                    x.ProdDescription,
                    Convert.ToString(x.ProdCatId),
                    Convert.ToString(x.TotalRowCount)

                }).ToList();
            int TotalRecords = 0;
            if (productData != null&& productData.Count>0)
            {
                TotalRecords = productData[0].TotalRowCount.Value;
            }
            return Json(new
            {
                param.sEcho,
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalRecords,
                aaData = resultSet
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Save Product information 
        /// </summary>

        [HttpPost]
        public ActionResult SaveProduct(ProductModel model)
        {
            List<string> messages = new List<string>();
            bool responseStatus = true;

            if (!ModelState.IsValid)
            {
                responseStatus = false;
                messages = ModelState.Values
                    .SelectMany(x => x.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToList();
               
            }
            try
            {
                var productData = productRepository.GetProductInfo("", "", "", 1, int.MaxValue);

                if (responseStatus && model.ProductId > 0 && productRepository.getProductDetilsById(model.ProductId) != null)
                {
                    InsertUpdateProduct(model);
                    responseStatus = true;
                    messages.Add("Product data inserted successfully.");
                }
                else if (responseStatus)
                {
                    InsertUpdateProduct(model);

                    responseStatus = true;
                    messages.Add("Product data updated successfully.");
                }

            }
            catch (Exception ex)
            {
                responseStatus = false;
                messages.Add("something went wrong, please contact your administrator." + ex.Message);
            }


            return Json(new { messages, responseStatus }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Delete Product information 
        /// </summary>
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var result = productRepository.Delete(id);


            return View("Product");
        }
        /// <summary>
        /// Call Product partial view for pop up add/update product information 
        /// </summary>
        [HttpGet]
        public ActionResult ProductFormPartialView(int? id)
        {
            if (id == null)
                id = 0;
            var modeldata = productRepository.getProductDetilsById(id.Value);
            if (modeldata == null) {
                ProductModel objresult = new ProductModel();
                BindProductModel(objresult);
                return View(objresult);
            }
            var result = new ProductModel()
            {
                ProductId = modeldata.ProductId,
                ProdDescription = modeldata.ProdDescription,
                ProdName = modeldata.ProdName,
                ProdCatId = modeldata.ProdCatId

            };
            result = result == null ? new ProductModel() : result;
            BindProductModel(result);
            BindProductAttributeModel(result);
            return View(result);
        }
        /// <summary>
        /// Call no action method to call save product
        /// </summary>
        [NonAction]
        private void InsertUpdateProduct(ProductModel model)
        {
            try
            {
                Product product = new Product();
                product.ProdCatId = model.ProdCatId;
                product.ProdDescription = model.ProdDescription;
                product.ProdName = model.ProdName;
                product.ProductId = model.ProductId;
                product.lstProductAttribute = model.lstProductAttribute.Select(x => new ProductAttribute()
                {
                    AttributeId = x.AttributeId,
                    AttributeValue = x.AttributeValue,
                    AttributeName = x.AttributeName,
                    ProdCatId = x.ProdCatId,
                }).ToList();
                var result = productRepository.AddUpdateProduct(product);
            }
            catch (Exception ex)
            { throw ex; }


        }

        [NonAction]
        private void BindProductAttributeModel(ProductModel model)
        {
            ProductModel productModel = new ProductModel();
            var modeldata = productCategoriesRepository.getProductAttributeLookupById(model.ProdCatId, model.ProductId);
            var Result = modeldata.Select(x => new ProductAttributeModal()
            {
                AttributeId = x.AttributeId,
                AttributeValue = x.AttributeValue,
                AttributeName = x.AttributeName,
                ProdCatId = x.ProdCatId,
            }).ToList();
            model.lstProductAttribute = Result;



        }
        [NonAction]
        private void BindProductModel(ProductModel model)
        {
            model.lstProductCategory = productCategoriesRepository.GetProductCategoryInfo(null, null, null, 1, int.MaxValue)
                  .Select(x => new SelectListItem()
                  {
                      Text = x.CategoryName,
                      Value = x.ProdCatId.ToString()
                  }).ToList();

            model.lstProductCategory.Insert(0, new SelectListItem() { Text = "Select", Value = "" });

        }

        [HttpGet]
        public ActionResult GetProductAttributeViewByCategoryId(int id)
        {
            ProductModel productModel = new ProductModel();
            var modeldata = productCategoriesRepository.getProductAttributeLookupById(id,0);
            var Result= modeldata.Select(x => new ProductAttributeModal()
            {
                AttributeId = x.AttributeId,
                AttributeValue = x.AttributeValue,
                AttributeName = x.AttributeName,
                ProdCatId = x.ProdCatId,
            }).ToList();
            productModel.lstProductAttribute = Result;
            return View(productModel);
        }

    }
}