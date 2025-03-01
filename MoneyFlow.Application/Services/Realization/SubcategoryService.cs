using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.SubcategoryCaseInterfaces;

namespace MoneyFlow.Application.Services.Realization
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ICreateSubcategoryUseCase _createSubcategoryUseCase;
        private readonly IDeleteSubcategoryUseCase _deleteSubcategoryUseCase;
        private readonly IGetSubcategoryUseCase _getSubcategoryUseCase;
        private readonly IUpdateSubcategoryUseCase _updateSubcategoryUseCase;

        public SubcategoryService(ICreateSubcategoryUseCase createSubcategoryUseCase, IDeleteSubcategoryUseCase deleteSubcategoryUseCase, IGetSubcategoryUseCase getSubcategoryUseCase, IUpdateSubcategoryUseCase updateSubcategoryUseCase)
        {
            _createSubcategoryUseCase = createSubcategoryUseCase;
            _deleteSubcategoryUseCase = deleteSubcategoryUseCase;
            _getSubcategoryUseCase = getSubcategoryUseCase;
            _updateSubcategoryUseCase = updateSubcategoryUseCase;
        }

        public async Task<(SubcategoryDTO SubcategoryDTO, string Message)> CreateAsyncSubcategory(string? subcategoryName, string? description, byte[]? image)
        {
            return await _createSubcategoryUseCase.CreateAsyncSubcategory(subcategoryName, description, image);
        }
        public (SubcategoryDTO SubcategoryDTO, string Message) CreateSubcategory(string? subcategoryName, string? description, byte[]? image)
        {
            return _createSubcategoryUseCase.CreateSubcategory(subcategoryName, description, image);
        }

        public async Task<List<SubcategoryDTO>> GetAllAsyncSubcategory()
        {
            return await _getSubcategoryUseCase.GetAllAsyncSubcategory();
        }
        public List<SubcategoryDTO> GetAllSubcategory()
        {
            return _getSubcategoryUseCase.GetAllSubcategory();
        }

        public async Task<SubcategoryDTO> GetAsyncSubcategory(int idSubcategory)
        {
            return await _getSubcategoryUseCase.GetAsyncSubcategory(idSubcategory);
        }
        public SubcategoryDTO GetSubcategory(int idSubcategory)
        {
            return _getSubcategoryUseCase.GetSubcategory(idSubcategory);
        }

        public async Task<SubcategoryDTO> GetAsyncSubcategory(string subcategoryName)
        {
            return await _getSubcategoryUseCase.GetAsyncSubcategory(subcategoryName);
        }
        public SubcategoryDTO GetSubcategory(string subcategoryName)
        {
            return _getSubcategoryUseCase.GetSubcategory(subcategoryName);
        }

        public async Task<int> UpdateAsyncSubcategory(int idSubcategory, string? subcategoryName, string? description, byte[]? image)
        {
            return await _updateSubcategoryUseCase.UpdateAsyncSubcategory(idSubcategory, subcategoryName, description, image);
        }
        public int UpdateSubcategory(int idSubcategory, string? subcategoryName, string? description, byte[]? image)
        {
            return _updateSubcategoryUseCase.UpdateSubcategory(idSubcategory, subcategoryName, description, image);
        }

        public async Task DeleteAsyncSubcategory(int idSubcategory)
        {
            await _deleteSubcategoryUseCase.DeleteAsync(idSubcategory);
        }
        public void DeleteSubcategory(int idSubcategory)
        {
            _deleteSubcategoryUseCase.Delete(idSubcategory);
        }
    }
}
