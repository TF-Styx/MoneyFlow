using MoneyFlow.Application.DTOs.BaseDTOs;
using System.Collections.ObjectModel;

namespace MoneyFlow.Application.DTOs
{
    public class FinancialRecordViewingDTO : BaseDTO<FinancialRecordViewingDTO>
    {
        public int IdFinancialRecord { get; set; }
        public string? RecordName { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public string TransactionTypeName { get; set; }
        public int? IdUser { get; set; }
        public string CategoryName { get; set; }
        public ObservableCollection<string> SubcategoryName { get; set; }
        public int? AccountNumber { get; set; }
        public DateTime? Date { get; set; }
    }
}
