using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    public static class CategoriesMapper
    {
        public static (CategoryDTO CategoryDTO, string Message) ToDTO(this CategoryDomain category)
        {
            string message = string.Empty;

            if (category == null) { return (null, "Данной категории не найдено!!"); }

            var dto = new CategoryDTO()
            {
                IdCategory = category.IdCategory,
                CategoryName = category.CategoryName,
                Description = category.Description,
                Color = category.Color,
                Image = category.Image,
                IdUser = category.IdUser,
            };

            return (dto, message);
        }

        public static List<CategoryDTO> ToListDTO(this IEnumerable<CategoryDomain> categories)
        {
            var list = new List<CategoryDTO>();

            foreach (var item in categories)
            {
                list.Add(item.ToDTO().CategoryDTO);
            }

            return list;
        }
    }
}
