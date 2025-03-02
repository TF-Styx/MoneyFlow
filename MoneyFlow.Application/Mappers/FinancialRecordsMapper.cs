using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    internal static class FinancialRecordsMapper
    {
        public static (FinancialRecordDTO FinancialRecordDTO, string Message) ToDTO(this FinancialRecordDomain financialRecord)
        {
            if (financialRecord == null) { return (null, "Данной финансовой записи нет!!"); }
            return FinancialRecordDTO.Create(financialRecord.IdFinancialRecord, financialRecord.RecordName, financialRecord.Amount, financialRecord.Description, financialRecord.IdTransactionType, financialRecord.IdUser, financialRecord.IdCategory, financialRecord.IdAccount, financialRecord.Date);
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
