using MoneyFlow.MVVM.Models.MSSQL_DB;

namespace MoneyFlow.Utils.Services.AuthorizationVerificationServices
{
    public interface IAuthorizationVerificationService
    {
        User CurrentUser { get; }
        bool CheckAuthorization();
        void CreateJsonUser(User user);
    }
}
