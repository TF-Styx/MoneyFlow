using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    internal static class GendersMapper
    {
        public static (GenderDTO GenderDTO, string Message) ToDTO(this GenderDomain gender)
        {
            if (gender == null)
            {
                return (null, "Пол ноль");
            }
            return GenderDTO.Create(gender.IdGender, gender.GenderName);
        }

        public static List<GenderDTO> ToListDTO(this IEnumerable<GenderDomain> genders)
        {
            var list = new List<GenderDTO>();

            foreach (var item in genders)
            {
                list.Add(item.ToDTO().GenderDTO);
            }
            return list;
        }
    }
}
