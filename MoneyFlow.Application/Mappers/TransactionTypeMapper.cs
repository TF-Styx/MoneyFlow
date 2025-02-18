using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    public static class TransactionTypeMapper
    {
        public static (TransactionTypeDTO TransactionTypeDTO, string Message) ToDTO(this TransactionTypeDomain transactionType)
        {
            if (transactionType == null) { return (null, "Тип транзакции не найден!!"); }

            return TransactionTypeDTO.Create(transactionType.IdTransactionType, transactionType.TransactionTypeName, transactionType.Description);
        }

        public static List<TransactionTypeDTO> ToListDTO(this IEnumerable<TransactionTypeDomain> transactionTypes)
        {
            var list = new List<TransactionTypeDTO>();

            foreach (var item in transactionTypes)
            {
                list.Add(item.ToDTO().TransactionTypeDTO);
            }

            return list;
        }
    }
}
