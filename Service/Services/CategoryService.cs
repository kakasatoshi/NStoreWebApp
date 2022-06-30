﻿using Service.Interfaces;
using Service.Models;
using Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class CategoryService : ICategory
    {
        private readonly NStoreContext _context;

        public CategoryService(NStoreContext context)
        {
            _context = context;
        }

        public List<CategoryModel> GetAll()
        {
            var query = _context.Categories.ToList();
            var result = query.Select(x => new CategoryModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return result;
        }
    }
}