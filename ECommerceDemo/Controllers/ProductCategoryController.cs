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
    public class ProductCategoryController : Controller
    {
        // GET: ProductCategory
        private IProductRepository productRepository;
        private IProductCategoriesRepository productCategoriesRepository;
        public ProductCategoryController(IProductRepository _productRepository, IProductCategoriesRepository _productCategoriesRepository)
        {
            productRepository = _productRepository;
            productCategoriesRepository = _productCategoriesRepository;

        }
        // GET: ProductCategory
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
        /// Get Product Summary Screen  to call ajax call return ProductCategory information
        /// </summary>

        [HttpGet]
        public ActionResult GetProductCategory(JqueryDatatableParam param)
        {
            var filterText = !string.IsNullOrEmpty(param.sSearch) ? param.sSearch.ToLower() : null;
            var currentPage = Convert.ToInt32(param.sEcho);
            var pageSize = param.iDisplayLength;
            var sortDirection = Request.QueryString["sSortDir_0"];
            var sortColumnIndex = Convert.ToInt32(Request.QueryString["iSortCol_0"]);
            var sortColumnName = sortColumnIndex == 0
                ? "ProdCatId"
                : (sortColumnIndex == 1
                ? "CategoryName"
                : (sortColumnIndex == 2
                ? ""
                : (sortColumnIndex == 3
                ? ""
                : "")));
            currentPage = param.iDisplayStart == 0 ? 1 : (param.iDisplayStart / param.iDisplayLength) + 1;

            var productData = productCategoriesRepository.GetProductCategoryInfo(filterText, sortColumnName, sortDirection, currentPage, pageSize);
            var resultSet = productData
                .Select(x => new String[] {
                    Convert.ToString(x.ProdCatId),
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
        public ActionResult SaveProductCaterory(ProductCateroryModal model)
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
                    InsertUpdateProductCategory(model);
                    responseStatus = true;
                    messages.Add("Product Category data inserted successfully.");
                }
                else if (responseStatus)
                {
                    InsertUpdateProductCategory(model);

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
        public ActionResult Delete(int id)
        {
            var result = productCategoriesRepository.Delete(id);


            return View("ProductCategory");
        }
        /// <summary>
        /// Call Product partial view for pop up add/update product information 
        /// </summary>
        [HttpGet]
        public ActionResult ProductCategoryFormPartialView(int? id)
        {
            if (id == null)
                id = 0;
            var modeldata = productCategoriesRepository.getProductCategoryDetilsById(id.Value);
            if (modeldata == null)
            {
                ProductCateroryModal objresult = new ProductCateroryModal();
              
                return View(objresult);
            }
            var result = new ProductCateroryModal()
            {
                ProdCatId = modeldata.ProdCatId,
                CategoryName = modeldata.CategoryName
            };
            result = result == null ? new ProductCateroryModal() : result;
           
            return View(result);
        }
        /// <summary>
        /// Call no action method to call save product
        /// </summary>
        [NonAction]
        private void InsertUpdateProductCategory(ProductCateroryModal model)
        {
            try
            {
                ProductCategory product = new ProductCategory();
                product.CategoryName = model.CategoryName;
                
                product.ProdCatId = model.ProdCatId;
                var result = productCategoriesRepository.AddUpdateProductCategory(product);
            }
            catch (Exception ex)
            { throw ex; }


        }


       

    }
}