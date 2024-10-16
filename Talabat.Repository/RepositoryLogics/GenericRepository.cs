using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository.RepositoryLogics
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly TalabatDbContext _dbContext;

        public GenericRepository(TalabatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if(typeof(T) == typeof(Product))
                return(IReadOnlyList<T>) await _dbContext
                            .Set<Product>()
                            .Include(p=>p.ProductType)
                            .Include(p=>p.ProductBrand)
                            .ToListAsync();
            return await _dbContext .Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (typeof(T) == typeof(Product))
                return await _dbContext
                            .Set<Product>()
                            .Where(p=>p.Id == id)
                            .Include(p=>p.ProductType)
                            .Include(p=>p.ProductBrand)
                            .FirstOrDefaultAsync() as T;

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync(ISpecification<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).CountAsync();
        }
    }
}
