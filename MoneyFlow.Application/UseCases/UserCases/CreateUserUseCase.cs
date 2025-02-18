using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.UserCases
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUsersRepository _usersRepository;

        public CreateUserUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<(UserDTO UserDTO, string Message)> CreateAsyncUser(string userName, string login, string password)
        {
            string message = string.Empty;

            if (string.IsNullOrWhiteSpace(userName) &&
                string.IsNullOrWhiteSpace(login) &&
                string.IsNullOrWhiteSpace(password))
            {
                return (null, "Вы не заполнили поля!!");
            }

            var existUser = await _usersRepository.GetAsync(login);

            if (existUser != null) { return (null, "Пользователь с таким логином уже есть!!"); }

            var idUser = await _usersRepository.CreateAsync(userName, login, password);
            var userDomain = await _usersRepository.GetAsync(idUser);

            return (userDomain.ToDTO().UserDTO, message);
        }
        public (UserDTO UserDTO, string Message) CreateUser(string userName, string login, string password)
        {
            string message = string.Empty;

            if (string.IsNullOrWhiteSpace(userName) &&
                string.IsNullOrWhiteSpace(login) &&
                string.IsNullOrWhiteSpace(password))
            {
                return (null, "Вы не заполнили поля!!");
            }

            var existUser = _usersRepository.Get(login);

            if (existUser != null)
            {
                return (null, "Пользователь с таким логином уже есть!!");
            }

            var idUser = _usersRepository.Create(userName, login, password);
            var userDomain = _usersRepository.Get(idUser);

            return (userDomain.ToDTO().UserDTO, message);
        }
    }
}
