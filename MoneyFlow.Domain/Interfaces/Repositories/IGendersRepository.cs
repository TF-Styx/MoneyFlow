using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface IGendersRepository
    {
        Task<int> CreateAsync(string genderName);
        Task Delete(int idGender);
        Task<List<GenderDomain>> GetAll();
        Task<GenderDomain> Get(int idGender);
        Task<GenderDomain> Get(string genderName);
        Task<int> Update(int idGender, string genderName);
    }
}