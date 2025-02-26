using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly ContextMF _context;

        public SubcategoryRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string? subcategoryName, string? description, byte[]? image, int idCategory)
        {
            var entity = new Subcategory()
            {
                SubcategoryName = subcategoryName,
                Description = description,
                Image = image,
                IdCategory = idCategory
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _context.Subcategories.FirstOrDefault(x => x.SubcategoryName == subcategoryName).IdSubcategory;
        }
        public int Create(string? subcategoryName, string? description, byte[]? image, int idCategory)
        {
            var entity = new Subcategory()
            {
                SubcategoryName = subcategoryName,
                Description = description,
                Image = image,
                IdCategory = idCategory
            };

            _context.Add(entity);
            _context.SaveChanges();

            return _context.Subcategories.FirstOrDefault(x => x.SubcategoryName == subcategoryName).IdSubcategory;
        }

        public async Task<List<SubcategoryDomain>> GetAllAsync()
        {
            var list = new List<SubcategoryDomain>();
            var entity = await _context.Subcategories.ToListAsync();

            foreach (var item in entity)
            {
                list.Add(new SubcategoryDomain()
                {
                    IdSubcategory = item.IdSubcategory,
                    SubcategoryName = item.SubcategoryName,
                    Description = item.Description,
                    Image = item.Image,
                    IdCategory = item.IdCategory
                });
            }

            return list;
        }
        public List<SubcategoryDomain> GetAll()
        {
            var list = new List<SubcategoryDomain>();
            var entity = _context.Subcategories.ToList();

            foreach (var item in entity)
            {
                list.Add(new SubcategoryDomain()
                {
                    IdSubcategory = item.IdSubcategory,
                    SubcategoryName = item.SubcategoryName,
                    Description = item.Description,
                    Image = item.Image,
                    IdCategory = item.IdCategory
                });
            }

            return list;
        }

        public async Task<SubcategoryDomain> GetAsync(int idSubcategory)
        {
            var entity = await _context.Subcategories.FirstOrDefaultAsync(x => x.IdSubcategory == idSubcategory);
            var domain = new SubcategoryDomain()
            {
                IdSubcategory = entity.IdSubcategory,
                SubcategoryName = entity.SubcategoryName,
                Description = entity.Description,
                Image = entity.Image,
                IdCategory = entity.IdCategory
            };

            return domain;
        }
        public SubcategoryDomain Get(int idSubcategory)
        {
            var entity = _context.Subcategories.FirstOrDefault(x => x.IdSubcategory == idSubcategory);
            var domain = new SubcategoryDomain()
            {
                IdSubcategory = entity.IdSubcategory,
                SubcategoryName = entity.SubcategoryName,
                Description = entity.Description,
                Image = entity.Image,
                IdCategory = entity.IdCategory
            };

            return domain;
        }

        public async Task<SubcategoryDomain> GetAsync(string subcategoryName)
        {
            var entity = await _context.Subcategories.FirstOrDefaultAsync(x => x.SubcategoryName == subcategoryName);
            var domain = new SubcategoryDomain()
            {
                IdSubcategory = entity.IdSubcategory,
                SubcategoryName = entity.SubcategoryName,
                Description = entity.Description,
                Image = entity.Image,
                IdCategory = entity.IdCategory
            };

            return domain;
        }
        public SubcategoryDomain Get(string subcategoryName)
        {
            var entity = _context.Subcategories.FirstOrDefault(x => x.SubcategoryName == subcategoryName);
            var domain = new SubcategoryDomain()
            {
                IdSubcategory = entity.IdSubcategory,
                SubcategoryName = entity.SubcategoryName,
                Description = entity.Description,
                Image = entity.Image,
                IdCategory = entity.IdCategory
            };

            return domain;
        }

        public async Task<int> UpdateAsync(int idSubcategory, string? subcategoryName, string? description, byte[]? image, int idCategory)
        {
            var entity = await _context.Subcategories.FirstOrDefaultAsync(x => x.IdSubcategory == idSubcategory);

            entity.SubcategoryName = subcategoryName;
            entity.Description = description;
            entity.Image = image;
            entity.IdCategory = idCategory;

            _context.Subcategories.Update(entity);
            _context.SaveChanges();

            return idSubcategory;
        }
        public int Update(int idSubcategory, string? subcategoryName, string? description, byte[]? image, int idCategory)
        {
            var entity = _context.Subcategories.FirstOrDefault(x => x.IdSubcategory == idSubcategory);

            entity.SubcategoryName = subcategoryName;
            entity.Description = description;
            entity.Image = image;
            entity.IdCategory = idCategory;

            _context.Subcategories.Update(entity);
            _context.SaveChanges();

            return idSubcategory;
        }

        public async Task DeleteAsync(int idSubcategory)
        {
            await _context.Subcategories.Where(x => x.IdSubcategory == idSubcategory).ExecuteDeleteAsync();
        }
        public void Delete(int idSubcategory)
        {
            _context.Subcategories.Where(x => x.IdSubcategory == idSubcategory).ExecuteDelete();
        }
    }
}
