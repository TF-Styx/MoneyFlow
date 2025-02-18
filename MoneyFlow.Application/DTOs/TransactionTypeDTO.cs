using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Application.DTOs
{
    public class TransactionTypeDTO
    {
        private TransactionTypeDTO(int idTransactionType, string? transactionTypeName, string? description)
        {
            IdTransactionType = idTransactionType;
            TransactionTypeName = transactionTypeName;
            Description = description;
        }

        public int IdTransactionType { get; set; }
        public string? TransactionTypeName { get; set; }
        public string? Description { get; set; }

        public static (TransactionTypeDTO TransactionTypeDTO, string Message) Create(int idTransactionType, string? transactionTypeName, string? description)
        {
            var message = string.Empty;

            if (transactionTypeName.Length > IntConstants.MAX_TRANSACTIONTYPENAME_LENGHT &&
                description.Length > IntConstants.MAX_DESCRIPTION_LENGHT)
            {
                return (null, "Превышена допустимая длина в «255» символов!!");
            }

            var transactionType = new TransactionTypeDTO(idTransactionType, transactionTypeName, description);

            return (transactionType, message);
        }
    }
}
