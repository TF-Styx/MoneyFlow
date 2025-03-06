using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Mappers;
using MoneyFlow.Application.UseCaseInterfaces.SubcategoryCaseInterfaces;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;

namespace MoneyFlow.Application.UseCases.SubcategoryCases
{
    public class CreateSubcategoryUseCase : ICreateSubcategoryUseCase
    {
        private readonly ISubcategoryRepository _subcategoryRepository;
        private readonly ICatLinkSubRepository _catLinkSubRepository;

        public CreateSubcategoryUseCase(ISubcategoryRepository subcategoryRepository, ICatLinkSubRepository catLinkSubRepository)
        {
            _subcategoryRepository = subcategoryRepository;
            _catLinkSubRepository = catLinkSubRepository;
        }

        public async Task<(SubcategoryDTO SubcategoryDTO, string Message)> CreateAsyncSubcategory(string? subcategoryName, string? description, byte[]? image)
        {
            var (CreateSubcategoryDomain, Message) = SubcategoryDomain.Create(0, subcategoryName, null, null);

            //var exist = await _subcategoryRepository.GetAsync(subcategoryName);

            //if (exist != null) { return (null, "Подкатегория с таким именем уже есть!!"); }

            var id = await _subcategoryRepository.CreateAsync(subcategoryName, description, image);
            var domain = await _subcategoryRepository.GetAsync(id);

            return (domain.ToDTO().SubcategoryDTO, Message);
        }
        public (SubcategoryDTO SubcategoryDTO, string Message) CreateSubcategory(string? subcategoryName, string? description, byte[]? image)
        {
            var (CreateSubcategoryDomain, Message) = SubcategoryDomain.Create(0, subcategoryName, null, null);

            //var exist = _subcategoryRepository.Get(subcategoryName);

            //if (exist != null) { return (null, "Подкатегория с таким именем уде есть!!"); }

            var id = _subcategoryRepository.Create(subcategoryName, description, image);
            var domain = _subcategoryRepository.Get(id);

            return (domain.ToDTO().SubcategoryDTO, Message);
        }

        // TODO : Додумать
        //public async Task<(CategoryDTO CategoryDTO, SubcategoryDTO SubcategoryDTO, string Message)> CreateAsyncCatLinkSub(int idUser, int idCategory, int idSubcategory)
        //{
        //    var catLinSub = CatLinkSubDomain.Create(idUser, idCategory, idSubcategory);

        //    var id = await _catLinkSubRepository.CreateAsync(idUser, idCategory, idSubcategory);
        //}
    }
}
