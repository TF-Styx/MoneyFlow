using System;
using System.Collections.Generic;

namespace MoneyFlow.MVVM.Models.MSSQL_DB;

public partial class FinancialRecord
{
    public int IdFinancialRecord { get; set; }

    public string RecordName { get; set; }

    public decimal Amount { get; set; }

    public string Description { get; set; }

    public DateTime Date { get; set; }

    public int IdCategory { get; set; }

    public int? IdSubcategory { get; set; }

    public virtual Category IdCategoryNavigation { get; set; }

    public virtual Subcategory IdSubcategoryNavigation { get; set; }
}
