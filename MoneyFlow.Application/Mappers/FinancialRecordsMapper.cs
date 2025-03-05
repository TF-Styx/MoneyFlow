using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    internal static class FinancialRecordsMapper
    {
        public static (FinancialRecordDTO FinancialRecordDTO, string Message) ToDTO(this FinancialRecordDomain financialRecord)
        {
            string message = string.Empty;

            if (financialRecord == null) { return (null, "Данной финансовой записи нет!!"); }

            var dto = new FinancialRecordDTO()
            {
                IdFinancialRecord = financialRecord.IdFinancialRecord,
                RecordName = financialRecord.RecordName,
                Amount = financialRecord.Amount,
                Description = financialRecord.Description,
                IdTransactionType = financialRecord.IdTransactionType,
                IdUser = financialRecord.IdUser,
                IdCategory = financialRecord.IdCategory,
                IdAccount = financialRecord.IdAccount,
                Date = financialRecord.Date,
            };

            return (dto, message);
        }

        public static List<FinancialRecordDTO> ToListDTO(this IEnumerable<FinancialRecordDomain> financialRecords)
        {
            var list = new List<FinancialRecordDTO>();

            foreach (var item in financialRecords)
            {
                list.Add(item.ToDTO().FinancialRecordDTO);
            }
            return list;
        }
    }
}
