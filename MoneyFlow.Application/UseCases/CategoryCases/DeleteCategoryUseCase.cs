using MoneyFlow.Application.UseCaseInterfaces.CategoryCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.CategoryCases
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task DeleteAsyncCategory(int idCategory)
        {
            await _categoryRepository.DeleteAsync(idCategory);
        }
        public void DeleteCategory(int idCategory)
        {
            _categoryRepository.Delete(idCategory);
        }
    }
}
