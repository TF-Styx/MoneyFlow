using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class CatLinkSubRepository : ICatLinkSubRepository
    {
        private readonly ContextMF _context;

        public CatLinkSubRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(int idUser, int idCategory, int idSubcategory)
        {
            var entity = new CatLinkSub()
            {
                IdUser = idUser,
                IdCategory = idCategory,
                IdSubcategory = idSubcategory,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return idCategory;
        }
        public int Create(int idUser, int idCategory, int idSubcategory)
        {
            var entity = new CatLinkSub()
            {
                IdUser = idUser,
                IdCategory = idCategory,
                IdSubcategory = idSubcategory,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            _context.Add(entity);
            _context.SaveChanges();

            return idCategory;
        }

        public List<SubcategoryDomain> GetAllSubcategory(int idUser, int idCategory)
        {
            var list = new List<SubcategoryDomain>();
            var entity = _context.CatLinkSubs.Where(x => x.IdUser == idUser && x.IdCategory == idCategory).Include(x => x.IdSubcategoryNavigation);

            foreach (var item in entity)
            {
                list.Add(SubcategoryDomain.Create(item.IdSubcategoryNavigation.IdSubcategory, item.IdSubcategoryNavigation.SubcategoryName, item.IdSubcategoryNavigation.Description, item.IdSubcategoryNavigation.Image).SubcategoryDomain);
            }

            return list;
        }

        public async Task DeleteAsync(int idUser, int idCategory, int idSubcategory)
        {
            await _context.CatLinkSubs.Where(x => x.IdUser == idUser && x.IdCategory == idCategory && x.IdSubcategory == idSubcategory).ExecuteDeleteAsync();
        }
    }
}
