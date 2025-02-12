using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Application.DTOs
{
    public class BankDTO
    {
        private BankDTO(int idBank, string bankName)
        {
            IdBank = idBank;
            BankName = bankName;
        }

        public int IdBank { get; }
        public string? BankName { get; }


        public static (BankDTO BankDTO, string Message) Create(int idBank, string bankName)
        {
            var message = string.Empty;

            if (bankName.Length > IntConstants.MAX_BANK_NAME_LENGHT)
            {
                return (null, "Превышена длина слова в «255» символов");
            }

            var bank = new BankDTO(idBank, bankName);

            return (bank, message);
        }
    }
}
