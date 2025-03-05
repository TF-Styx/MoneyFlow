using MoneyFlow.Domain.DomainModels;

namespace MoneyFlow.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        Task<int> CreateAsync(string? categoryName, string? description, string? color, byte[]? image, int idUser);
        int Create(string? categoryName, string? description, string? color, byte[]? image, int idUser);

        Task<List<CategoryDomain>> GetAllAsync();
        List<CategoryDomain> GetAll();

        int GetIdCat(int idUser);

        //Task<CategoryDomain> GetIdCatAsync(int idUser);
        //CategoryDomain GetIdCat(int idUser);

        Task<List<CategoryDomain>> GetCatAsync(int idUser);
        List<CategoryDomain> GetCat(int idUser);

        Task<CategoryDomain> GetAsync(int idCategory);
        CategoryDomain Get(int idCategory);

        Task<CategoryDomain> GetAsync(string categoryName);
        CategoryDomain Get(string categoryName);

        Task<int> UpdateAsync(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser);
        int Update(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser);

        Task DeleteAsync(int idCategory);
        void Delete(int idCategory);
    }
}