using ECommerceDemo.Entities;
using ECommerceDemo.Models;
using ECommerceDemo.Services;
using ECommerceDemo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerceDemo.Controllers
{
    public class ProductAttributeController : Controller
    {
        // GET: ProductAttribute
        // GET: ProductAttribute
        private IProductRepository productRepository;
        private IProductCategoriesRepository productCategoriesRepository;
        public ProductAttributeController(IProductRepository _productRepository, IProductCategoriesRepository _productCategoriesRepository)
        {
            productRepository = _productRepository;
            productCategoriesRepository = _productCategoriesRepository;

        }
        // GET: ProductAttribute
        /// <summary>
        /// Get ProductAttribute Summary Screen View call
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get Product Summary Screen  to call ajax call return ProductAttribute information
        /// </summary>

        [HttpGet]
        public ActionResult GetProductAttribute(JqueryDatatableParam param)
        {
            var filterText = !string.IsNullOrEmpty(param.sSearch) ? param.sSearch.ToLower() : null;
           
            var currentPage = Convert.ToInt32(param.sEcho);
            var pageSize = param.iDisplayLength;
            var sortDirection = Request.QueryString["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request.QueryString["iSortCol_0"]);
            var sortColumnName = sortColumnIndex == 0
                ? "AttributeId"
                : (sortColumnIndex == 1
                ? "CategoryName"
                : (sortColumnIndex == 2
                ? "AttributeName"
                : (sortColumnIndex == 3
                ? ""
                : "")));
            currentPage = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1;
            var productData = productCategoriesRepository.GetProductAttributeInfo(filterText, sortColumnName, sortDirection, currentPage, pageSize);
            var resultSet = productData
                .Select(x => new String[] {
                    Convert.ToString(x.AttributeId),
                    x.AttributeName,
                    x.CategoryName,
                    Convert.ToString(x.TotalRowCount)

                }).ToList();
            int TotalRecords = 0;
            if (productData != null && productData.Count > 0)
            {
                TotalRecords = productData[0].TotalRowCount.Value;
            }
            return Json(new
            {
                param.iDisplayStart,
                iTotalRecords = TotalRecords,
                iTotalDisplayRecords = TotalRecords,
                aaData = resultSet
            }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Save Product information 
        /// </summary>

        [HttpPost]
        public ActionResult SaveProductAttribute(ProductAttributeModal model)
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
                if (responseStatus && model.ProdCatId > 0 && productCategoriesRepository.getProductCategoryDetilsById(model.ProdCatId) != null)
                {
                    InsertUpdateProductAttribute(model);
                    responseStatus = true;
                    messages.Add("Product Category data inserted successfully.");
                }
                else if (responseStatus)
                {
                    InsertUpdateProductAttribute(model);

                    responseStatus = true;
                    messages.Add("Product Category data updated successfully.");
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
        public ActionResult DeleteProductAttribute(int id)
        {
            var result = productCategoriesRepository.DeleteProductAttribute(id);


            return View("ProductCategory");
        }
        /// <summary>
        /// Call ProductAttribute partial view for pop up add/update ProductAttribute information 
        /// </summary>
        [HttpGet]
        public ActionResult ProductAttributeFormPartialView(int? id)
        {
            if (id == null)
                id = 0;
            var modeldata = productCategoriesRepository.getProductAttributeById(id.Value);
            if (modeldata == null)
            {
                ProductAttributeModal objresult = new ProductAttributeModal();
                BindProductCategoryModel(objresult);
                return View(objresult);
            }
            var result = new ProductAttributeModal()
            {
                AttributeId = modeldata.AttributeId,
                ProdCatId = modeldata.ProdCatId,
                AttributeName = modeldata.AttributeName
            };
            BindProductCategoryModel(result);
            result = result == null ? new ProductAttributeModal() : result;

            return View(result);
        }
        [NonAction]
        private void BindProductCategoryModel(ProductAttributeModal model)
        {
            model.ProductCategoryList = productCategoriesRepository.GetProductCategoryInfo(null, null, null, 1, int.MaxValue)
                  .Select(x => new SelectListItem()
                  {
                      Text = x.CategoryName,
                      Value = x.ProdCatId.ToString()
                  }).ToList();

            model.ProductCategoryList.Insert(0, new SelectListItem() { Text = "Select", Value = "" });

        }
        /// <summary>
        /// Call no action method to call save ProductAttribute
        /// </summary>
        [NonAction]
        private void InsertUpdateProductAttribute(ProductAttributeModal model)
        {
            try
            {
                ProductAttribute productAttribute = new ProductAttribute();
                productAttribute.AttributeName = model.AttributeName;
                productAttribute.ProdCatId = model.ProdCatId;
                productAttribute.AttributeId = model.AttributeId;
                var result = productCategoriesRepository.InsertUpdateProductAttribute(productAttribute);
            }
            catch (Exception ex)
            { throw ex; }


        }




    }
}