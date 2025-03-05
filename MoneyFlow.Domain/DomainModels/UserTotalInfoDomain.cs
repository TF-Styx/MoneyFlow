﻿namespace MoneyFlow.Domain.DomainModels
{
    public class UserTotalInfoDomain
    {
        private UserTotalInfoDomain(string? genderName, decimal? totalBalance, int bankCount, int categoryCount, int subcategoryCount, int financialRecordCount)
        {
            GenderName = genderName;
            TotalBalance = totalBalance;
            BankCount = bankCount;
            CategoryCount = categoryCount;
            SubcategoryCount = subcategoryCount;
            FinancialRecordCount = financialRecordCount;
        }

        public string? GenderName { get; private set; }
        public decimal? TotalBalance { get; private set; }
        public int BankCount { get; private set; }
        public int CategoryCount { get; private set; }
        public int SubcategoryCount { get; private set; }
        public int FinancialRecordCount { get; private set; }

        public static (UserTotalInfoDomain UserTotalInfoDomain, string Message) Create(string? genderName, decimal? totalBalance, int bankCount, int categoryCount, int subcategoryCount, int financialRecordCount)
        {
            string message = string.Empty;

            var userAccountTypes = new UserTotalInfoDomain(genderName, totalBalance, bankCount, categoryCount, subcategoryCount, financialRecordCount);

            return (userAccountTypes, message);
        }
    }
}
