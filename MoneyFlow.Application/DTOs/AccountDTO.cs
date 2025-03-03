using MoneyFlow.Application.DTOs.BaseDTOs;
using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Application.DTOs
{
    public class AccountDTO : BaseDTO<AccountDTO>
    {
        private AccountDTO(int idAccount, int? numberAccount, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance)
        {
            IdAccount = idAccount;
            NumberAccount = numberAccount;
            Bank = bankDTO;
            AccountType = accountTypeDTO;
            Balance = balance;
        }

        public int IdAccount { get; set; }
        public int? NumberAccount { get; set; }
        public BankDTO Bank { get; set; }
        public AccountTypeDTO AccountType { get; set; }
        public decimal? Balance { get; set; }

        public static (AccountDTO AccountDTO, string Message) Create(int idAccount, int? numberAccount, BankDTO bankDTO, AccountTypeDTO accountTypeDTO, decimal? balance)
        {
            var message = string.Empty;
            
            if (numberAccount.ToString().Length > IntConstants.MAX_NUMBERACCOUNT_LENGHT)
            {
                return (null, "Превышена допустимая длина в «16» символов");
            }

            var account = new AccountDTO(idAccount, numberAccount, bankDTO, accountTypeDTO, balance);

            return (account, message);
        }
    }
}
