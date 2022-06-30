using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NStore.Common;
using NStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        public IActionResult Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var input = new ProductModel.Input.GetProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                CategoryId = categoryId
            };
            var data = Utilities.SendDataRequest<ProductModel.Output.PagedResult>(ConstantValues.Product.Paging, input);
            ViewBag.Keyword = keyword;

            var categories = Utilities.SendDataRequest<List<CategoryModel>>(ConstantValues.Category.GetAll);
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString(),
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            });
            if (TempData["result"] != null)
            {
                ViewBag.Successmsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Detail(int productId)
        {
            var data = Utilities.SendDataRequest<ProductModel.ProductBase>(ConstantValues.Product.Detail + $"/{productId}", productId);
            return View(data);
        }
    }
}