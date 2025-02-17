using MoneyFlow.Application.DTOs.BaseDTOs;
using MoneyFlow.Shared.Constants;

namespace MoneyFlow.Application.DTOs
{
    public class UserDTO : BaseDTO<UserDTO>
    {
        private UserDTO(int idUser, string? userName, byte[]? avatar,
                        string login, string password, int? idGender)
        {
            IdUser = idUser;
            UserName = userName;
            Avatar = avatar;
            Login = login;
            Password = password;
            IdGender = idGender;
        }

        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public byte[]? Avatar { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int? IdGender { get; set; }

        public static (UserDTO UserDTO, string Message) Create(int idUser, string? userName, byte[]? avatar,
                                                               string login, string password, int? idGender)
        {
            var message = string.Empty;

            if (userName.Length > IntConstants.MAX_USER_NAME_LENGHT &&
                login.Length > IntConstants.MAX_LOGIN_LENGHT &&
                password.Length > IntConstants.MAX_PASSWORD_LENGHT)
            {
                return (null, "Превышена допустимая длина в «255» символов!!");
            }

            var user = new UserDTO(idUser, userName, avatar, login, password, idGender);

            return (user, message);
        }
    }
}
