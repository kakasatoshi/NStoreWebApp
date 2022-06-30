using Service.Interfaces;
using Service.Models;
using Service.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Service.Services
{
    public class ProductService : IProduct
    {
        private readonly NStoreContext _context;

        public ProductService(NStoreContext context)
        {
            _context = context;
        }

        public ProductModel.Output.PagedResult GetAllPaging(ProductModel.Input.GetProductPagingRequest request)
        {
            var query = from p in _context.Products
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        select new { p, pic, pi };
            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword) || x.p.Name.Contains(request.Keyword));
            }

            if (request.CategoryId != null && request.CategoryId != 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3. Paging
            int totalRow = query.Count();

            var data = query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductModel.ProductBase()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Price = x.p.Price,
                    OriginalPrice = x.p.OriginalPrice,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    DateCreated = x.p.DateCreated,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    ImagePath = x.pi.ImagePath
                }).ToList();
            var pagedResult = new ProductModel.Output.PagedResult()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };
            return pagedResult;
        }

        public ProductModel.ProductBase GetById(int productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id.Equals(productId));

            var categories = (from c in _context.Categories
                              join pic in _context.ProductInCategories on c.Id equals pic.CategoryId
                              where pic.ProductId == productId
                              select c.Name).ToList();

            var productModel = new ProductModel.ProductBase()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = product.Description,
                Details = product.Details,
                Name = product.Name,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                Categories = categories
            };
            return productModel;
        }

        public List<ProductModel.ProductBase> GetFeaturedProducts(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in ppic.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        select new { p, c, pi };

            var data = query.OrderByDescending(x => x.p.ViewCount).Take(take)
                .Select(x => new ProductModel.ProductBase()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Price = x.p.Price,
                    OriginalPrice = x.p.OriginalPrice,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    DateCreated = x.p.DateCreated,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    ImagePath = x.pi.ImagePath
                }).ToList();

            return data;
        }

        public List<ProductModel.ProductBase> GetLastestProduct(int take)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _context.Categories on pic.CategoryId equals c.Id into picc
                        from c in ppic.DefaultIfEmpty()
                        join pi in _context.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        select new { p, c, pi };

            var data = query.OrderByDescending(x => x.p.DateCreated).Take(take)
                .Select(x => new ProductModel.ProductBase()
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Price = x.p.Price,
                    OriginalPrice = x.p.OriginalPrice,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    DateCreated = x.p.DateCreated,
                    Description = x.p.Description,
                    Details = x.p.Details,
                    ImagePath = x.pi.ImagePath
                }).ToList();

            return data;
        }
    }
}