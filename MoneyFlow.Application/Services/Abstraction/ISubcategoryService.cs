using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.Services.Abstraction
{
    public interface ISubcategoryService
    {
        Task<(SubcategoryDTO SubcategoryDTO, string Message)> CreateAsyncSubcategory(string? subcategoryName, string? description, byte[]? image, int idUser);
        (SubcategoryDTO SubcategoryDTO, string Message) CreateSubcategory(string? subcategoryName, string? description, byte[]? image, int idUser);

        Task DeleteAsyncSubcategory(int idSubcategory);
        void DeleteSubcategory(int idSubcategory);

        Task<List<SubcategoryDTO>> GetAllAsyncSubcategory();
        List<SubcategoryDTO> GetAllSubcategory();

        List<SubcategoryDTO> GetAllIdUserSub(int idUser);

        List<SubcategoryDTO> GetIdUserIdCategorySub(int idUser, int idCategory);

        Task<SubcategoryDTO> GetAsyncSubcategory(int idSubcategory);
        Task<SubcategoryDTO> GetAsyncSubcategory(string subcategoryName);

        SubcategoryDTO GetSubcategory(int idSubcategory);
        SubcategoryDTO GetSubcategory(string subcategoryName);

        Task<int> UpdateAsyncSubcategory(int idSubcategory, string? subcategoryName, string? description, byte[]? image);
        int UpdateSubcategory(int idSubcategory, string? subcategoryName, string? description, byte[]? image);
    }
}