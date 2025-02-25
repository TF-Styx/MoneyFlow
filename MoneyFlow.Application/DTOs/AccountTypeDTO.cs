using MoneyFlow.Application.DTOs.BaseDTOs;
using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Application.DTOs
{
    public class AccountTypeDTO : BaseDTO<AccountTypeDTO>
    {
        private AccountTypeDTO(int idAccountType, string accountTypeName)
        {
            IdAccountType = idAccountType;
            AccountTypeName = accountTypeName;
        }

        public int IdAccountType { get; set; }
        public string? AccountTypeName { get; set; }

        public static (AccountTypeDTO AccountTypeDTO, string Message) Create(int idAccountType, string accountTypeName)
        {
            var message = string.Empty;

            if (accountTypeName.Length > IntConstants.MAX_ACCOUNT_TYPE_NAME_LENGHT)
            {
                return (null, "Превышена длина слова в «255» символов");
            }

            var accountType = new AccountTypeDTO(idAccountType, accountTypeName);

            return (accountType, message);
        }
    }
}
