using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces;

namespace MoneyFlow.Application.Services.Realization
{
    public class UserService : IUserService
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly IDeleteUserUseCase _deleteUserCase;
        private readonly IGetUserUseCase    _getUserUseCase;
        private readonly IUpdateUserUseCase _updateUserUseCase;

        public UserService(ICreateUserUseCase createUserUseCase, IDeleteUserUseCase deleteUserCase, IGetUserUseCase getUserUseCase, IUpdateUserUseCase updateUserUseCase)
        {
            _createUserUseCase = createUserUseCase;
            _deleteUserCase    = deleteUserCase;
            _getUserUseCase    = getUserUseCase;
            _updateUserUseCase = updateUserUseCase;
        }

        public async Task<(UserDTO UserDTO, string Message)> CreateUser(string userName, string login, string password)
        {
            return await _createUserUseCase.CreateUser(userName, login, password);
        }

        public async Task DeleteUser(int idUser)
        {
            await _deleteUserCase.DeleteUser(idUser);
        }

        public async Task<List<UserDTO>> GetAllUser()
        {
            return await _getUserUseCase.GetAllUser();
        }

        public async Task<UserDTO> GetUser(int idUser)
        {
            return await _getUserUseCase.GetUser(idUser);
        }

        public async Task<UserDTO> GetUser(string login)
        {
            return await _getUserUseCase.GetUser(login);
        }

        public async Task<int> UpdateUser(int idUser, string? userName, byte[]? avatar,
                                      string password, int? idGender)
        {
            return await _updateUserUseCase.UpdateUser(idUser, userName, avatar, password, idGender);
        }
    }
}       
        