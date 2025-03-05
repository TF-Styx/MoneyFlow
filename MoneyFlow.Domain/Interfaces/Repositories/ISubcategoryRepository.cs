using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface ISubcategoryRepository
    {
        Task<int> CreateAsync(string? subcategoryName, string? description, byte[]? image);
        int Create(string? subcategoryName, string? description, byte[]? image);

        Task<List<SubcategoryDomain>> GetAllAsync();
        List<SubcategoryDomain> GetAll();

        List<SubcategoryDomain> GetAllIdUserSub(int idUser);

        List<SubcategoryDomain> GetIdUserIdCategorySub(int idUser, int idCategory);

        Task<SubcategoryDomain> GetAsync(int idSubcategory);
        SubcategoryDomain Get(int idSubcategory);
        
        Task<SubcategoryDomain> GetAsync(string subcategoryName);
        SubcategoryDomain Get(string subcategoryName);

        Task<int> UpdateAsync(int idSubcategory, string? subcategoryName, string? description, byte[]? image);
        int Update(int idSubcategory, string? subcategoryName, string? description, byte[]? image);

        Task DeleteAsync(int idSubcategory);
        void Delete(int idSubcategory);
    }
}