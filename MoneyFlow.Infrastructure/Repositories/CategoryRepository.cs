using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ContextMF _context;

        public CategoryRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            var entity = new Category()
            {
                CategoryName = categoryName,
                Description = description,
                Color = color,
                Image = image,
                IdUser = idUser
            };

            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            return _context.Categories.FirstOrDefault(x => x.CategoryName == categoryName).IdCategory;
        }
        public int Create(string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            var entity = new Category()
            {
                CategoryName = categoryName,
                Description = description,
                Color = color,
                Image = image,
                IdUser = idUser
            };

            _context.Add(entity);
            _context.SaveChanges();

            return _context.Categories.FirstOrDefault(x => x.CategoryName == categoryName).IdCategory;
        }

        public async Task<List<CategoryDomain>> GetAllAsync()
        {
            var list = new List<CategoryDomain>();
            var entity = await _context.Categories.ToListAsync();

            foreach (var item in entity)
            {
                list.Add(CategoryDomain.Create(item.IdCategory, item.CategoryName, item.Description, item.Color, item.Image, item.IdUser).CategoryDomain);
            }

            return list;
        }
        public List<CategoryDomain> GetAll()
        {
            var list = new List<CategoryDomain>();
            var entity = _context.Categories.ToList();

            foreach (var item in entity)
            {
                list.Add(CategoryDomain.Create(item.IdCategory, item.CategoryName, item.Description, item.Color, item.Image, item.IdUser).CategoryDomain);
            }

            return list;
        }

        public async Task<CategoryDomain> GetAsync(int idCategory)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(x => x.IdCategory == idCategory);
            var domain = CategoryDomain.Create(entity.IdCategory, entity.CategoryName, entity.Description, entity.Color, entity.Image, entity.IdUser).CategoryDomain;

            return domain;
        }
        public CategoryDomain Get(int idCategory)
        {
            var entity = _context.Categories.FirstOrDefault(x => x.IdCategory == idCategory);
            var domain = CategoryDomain.Create(entity.IdCategory, entity.CategoryName, entity.Description, entity.Color, entity.Image, entity.IdUser).CategoryDomain;

            return domain;
        }

        public async Task<CategoryDomain> GetAsync(string categoryName)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);
            var domain = CategoryDomain.Create(entity.IdCategory, entity.CategoryName, entity.Description, entity.Color, entity.Image, entity.IdUser).CategoryDomain;

            return domain;
        }
        public CategoryDomain Get(string categoryName)
        {
            var entity = _context.Categories.FirstOrDefault(x => x.CategoryName == categoryName);
            var domain = CategoryDomain.Create(entity.IdCategory, entity.CategoryName, entity.Description, entity.Color, entity.Image, entity.IdUser).CategoryDomain;

            return domain;
        }

        public async Task<int> UpdateAsync(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(x => x.IdCategory == idCategory);

            entity.CategoryName = categoryName;
            entity.Description = description;
            entity.Color = color;
            entity.Image = image;
            entity.IdUser = idUser;

            _context.Categories.Update(entity);
            _context.SaveChanges();

            return idCategory;
        }
        public int Update(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            var entity = _context.Categories.FirstOrDefault(x => x.IdCategory == idCategory);

            entity.CategoryName = categoryName;
            entity.Description = description;
            entity.Color = color;
            entity.Image = image;
            entity.IdUser = idUser;

            _context.Categories.Update(entity);
            _context.SaveChanges();

            return idCategory;
        }

        public async Task DeleteAsync(int idCategory)
        {
            await _context.Categories.Where(x => x.IdCategory == idCategory).ExecuteDeleteAsync();
        }
        public void Delete(int idCategory)
        {
            _context.Categories.Where(x => x.IdCategory == idCategory).ExecuteDelete();
        }
    }
}
