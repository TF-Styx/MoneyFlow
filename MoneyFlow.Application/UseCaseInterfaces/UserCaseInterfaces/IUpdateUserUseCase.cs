namespace MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces
{
    public interface IUpdateUserUseCase
    {
        Task<int> UpdateUser(int idUser, string? userName, byte[]? avatar, string password, int? idGender);
    }
}