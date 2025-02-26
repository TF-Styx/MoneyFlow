namespace MoneyFlow.Domain.DomainModels
{
    public class SubcategoryDomain
    {
        public int IdSubcategory { get; set; }
        public string? SubcategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public int IdCategory { get; set; }
    }
}
