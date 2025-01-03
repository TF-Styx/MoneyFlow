using Microsoft.EntityFrameworkCore;
using MoneyFlow.MVVM.Models.MSSQL_DB;

namespace MoneyFlow.Utils.Helpers
{
    public static class DataBaseHelper
    {
        public static async Task<List<Subcategory>> GetSubcategoriesByUserIdAsync(int idUser)
        {
            using (var context = new MoneyFlowContext())
            {
                var subcategories = await context.Subcategories
                    .Where(sub => context.Categories
                        .Any(cat => cat.IdCategory == sub.IdSubcategory && cat.IdUser == idUser))
                    .ToListAsync();

                return subcategories;
            }
        }
    }
}
