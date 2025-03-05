﻿using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Domain.DomainModels
{
    public class AccountDomain
    {
        private AccountDomain(int idAccount, int? numberAccount, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance)
        {
            IdAccount = idAccount;
            NumberAccount = numberAccount;
            Bank = bankDomain;
            AccountType = accountTypeDomain;
            Balance = balance;
        }

        public int IdAccount { get; set; }
        public int? NumberAccount { get; set; }
        public BankDomain Bank { get; set; }
        public AccountTypeDomain AccountType { get; set; }
        public decimal? Balance { get; set; }

        public static (AccountDomain AccountDomain, string Message) Create(int idAccount, int? numberAccount, BankDomain bankDomain, AccountTypeDomain accountTypeDomain, decimal? balance)
        {
            var message = string.Empty;

            if (numberAccount.ToString().Length > IntConstants.MAX_NUMBERACCOUNT_LENGHT)
            {
                return (null, "Превышена допустимая длина в «16» символов");
            }

            var account = new AccountDomain(idAccount, numberAccount, bankDomain, accountTypeDomain, balance);

            return (account, message);
        }
    }
}
