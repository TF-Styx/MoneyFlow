using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.SubcategoryCaseInterfaces;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.SubcategoryCases
{
    public class CreateSubcategoryUseCase : ICreateSubcategoryUseCase
    {
        private readonly ISubcategoryRepository _subcategoryRepository;

        public CreateSubcategoryUseCase(ISubcategoryRepository subcategoryRepository)
        {
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task<(SubcategoryDTO SubcategoryDTO, string Message)> CreateAsyncSubcategory(string? subcategoryName, string? description, byte[]? image)
        {
            var message = string.Empty;

            if (string.IsNullOrWhiteSpace(subcategoryName))
            {
                return (null, "Вы не заполнили поря!!");
            }

            var exist = await _subcategoryRepository.GetAsync(subcategoryName);

            if (exist != null) { return (null, "Подкатегория с таким именем уже есть!!"); }

            var id = await _subcategoryRepository.CreateAsync(subcategoryName, description, image);
            var domain = await _subcategoryRepository.GetAsync(id);

            return (domain.ToDTO().SubcategoryDTO, message);
        }
        public (SubcategoryDTO SubcategoryDTO, string Message) CreateSubcategory(string? subcategoryName, string? description, byte[]? image)
        {
            var message = string.Empty;

            if (string.IsNullOrWhiteSpace(subcategoryName))
            {
                return (null, "Вы не заполнили поря!!");
            }

            var exist = _subcategoryRepository.Get(subcategoryName);

            if (exist != null) { return (null, "Подкатегория с таким именем уде есть!!"); }

            var id = _subcategoryRepository.Create(subcategoryName, description, image);
            var domain = _subcategoryRepository.Get(id);

            return (domain.ToDTO().SubcategoryDTO, message);
        }
    }
}
