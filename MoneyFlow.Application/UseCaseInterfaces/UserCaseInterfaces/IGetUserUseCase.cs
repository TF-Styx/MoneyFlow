using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces
{
    public interface IGetUserUseCase
    {
        Task<List<UserDTO>> GetAllUser();
        Task<UserDTO> GetUser(int idUser);
        Task<UserDTO> GetUser(string login);
    }
}