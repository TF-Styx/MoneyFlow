using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.CategoryCaseInterfaces
{
    public interface ICreateCategoryUseCase
    {
        Task<(CategoryDTO CategoryDTO, string Message)> CreateAsyncCategory(string? categoryName, string? description, string? color, byte[]? image, int idUser);
        (CategoryDTO CategoryDTO, string Message) CreateCategory(string? categoryName, string? description, string? color, byte[]? image, int idUser);
    }
}