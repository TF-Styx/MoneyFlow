namespace MoneyFlow.Application.UseCaseInterfaces.CategoryCaseInterfaces
{
    public interface IDeleteCategoryUseCase
    {
        Task DeleteAsyncCategory(int idCategory);
        void DeleteCategory(int idCategory);
    }
}