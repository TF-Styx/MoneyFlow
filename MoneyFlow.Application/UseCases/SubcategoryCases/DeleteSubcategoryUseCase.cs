using MoneyFlow.Application.UseCaseInterfaces.SubcategoryCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.SubcategoryCases
{
    public class DeleteSubcategoryUseCase : IDeleteSubcategoryUseCase
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public DeleteSubcategoryUseCase(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task DeleteAsync(int idSubcategory)
        {
            await _subcategoryRepository.DeleteAsync(idSubcategory);
        }
        public void Delete(int idSubcategory)
        {
            _subcategoryRepository.Delete(idSubcategory);
        }
    }
}
