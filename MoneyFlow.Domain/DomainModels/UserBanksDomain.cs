namespace MoneyFlow.Domain.DomainModels
{
    public class UserBanksDomain
    {
        public int IdUser { get; set; } // private set;
        public List<BankDomain> Banks { get; set; } = new List<BankDomain>(); // private set;

        public void AddNewBank(BankDomain bank)
        {
            if (string.IsNullOrWhiteSpace(bank.BankName)) { throw new Exception("Наименование банка отсутствует!!"); }

            Banks.Add(bank);
        }
    }
}
