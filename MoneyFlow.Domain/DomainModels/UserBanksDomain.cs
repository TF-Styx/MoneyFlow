namespace MoneyFlow.Domain.DomainModels
{
    public class UserBanksDomain
    {
        public int IdUser { get; set; }
        public List<BankDomain> Banks { get; set; } = new List<BankDomain>();
    }
}
