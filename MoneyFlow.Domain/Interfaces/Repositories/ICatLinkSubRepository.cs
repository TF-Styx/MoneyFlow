using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface ICatLinkSubRepository
    {
        Task<int> CreateAsync(int idUser, int idCategory, int idSubcategory);
        int Create(int idUser, int idCategory, int idSubcategory);

        List<SubcategoryDomain> GetAllSubcategory(int idUser, int idCategory);

        Task DeleteAsync(int idUser, int idCategory, int idSubcategory);
    }
}