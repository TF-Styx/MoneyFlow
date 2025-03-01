using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.CategoryCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.CategoryCases
{
    public class GetCategoryUseCase : IGetCategoryUseCase
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoryUseCase(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryDTO>> GetAllAsyncCategory()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var toDTO = categories.ToListDTO();

            return toDTO;
        }
        public List<CategoryDTO> GetAllCategory()
        {
            var categories = _categoryRepository.GetAll();
            var toDTO = categories.ToListDTO();

            return toDTO;
        }

        public async Task<CategoryDTO> GetAsyncCategory(int idCategory)
        {
            var categories = await _categoryRepository.GetAsync(idCategory);

            if (categories == null) { return null; }

            var toDTO = categories.ToDTO();

            return toDTO.CategoryDTO;
        }
        public CategoryDTO GetCategory(int idCategory)
        {
            var categories = _categoryRepository.Get(idCategory);

            if (categories == null) { return null; }

            var toDTO = categories.ToDTO();

            return toDTO.CategoryDTO;
        }

        public async Task<CategoryDTO> GetAsyncCategory(string categoryName)
        {
            var categories = await _categoryRepository.GetAsync(categoryName);

            if (categories == null) { return null; }

            var toDTO = categories.ToDTO();

            return toDTO.CategoryDTO;
        }
        public CategoryDTO GetCategory(string categoryName)
        {
            var categories = _categoryRepository.Get(categoryName);

            if (categories == null) { return null; }

            var toDTO = categories.ToDTO();

            return toDTO.CategoryDTO;
        }
    }
}
