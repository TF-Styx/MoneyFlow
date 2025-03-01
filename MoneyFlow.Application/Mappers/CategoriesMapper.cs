using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    public static class CategoriesMapper
    {
        public static (CategoryDTO CategoryDTO, string Message) ToDTO(this CategoryDomain category)
        {
            if (category == null) { return (null, "Данной категории не найдено!!"); }

            return CategoryDTO.Create(category.IdCategory, category.CategoryName, category.Description, category.Color, category.Image, category.IdUser);
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
