using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.ViewModels;

namespace Service.Interfaces
{
    public interface IProduct
    {
        ProductModel.Output.PagedResult GetAllPaging(ProductModel.Input.GetProductPagingRequest request);

        ProductModel.ProductBase GetById(int productId);
    }
}