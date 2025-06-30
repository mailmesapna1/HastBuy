using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HastBuyDAL.Models;

namespace HastBuyDAL
{
    public class HastBuyRepository
    {
        private HastBuyDbContext context;
        public HastBuyRepository(HastBuyDbContext context)
        {
            this.context = context;
        }
        public List<Category> GetAllCategories()
        {
            var categoriesList = context.Categories
                            .OrderBy(c => c.CategoryId)
                            .ToList();
            return categoriesList;
        }
    }
}
