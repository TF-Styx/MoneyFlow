namespace MoneyFlow.Application.UseCaseInterfaces.UserCaseInterfaces
{
    public interface IDeleteUserUseCase
    {
        Task DeleteUser(int idUser);
    }
}