using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.UserCases
{
    public class GetUserUseCase : IGetUserUseCase
    {
        private readonly IUsersRepository _usersRepository;

        public GetUserUseCase(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<List<UserDTO>> GetAllUser()
        {
            var users = await _usersRepository.GetAll();
            var usersDTO = users.ToListDTO();

            return usersDTO;
        }

        public async Task<UserDTO> GetUser(int idUser)
        {
            var user = await _usersRepository.Get(idUser);

            if (user == null) { return null; } // TODO : Сделать исключение

            var userDTO = user.ToDTO();

            return userDTO.UserDTO;
        }

        public async Task<UserDTO> GetUser(string login)
        {
            var user = await _usersRepository.Get(login);

            if (user == null) { return null; } // TODO : Сделать исключение

            var userDTO = user.ToDTO();

            return userDTO.UserDTO;
        }
    }
}
