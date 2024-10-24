using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.RepositoryInterfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> CreateRepo<TEntity>() where TEntity : BaseModel;
        Task<int> CompleteAsync();
    }
}
