using MoneyFlow.MVVM.Models.DB_MSSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyFlow.Utils.Helpers
{
    public class LastRecordHelper(Func<MoneyFlowDbContext> context)
    {
        private readonly Func<MoneyFlowDbContext> _context = context;

        public FinancialRecord LastRecordFinancialRecord(User user)
        {
            using (MoneyFlowDbContext context = _context())
            {
                return context.FinancialRecords
                    .Where(x => x.IdCategoryNavigation.IdUser == user.IdUser)
                        .OrderByDescending(x => x.IdFinancialRecord)
                            .FirstOrDefault();
            }
        }

        public Category LastRecordCategory(User user)
        {
            using (MoneyFlowDbContext context = _context())
            {
                return context.Categories
                    .Where(x => x.IdUser == user.IdUser)
                        .OrderByDescending(x => x.IdCategory)
                            .FirstOrDefault();
            }
        }

        public Subcategory LastRecordSubcategory(Category category)
        {
            using (MoneyFlowDbContext context = _context())
            {
                return context.Subcategories
                    .Where(x => x.IdCategory == category.IdCategory)
                        .OrderByDescending(x => x.IdCategory)
                            .FirstOrDefault();
            }
        }

        public Account LastRecordAccount(User user)
        {
            using (MoneyFlowDbContext context = _context())
            {
                return context.Accounts
                    .Where(x => x.IdUser == user.IdUser)
                        .OrderByDescending(x => x.IdAccount)
                            .FirstOrDefault();
            }
        }
    }
}
