using MoneyFlow.Application.DTOs.BaseDTOs;
using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Application.DTOs
{
    public class SubcategoryDTO : BaseDTO<SubcategoryDTO>
    {
        private SubcategoryDTO(int idSubcategory, string?subcategoryName, string? description, byte[] image, int idCategory)
        {
            IdSubcategory = idSubcategory;
            SubcategoryName = subcategoryName;
            Description = description;
            Image = image;
            IdCategory = idCategory;
        }

        public int IdSubcategory { get; set; }
        public string? SubcategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Image { get; set; }
        public int IdCategory { get; set; }

        public static (SubcategoryDTO SubcategoryDTO, string Message) Create(int idSubcategory, string? subcategoryName, string? description, byte[] image, int idCategory)
        {
            var message = string.Empty;

            if (subcategoryName.Length > IntConstants.MAX_SUBCATEGORYNAME_LENGHT)
            {
                return (null, "Превышена допустимая длина в «255» символов");
            }

            var subcategory = new SubcategoryDTO(idSubcategory, subcategoryName, description, image, idCategory);

            return (subcategory, message);
        }
    }
}
