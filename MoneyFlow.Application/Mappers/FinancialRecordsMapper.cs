﻿using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;
using System.Collections.ObjectModel;

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


        public static (FinancialRecordViewingDTO FinancialRecordViewingDTO, string Message) ToDTO(this FinancialRecordViewingDomain financialRecordViewing)
        {
            string message = string.Empty;

            if (financialRecordViewing == null) { return (null, "Данной финансовой записи нет!!"); }

            var subcategoryName = new ObservableCollection<string>();
            foreach (var item in financialRecordViewing.SubcategoryName)
            {
                subcategoryName.Add(item);
            }

            var dto = new FinancialRecordViewingDTO()
            {
                IdFinancialRecord = financialRecordViewing.IdFinancialRecord,
                RecordName = financialRecordViewing.RecordName,
                Amount = financialRecordViewing.Amount,
                Description = financialRecordViewing.Description,
                TransactionTypeName = financialRecordViewing.TransactionTypeName,
                IdUser = financialRecordViewing.IdUser,
                CategoryName = financialRecordViewing.CategoryName,
                SubcategoryName = subcategoryName,
                AccountNumber = financialRecordViewing.AccountNumber,
                Date = financialRecordViewing.Date,
            };

            return (dto, message);
        }

        public static List<FinancialRecordViewingDTO> ToListDTO(this IEnumerable<FinancialRecordViewingDomain> financialRecordViewings)
        {
            var list = new List<FinancialRecordViewingDTO>();

            foreach (var item in financialRecordViewings)
            {
                list.Add(item.ToDTO().FinancialRecordViewingDTO);
            }
            return list;
        }
    }
}
