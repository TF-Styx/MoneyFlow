using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Domain.DomainModels
{
    public class CategoryDomain
    {
        private CategoryDomain(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            IdCategory = idCategory;
            CategoryName = categoryName;
            Description = description;
            Color = color;
            Image = image;
            IdUser = idUser;
        }

        public int IdCategory { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? Color { get; set; }
        public byte[]? Image { get; set; }
        public int IdUser { get; set; }

        public static (CategoryDomain CategoryDomain, string Message) Create(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            var message = string.Empty;

            if (categoryName.Length > IntConstants.MAX_SUBCATEGORYNAME_LENGHT)
            {
                return (null, "Превышена допустимая длина в «255» символов");
            }

            var category = new CategoryDomain(idCategory, categoryName, description, color, image, idUser);

            return (category, message);
        }
    }
}
