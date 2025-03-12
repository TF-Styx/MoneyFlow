using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Domain.DomainModels
{
    public class FinancialRecordViewingDomain
    {
        private FinancialRecordViewingDomain(int idFinancialRecord, string? recordName, decimal? amount, string? description, string transactionTypeName, int? idUser, string categoryName, List<string> subcategoryName, int? accountNumber, DateTime? date)
        {
            IdFinancialRecord = idFinancialRecord;
            RecordName = recordName;
            Amount = amount;
            Description = description;
            TransactionTypeName = transactionTypeName;
            IdUser = idUser;
            CategoryName = categoryName;
            SubcategoryName = subcategoryName;
            AccountNumber = accountNumber;
            Date = date;
        }

        public int IdFinancialRecord { get; private set; }
        public string? RecordName { get; private set; }
        public decimal? Amount { get; private set; }
        public string? Description { get; private set; }
        public string TransactionTypeName { get; private set; }
        public int? IdUser { get; private set; }
        public string CategoryName { get; private set; }
        public List<string> SubcategoryName { get; private set; }
        public int? AccountNumber { get; private set; }
        public DateTime? Date { get; private set; }

        public static (FinancialRecordViewingDomain FinancialRecordViewingDomain, string Message) Create(int idFinancialRecord, string? recordName, decimal? amount, string? description, string transactionTypeName, int? idUser, string categoryName, List<string> subcategoryName, int? accountNumber, DateTime? date)
        {
            var message = string.Empty;

            var financialRecord = new FinancialRecordViewingDomain(idFinancialRecord, recordName, amount, description, transactionTypeName, idUser, categoryName, subcategoryName, accountNumber, date);

            return (financialRecord, message);
        }
    }
}
