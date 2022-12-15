﻿using OnlineStore.DataAccess.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineStore.DataAccess.Repositories.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<Category> GetCategory(int id);
        Task<Category> GetCategoryWithProducts(int id);
        Task<IEnumerable<Category>> GetBaseCategories();
    }
}
