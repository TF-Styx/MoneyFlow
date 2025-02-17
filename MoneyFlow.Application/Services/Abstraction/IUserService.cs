using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface IUserService
    {
        Task<(UserDTO UserDTO, string Message)> CreateUser(string userName, string login, string password);
        Task DeleteUser(int idUser);
        Task<List<UserDTO>> GetAllUser();
        Task<UserDTO> GetUser(int idUser);
        Task<UserDTO> GetUser(string login);
        Task<int> UpdateUser(int idUser, string? userName, byte[]? avatar, string password, int? idGender);
    }
}