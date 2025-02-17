using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;

namespace MoneyFlow.Application.Services.Realization
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IUserService _userService;

        public RegistrationService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<(UserDTO UserDTO, string Message)> Registration(string userName, string login, string password)
        {
            var (UserDTO, Message) = await _userService.CreateUser(userName, login, password);

            return (UserDTO, Message);
        }
    }
}
