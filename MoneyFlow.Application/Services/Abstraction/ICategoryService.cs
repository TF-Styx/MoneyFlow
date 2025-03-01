using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<(CategoryDTO CategoryDTO, string Message)> CreateAsyncCategory(string? categoryName, string? description, string? color, byte[]? image, int idUser);
        (CategoryDTO CategoryDTO, string Message) CreateCategory(string? categoryName, string? description, string? color, byte[]? image, int idUser);

        Task<List<CategoryDTO>> GetAllAsyncCategory();
        List<CategoryDTO> GetAllCategory();

        Task<CategoryDTO> GetAsyncCategory(int idCategory);
        CategoryDTO GetCategory(int idCategory);

        Task<CategoryDTO> GetAsyncCategory(string categoryName);
        CategoryDTO GetCategory(string categoryName);

        Task<int> UpdateAsync(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser);
        int Update(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser);

        Task DeleteAsyncCategory(int idCategory);
        void DeleteCategory(int idCategory);
    }
}