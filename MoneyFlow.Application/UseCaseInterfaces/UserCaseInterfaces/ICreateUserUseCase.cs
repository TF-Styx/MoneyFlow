using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces
{
    public interface ICreateUserUseCase
    {
        Task<(UserDTO UserDTO, string Message)> CreateUser(string userName, string login, string password);
    }
}