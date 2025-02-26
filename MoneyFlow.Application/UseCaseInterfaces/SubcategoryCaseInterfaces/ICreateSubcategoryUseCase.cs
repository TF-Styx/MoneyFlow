using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.SubcategoryCaseInterfaces
{
    public interface ICreateSubcategoryUseCase
    {
        Task<(SubcategoryDTO SubcategoryDTO, string Message)> CreateAsyncSubcategory(string? subcategoryName, string? description, byte[]? image, int idCategory);
        (SubcategoryDTO SubcategoryDTO, string Message) CreateSubcategory(string? subcategoryName, string? description, byte[]? image, int idCategory);
    }
}