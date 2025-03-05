﻿using MoneyFlow.Application.DTOs;

namespace MoneyFlow.Application.UseCaseInterfaces.CategoryCaseInterfaces
{
    public interface IGetCategoryUseCase
    {
        Task<List<CategoryDTO>> GetAllAsyncCategory();
        List<CategoryDTO> GetAllCategory();

        int GetIdCat(int idUser);

        //Task<CategoryDTO> GetIdCatAsync(int idUser);
        //CategoryDTO GetIdCat(int idUser);

        Task<List<CategoryDTO>> GetCatAsyncCategory(int idUser);
        List<CategoryDTO> GetCatCategory(int idUser);

        Task<CategoryDTO> GetAsyncCategory(int idCategory);
        CategoryDTO GetCategory(int idCategory);

        Task<CategoryDTO> GetAsyncCategory(string categoryName);
        CategoryDTO GetCategory(string categoryName);
    }
}