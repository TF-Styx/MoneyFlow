namespace MoneyFlow.Domain.DomainModels
{
    public class AccountDomain
    {
        public int IdAccount { get; set; }
        public int? NumberAccount { get; set; }
        public int? IdUser { get; set; }
        public int? IdBank { get; set; }
        public int? IdAccountType { get; set; }
        public decimal? Balance { get; set; }
    }
}
