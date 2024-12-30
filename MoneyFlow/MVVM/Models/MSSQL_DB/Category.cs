using System;
using System.Collections.Generic;

namespace MoneyFlow.MVVM.Models.MSSQL_DB;

public partial class Category
{
    public int IdCategory { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Color { get; set; }

    public byte[]? Image { get; set; }

    public int IdUser { get; set; }

    public virtual ICollection<FinancialRecord> FinancialRecords { get; set; } = new List<FinancialRecord>();

    public virtual User IdUserNavigation { get; set; } = null!;
}
