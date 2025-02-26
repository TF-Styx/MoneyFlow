namespace MoneyFlow.Application.UseCaseInterfaces.SubcategoryCaseInterfaces
{
    public interface IDeleteSubcategoryUseCase
    {
        Task DeleteAsync(int idSubcategory);
        void Delete(int idSubcategory);
    }
}