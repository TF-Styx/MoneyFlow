using System;
using System.Collections.Generic;

namespace MoneyFlow.MVVM.Models.MSSQL_DB;

public partial class User
{
    public int IdUser { get; set; }

    public string UserName { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public int? IdGender { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual Gender IdGenderNavigation { get; set; }
}
