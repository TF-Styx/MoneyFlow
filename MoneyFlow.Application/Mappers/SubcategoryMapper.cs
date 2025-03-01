using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    public static class SubcategoryMapper
    {
        public static (SubcategoryDTO SubcategoryDTO, string Message) ToDTO(this SubcategoryDomain subcategory)
        {
            if (subcategory == null) { return (null, "Данной под категории не найдено!!"); }

            return SubcategoryDTO.Create(subcategory.IdSubcategory, subcategory.SubcategoryName, subcategory.Description, subcategory.Image);
        }

        public static List<SubcategoryDTO> ToListDTO(this IEnumerable<SubcategoryDomain> subcategories)
        {
            var list = new List<SubcategoryDTO>();

            foreach (var item in subcategories)
            {
                list.Add(item.ToDTO().SubcategoryDTO);
            }

            return list;
        }
    }
}
