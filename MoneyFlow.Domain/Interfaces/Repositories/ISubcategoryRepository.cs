using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface ISubcategoryRepository
    {
        int Create(string? subcategoryName, string? description, byte[]? image, int idCategory);
        Task<int> CreateAsync(string? subcategoryName, string? description, byte[]? image, int idCategory);
        void Delete(int idSubcategory);
        Task DeleteAsync(int idSubcategory);
        SubcategoryDomain Get(int idSubcategory);
        SubcategoryDomain Get(string subcategoryName);
        List<SubcategoryDomain> GetAll();
        Task<List<SubcategoryDomain>> GetAllAsync();
        Task<SubcategoryDomain> GetAsync(int idSubcategory);
        Task<SubcategoryDomain> GetAsync(string subcategoryName);
        int Update(int idSubcategory, string? subcategoryName, string? description, byte[]? image, int idCategory);
        Task<int> UpdateAsync(int idSubcategory, string? subcategoryName, string? description, byte[]? image, int idCategory);
    }
}