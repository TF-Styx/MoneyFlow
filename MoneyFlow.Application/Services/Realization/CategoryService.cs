﻿using MoneyFlow.Application.DTOs;
using MoneyFlow.Application.Services.Abstraction;
using MoneyFlow.Application.UseCaseInterfaces.CategoryCaseInterfaces;

namespace MoneyFlow.Application.Services.Realization
{
    public class CategoryService : ICategoryService
    {
        private readonly ICreateCategoryUseCase _createCategoryUseCase;
        private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;
        private readonly IGetCategoryUseCase _getCategoryUseCase;
        private readonly IUpdateCategoryUseCase _updateCategoryUseCase;

        public CategoryService(ICreateCategoryUseCase createCategoryUseCase, IDeleteCategoryUseCase deleteCategoryUseCase, IGetCategoryUseCase getCategoryUseCase, IUpdateCategoryUseCase updateCategoryUseCase)
        {
            _createCategoryUseCase = createCategoryUseCase;
            _deleteCategoryUseCase = deleteCategoryUseCase;
            _getCategoryUseCase = getCategoryUseCase;
            _updateCategoryUseCase = updateCategoryUseCase;
        }

        public async Task<(CategoryDTO CategoryDTO, string Message)> CreateAsyncCategory(string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            return await _createCategoryUseCase.CreateAsyncCategory(categoryName, description, color, image, idUser);
        }
        public (CategoryDTO CategoryDTO, string Message) CreateCategory(string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            return _createCategoryUseCase.CreateCategory(categoryName, description, color, image, idUser);
        }

        public async Task<List<CategoryDTO>> GetAllAsyncCategory()
        {
            return await _getCategoryUseCase.GetAllAsyncCategory();
        }
        public List<CategoryDTO> GetAllCategory()
        {
            return _getCategoryUseCase.GetAllCategory();
        }

        public int GetIdCat(int idUser)
        {
            return _getCategoryUseCase.GetIdCat(idUser);
        }
        public int GetIdSubCat(int idUser, int idSub)
        {
            return _getCategoryUseCase.GetIdSubCat(idUser, idSub);
        }

        //public async Task<CategoryDTO> GetIdCatAsync(int idUser)
        //{
        //    return await _getCategoryUseCase.GetIdCatAsync(idUser);
        //}
        //public CategoryDTO GetIdCat(int idUser)
        //{
        //    return _getCategoryUseCase.GetIdCat(idUser);
        //}

        public async Task<List<CategoryDTO>> GetCatAsyncCategory(int idUser)
        {
            return await _getCategoryUseCase.GetCatAsyncCategory(idUser);
        }
        public List<CategoryDTO> GetCatCategory(int idUser)
        {
            return _getCategoryUseCase.GetCatCategory(idUser);
        }

        public async Task<CategoryDTO> GetAsyncCategory(int idCategory)
        {
            return await _getCategoryUseCase.GetAsyncCategory(idCategory);
        }
        public CategoryDTO GetCategory(int idCategory)
        {
            return _getCategoryUseCase.GetCategory(idCategory);
        }

        public async Task<CategoryDTO> GetAsyncCategory(string categoryName)
        {
            return await _getCategoryUseCase.GetAsyncCategory(categoryName);
        }
        public CategoryDTO GetCategory(string categoryName)
        {
            return _getCategoryUseCase.GetCategory(categoryName);
        }

        public async Task<int> UpdateAsyncCategory(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            return await _updateCategoryUseCase.UpdateAsyncCategory(idCategory, categoryName, description, color, image, idUser);
        }
        public int UpdateCategory(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            return _updateCategoryUseCase.UpdateCategory(idCategory, categoryName, description, color, image, idUser);
        }

        public async Task DeleteAsyncCategory(int idCategory)
        {
            await _deleteCategoryUseCase.DeleteAsyncCategory(idCategory);
        }
        public void DeleteCategory(int idCategory)
        {
            _deleteCategoryUseCase.DeleteCategory(idCategory);
        }
    }
}
