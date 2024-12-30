using System;
using System.Collections.Generic;

namespace MoneyFlow.MVVM.Models.MSSQL_DB;

public partial class Subcategory
{
    public int IdSubcategory { get; set; }

    public string SubcategoryName { get; set; } = null!;

    public byte[]? Image { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<FinancialRecord> FinancialRecords { get; set; } = new List<FinancialRecord>();
}
