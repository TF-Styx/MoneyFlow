using MoneyFlow.Application.DTOs.BaseDTOs;
using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Application.DTOs
{
    public class FinancialRecordDTO : BaseDTO<FinancialRecordDTO>
    {
        private FinancialRecordDTO(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            IdFinancialRecord = idFinancialRecord;
            RecordName = recordName;
            Amount = amount;
            Description = description;
            IdTransactionType = idTransactionType;
            IdUser = idUser;
            IdCategory = idCategory;
            IdAccount = idAccount;
            Date = date;
        }

        public int IdFinancialRecord { get; set; }
        public string? RecordName { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public int? IdTransactionType { get; set; }
        public int? IdUser { get; set; }
        public int? IdCategory { get; set; }
        public int? IdAccount { get; set; }
        public DateTime? Date { get; set; }

        public static (FinancialRecordDTO FinancialRecordDTO, string Message) Create(int idFinancialRecord, string? recordName, decimal? amount, string? description, int? idTransactionType, int? idUser, int? idCategory, int? idAccount, DateTime? date)
        {
            var message = string.Empty;
            
            if (recordName.Length > IntConstants.MAX_RECORDNAME_LENGHT &&
                description.Length > IntConstants.MAX_DESCRIPTION_LENGHT &&
                amount > IntConstants.MAX_AMOUNT_LENGHT)
            {
                return (null, "Превышена максимально допустимая длина!!");
            }

            var financialRecord = new FinancialRecordDTO(idFinancialRecord, recordName, amount, description, idTransactionType, idUser, idCategory, idAccount, date);

            return (financialRecord, message);
        }
    }
}
