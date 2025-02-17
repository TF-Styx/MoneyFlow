using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface IUsersRepository
    {
        Task<int> CreateAsync(string userName, string login, string password);
        Task Delete(int idUser);
        Task<UserDomain> Get(int idUser);
        Task<UserDomain> Get(string login);
        Task<List<UserDomain>> GetAll();
        Task<int> Update(int idUser, string? userName, byte[]? avatar, string password, int? idGender);
    }
}