using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.RepositoryInterfaces;
using Talabat.Repository.Data;

namespace Talabat.Repository.RepositoryLogics
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TalabatDbContext _context;
        private Hashtable _repos;

        public UnitOfWork(TalabatDbContext context)
        {
            _context = context;
            _repos = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity> CreateRepo<TEntity>() where TEntity : BaseModel
        {
            var key = typeof(TEntity).Name;
            if(!_repos.ContainsKey(key))
            {
                var myObj = new GenericRepository<TEntity>(_context);
                _repos.Add(key, myObj);
            }
            return _repos[key]as IGenericRepository<TEntity>;
        }

        public ValueTask DisposeAsync()
        => _context.DisposeAsync();
    }
}
