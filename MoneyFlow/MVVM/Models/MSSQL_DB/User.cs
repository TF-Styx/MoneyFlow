using System;
using System.Collections.Generic;

namespace MoneyFlow.MVVM.Models.MSSQL_DB;

public partial class User
{
    public int IdUser { get; set; }

    public string UserName { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int IdGender { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual Gender IdGenderNavigation { get; set; } = null!;
}
