using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly Func<ContextMF> _factory;

        public SubcategoryRepository(Func<ContextMF> factory)
        {
            _factory = factory;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> CreateAsync(string? subcategoryName, string? description, byte[]? image)
        {
            using (var context = _factory())
            {
                var entity = new Subcategory()
                {
                    SubcategoryName = subcategoryName,
                    Description = description,
                    Image = image,
                };

                await context.AddAsync(entity);
                await context.SaveChangesAsync();

                return context.Subcategories.FirstOrDefault(x => x.SubcategoryName == subcategoryName).IdSubcategory;
            }
        }
        public int Create(string? subcategoryName, string? description, byte[]? image)
        {
            using (var context = _factory())
            {
                var entity = new Subcategory()
                {
                    SubcategoryName = subcategoryName,
                    Description = description,
                    Image = image,
                };

                context.Add(entity);
                context.SaveChanges();

                return context.Subcategories.FirstOrDefault(x => x.SubcategoryName == subcategoryName).IdSubcategory;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<SubcategoryDomain>> GetAllAsync()
        {
            using (var context = _factory())
            {
                var list = new List<SubcategoryDomain>();
                var entity = await context.Subcategories.ToListAsync();

                foreach (var item in entity)
                {
                    list.Add(SubcategoryDomain.Create(item.IdSubcategory, item.SubcategoryName, item.Description, item.Image).SubcategoryDomain);
                }

                return list;
            }
        }
        public List<SubcategoryDomain> GetAll()
        {
            using (var context = _factory())
            {
                var list = new List<SubcategoryDomain>();
                var entity = context.Subcategories.ToList();

                foreach (var item in entity)
                {
                    list.Add(SubcategoryDomain.Create(item.IdSubcategory, item.SubcategoryName, item.Description, item.Image).SubcategoryDomain);
                }

                return list;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public List<SubcategoryDomain> GetAllIdUserSub(int idUser)
        {
            using (var context = _factory())
            {
                var list = new List<SubcategoryDomain>();
                var entity = context.CatLinkSubs.Where(x => x.IdUser == idUser).Include(x => x.IdSubcategoryNavigation);

                foreach (var item in entity)
                {
                    list.Add(SubcategoryDomain.Create(item.IdSubcategoryNavigation.IdSubcategory, item.IdSubcategoryNavigation.SubcategoryName, item.IdSubcategoryNavigation.Description, item.IdSubcategoryNavigation.Image).SubcategoryDomain);
                }

                return list;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public List<SubcategoryDomain> GetIdUserIdCategorySub(int idUser, int idCategory)
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

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<SubcategoryDomain> GetAsync(int idSubcategory)
        {
            using (var context = _factory())
            {
                var entity = await context.Subcategories.FirstOrDefaultAsync(x => x.IdSubcategory == idSubcategory);
                var domain = SubcategoryDomain.Create(entity.IdSubcategory, entity.SubcategoryName, entity.Description, entity.Image).SubcategoryDomain;

                return domain;
            }
        }
        public SubcategoryDomain Get(int idSubcategory)
        {
            using (var context = _factory())
            {
                var entity = context.Subcategories.FirstOrDefault(x => x.IdSubcategory == idSubcategory);
                var domain = SubcategoryDomain.Create(entity.IdSubcategory, entity.SubcategoryName, entity.Description, entity.Image).SubcategoryDomain;

                return domain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<SubcategoryDomain> GetAsync(string subcategoryName)
        {
            using (var context = _factory())
            {
                var entity = await context.Subcategories.FirstOrDefaultAsync(x => x.SubcategoryName == subcategoryName);
                var domain = SubcategoryDomain.Create(entity.IdSubcategory, entity.SubcategoryName, entity.Description, entity.Image).SubcategoryDomain;

                return domain;
            }
        }
        public SubcategoryDomain Get(string subcategoryName)
        {
            using (var context = _factory())
            {
                var entity = context.Subcategories.FirstOrDefault(x => x.SubcategoryName == subcategoryName);
                var domain = SubcategoryDomain.Create(entity.IdSubcategory, entity.SubcategoryName, entity.Description, entity.Image).SubcategoryDomain;

                return domain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> UpdateAsync(int idSubcategory, string? subcategoryName, string? description, byte[]? image)
        {
            using (var context = _factory())
            {
                var entity = await context.Subcategories.FirstOrDefaultAsync(x => x.IdSubcategory == idSubcategory);

                entity.SubcategoryName = subcategoryName;
                entity.Description = description;
                entity.Image = image;

                context.Subcategories.Update(entity);
                context.SaveChanges();

                return idSubcategory;
            }
        }
        public int Update(int idSubcategory, string? subcategoryName, string? description, byte[]? image)
        {
            using (var context = _factory())
            {
                var entity = context.Subcategories.FirstOrDefault(x => x.IdSubcategory == idSubcategory);

                entity.SubcategoryName = subcategoryName;
                entity.Description = description;
                entity.Image = image;

                context.Subcategories.Update(entity);
                context.SaveChanges();

                return idSubcategory;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task DeleteAsync(int idSubcategory)
        {
            using (var context = _factory())
            {
                await context.Subcategories.Where(x => x.IdSubcategory == idSubcategory).ExecuteDeleteAsync();
            }
        }
        public void Delete(int idSubcategory)
        {
            using (var context = _factory())
            {
                context.Subcategories.Where(x => x.IdSubcategory == idSubcategory).ExecuteDelete();
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
