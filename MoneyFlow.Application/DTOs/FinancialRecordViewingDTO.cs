﻿using MoneyFlow.Application.DTOs.BaseDTOs;

namespace MoneyFlow.Application.DTOs
{
    public class FinancialRecordViewingDTO : BaseDTO<FinancialRecordViewingDTO>
    {
        public int IdFinancialRecord { get; set; }
        public string? RecordName { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }
        public int? IdTransactionType { get; set; }
        public string TransactionTypeName { get; set; }
        public int? IdUser { get; set; }
        public int? IdCategory { get; set; }
        public string? CategoryName { get; set; }
        public int? IdSubcategory { get; set; }
        public string? SubcategoryName { get; set; }
        public int? AccountNumber { get; set; }
        public DateTime? Date { get; set; }

        public int Index { get; set; }
    }
}
