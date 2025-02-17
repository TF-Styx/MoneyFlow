using MoneyFlow.Application.DTOs;
using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Application.Mappers
{
    internal static class UsersMapper
    {
        public static (UserDTO UserDTO, string Message) ToDTO(this UserDomain user)
        {
            if (user == null)
            {
                return (null, "Данного пользователя нет!!");
            }
            return UserDTO.Create(user.IdUser, user.UserName, user.Avatar,
                user.Login, user.Password, user.IdGender);
        }

        public static List<UserDTO> ToListDTO(this IEnumerable<UserDomain> users)
        {
            var list = new List<UserDTO>();

            foreach (var item in users)
            {
                list.Add(item.ToDTO().UserDTO);
            }
            return list;
        }
    }
}
