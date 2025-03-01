namespace MoneyFlow.Domain.DomainModels
{
    public class CategoryDomain
    {
        public int IdCategory { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public byte[]? Image { get; set; }
        public int IdUser { get; set; }
    }
}
