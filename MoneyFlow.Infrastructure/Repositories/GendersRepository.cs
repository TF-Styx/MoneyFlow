using Microsoft.EntityFrameworkCore;
using MoneyFlow.Domain.DomainModels;
using MoneyFlow.Domain.Interfaces.Repositories;
using MoneyFlow.Infrastructure.Context;
using MoneyFlow.Infrastructure.EntityModel;

namespace MoneyFlow.Infrastructure.Repositories
{
    public class GendersRepository : IGendersRepository
    {
        private readonly ContextMF _context;

        public GendersRepository(ContextMF context)
        {
            _context = context;
        }

        public async Task<int> CreateAsync(string genderName)
        {
            var genderEntity = new Gender
            {
                GenderName = genderName,
            };

            await _context.AddAsync(genderEntity);
            await _context.SaveChangesAsync();

            return _context.Genders.FirstOrDefault(x => x.GenderName == genderName).IdGender;
        }

        public async Task<List<GenderDomain>> GetAll()
        {
            var genderList = new List<GenderDomain>();
            var genderEntities = await _context.Genders.ToListAsync();

            foreach (var item in genderEntities)
            {
                genderList.Add(new GenderDomain()
                {
                    IdGender = item.IdGender,
                    GenderName = item.GenderName,
                });
            }

            return genderList;
        }

        public async Task<GenderDomain> Get(int idGender)
        {
            var genderEntity = await _context.Genders.FirstOrDefaultAsync(x => x.IdGender == idGender);
            var genderDomain = new GenderDomain()
            {
                IdGender = genderEntity.IdGender,
                GenderName = genderEntity.GenderName,
            };

            return genderDomain;
        }

        public async Task<int> Update(int idGender, string genderName)
        {
            await _context.Genders
                .Where(x => x.IdGender == idGender)
                    .ExecuteUpdateAsync(x => x
                        .SetProperty(x => x.GenderName, x => genderName));

            return idGender;
        }

        public async Task Delete(int idGender)
        {
            await _context.Genders.Where(x => x.IdGender == idGender).ExecuteDeleteAsync();
        }
    }
}
