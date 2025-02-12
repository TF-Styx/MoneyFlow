using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface IBanksRepository
    {
        Task<int> CreateAsync(string bankName);
        Task Delete(int idBank);
        Task<List<BankDomain>> GetAll();
        Task<BankDomain> Get(int idBank);
        Task<BankDomain> Get(string bankName);
        Task<int> Update(int idBank, string bankName);
    }
}