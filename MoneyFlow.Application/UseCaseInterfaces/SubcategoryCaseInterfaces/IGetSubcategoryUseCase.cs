using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.SubcategoryCaseInterfaces
{
    public interface IGetSubcategoryUseCase
    {
        Task<List<SubcategoryDTO>> GetAllAsyncSubcategory();
        List<SubcategoryDTO> GetAllSubcategory();

        List<SubcategoryDTO> GetAllIdUserSub(int idUser);

        List<SubcategoryDTO> GetIdUserIdCategorySub(int idUser, int idCategory);

        Task<SubcategoryDTO> GetAsyncSubcategory(int idSubcategory);
        SubcategoryDTO GetSubcategory(int idSubcategory);

        Task<SubcategoryDTO> GetAsyncSubcategory(string subcategoryName);
        SubcategoryDTO GetSubcategory(string subcategoryName);
    }
}