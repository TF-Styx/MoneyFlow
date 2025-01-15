using MoneyFlow.MVVM.Models.MSSQL_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyFlow.Utils.Helpers
{
    public class LastRecordHelper(Func<MoneyFlowContext> context)
    {
        private readonly Func<MoneyFlowContext> _context = context;

        public FinancialRecord LastRecordFinancialRecord(User user)
        {
            using (MoneyFlowContext context = _context())
            {
                return context.FinancialRecords
                    .Where(x => x.IdCategoryNavigation.IdUser == user.IdUser)
                        .OrderByDescending(x => x.IdFinancialRecord)
                            .FirstOrDefault();
            }
        }
    }
}
