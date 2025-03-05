using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Func<ContextMF> _factory;

        public CategoryRepository(Func<ContextMF> factory)
        {
            _factory = factory;
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> CreateAsync(string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            using (var context = _factory())
            {
                var entity = new Category()
                {
                    CategoryName = categoryName,
                    Description = description,
                    Color = color,
                    Image = image,
                    IdUser = idUser
                };

                await context.AddAsync(entity);
                await context.SaveChangesAsync();

                return context.Categories.FirstOrDefault(x => x.CategoryName == categoryName).IdCategory;
            }
        }
        public int Create(string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            using (var context = _factory())
            {
                var entity = new Category()
                {
                    CategoryName = categoryName,
                    Description = description,
                    Color = color,
                    Image = image,
                    IdUser = idUser
                };

                context.Add(entity);
                context.SaveChanges();

                return context.Categories.FirstOrDefault(x => x.CategoryName == categoryName).IdCategory;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<List<CategoryDomain>> GetAllAsync()
        {
            using (var context = _factory())
            {
                var list = new List<CategoryDomain>();
                var entity = await context.Categories.ToListAsync();

                foreach (var item in entity)
                {
                    list.Add(CategoryDomain.Create(item.IdCategory, item.CategoryName, item.Description, item.Color, item.Image, item.IdUser).CategoryDomain);
                }

                return list;
            }
        }
        public List<CategoryDomain> GetAll()
        {
            using (var context = _factory())
            {
                var list = new List<CategoryDomain>();
                var entity = context.Categories.ToList();

                foreach (var item in entity)
                {
                    list.Add(CategoryDomain.Create(item.IdCategory, item.CategoryName, item.Description, item.Color, item.Image, item.IdUser).CategoryDomain);
                }

                return list;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<CategoryDomain> GetAsync(int idCategory)
        {
            using (var context = _factory())
            {
                var entity = await context.Categories.FirstOrDefaultAsync(x => x.IdCategory == idCategory);
                var domain = CategoryDomain.Create(entity.IdCategory, entity.CategoryName, entity.Description, entity.Color, entity.Image, entity.IdUser).CategoryDomain;

                return domain;
            }
        }
        public CategoryDomain Get(int idCategory)
        {
            using (var context = _factory())
            {
                var entity = context.Categories.FirstOrDefault(x => x.IdCategory == idCategory);
                var domain = CategoryDomain.Create(entity.IdCategory, entity.CategoryName, entity.Description, entity.Color, entity.Image, entity.IdUser).CategoryDomain;

                return domain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<CategoryDomain> GetAsync(string categoryName)
        {
            using (var context = _factory())
            {
                var entity = await context.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryName);
                var domain = CategoryDomain.Create(entity.IdCategory, entity.CategoryName, entity.Description, entity.Color, entity.Image, entity.IdUser).CategoryDomain;

                return domain;
            }
        }
        public CategoryDomain Get(string categoryName)
        {
            using (var context = _factory())
            {
                var entity = context.Categories.FirstOrDefault(x => x.CategoryName == categoryName);
                var domain = CategoryDomain.Create(entity.IdCategory, entity.CategoryName, entity.Description, entity.Color, entity.Image, entity.IdUser).CategoryDomain;

                return domain;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task<int> UpdateAsync(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            using (var context = _factory())
            {
                var entity = await context.Categories.FirstOrDefaultAsync(x => x.IdCategory == idCategory);

                entity.CategoryName = categoryName;
                entity.Description = description;
                entity.Color = color;
                entity.Image = image;
                entity.IdUser = idUser;

                context.Categories.Update(entity);
                context.SaveChanges();

                return idCategory;
            }
        }
        public int Update(int idCategory, string? categoryName, string? description, string? color, byte[]? image, int idUser)
        {
            using (var context = _factory())
            {
                var entity = context.Categories.FirstOrDefault(x => x.IdCategory == idCategory);

                entity.CategoryName = categoryName;
                entity.Description = description;
                entity.Color = color;
                entity.Image = image;
                entity.IdUser = idUser;

                context.Categories.Update(entity);
                context.SaveChanges();

                return idCategory;
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------

        public async Task DeleteAsync(int idCategory)
        {
            using (var context = _factory())
            {
                await context.Categories.Where(x => x.IdCategory == idCategory).ExecuteDeleteAsync();
            }
        }
        public void Delete(int idCategory)
        {
            using (var context = _factory())
            {
                context.Categories.Where(x => x.IdCategory == idCategory).ExecuteDelete();
            }
        }

        // ------------------------------------------------------------------------------------------------------------------------------------------------
    }
}
