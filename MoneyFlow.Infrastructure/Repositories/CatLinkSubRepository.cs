using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class CatLinkSubRepository : ICatLinkSubRepository
    {
        private readonly Func<ContextMF> _factory;

        public CatLinkSubRepository(Func<ContextMF> factory)
        {
            _factory = factory;
        }

        public async Task<int> CreateAsync(int idUser, int idCategory, int idSubcategory)
        {
            using (var context = _factory())
            {
                var entity = new CatLinkSub()
                {
                    IdUser = idUser,
                    IdCategory = idCategory,
                    IdSubcategory = idSubcategory,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                await context.AddAsync(entity);
                await context.SaveChangesAsync();

                return idCategory;
            }
        }
        public int Create(int idUser, int idCategory, int idSubcategory)
        {
            using (var context = _factory())
            {
                var entity = new CatLinkSub()
                {
                    IdUser = idUser,
                    IdCategory = idCategory,
                    IdSubcategory = idSubcategory,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };

                context.Add(entity);
                context.SaveChanges();

                return idCategory;
            }
        }

        public List<SubcategoryDomain> GetAllSubcategory(int idUser, int idCategory)
        {
            using (var context = _factory())
            {
                var list = new List<SubcategoryDomain>();
                var entity = context.CatLinkSubs.Where(x => x.IdUser == idUser && x.IdCategory == idCategory).Include(x => x.IdSubcategoryNavigation);

                foreach (var item in entity)
                {
                    list.Add(SubcategoryDomain.Create(item.IdSubcategoryNavigation.IdSubcategory, item.IdSubcategoryNavigation.SubcategoryName, item.IdSubcategoryNavigation.Description, item.IdSubcategoryNavigation.Image).SubcategoryDomain);
                }

                return list;
            }
        }

        public async Task DeleteAsync(int idUser, int idCategory, int idSubcategory)
        {
            using (var context = _factory())
            {
                await context.CatLinkSubs.Where(x => x.IdUser == idUser && x.IdCategory == idCategory && x.IdSubcategory == idSubcategory).ExecuteDeleteAsync();
            }
        }
    }
}
