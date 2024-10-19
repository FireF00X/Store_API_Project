using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.BasketEntities;

namespace Talabat.Core.RepositoryInterfaces
{
    public interface IRedisRepository
    {
        Task<CustomerBasket?> GetByIdAsync(string id);
        Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteByIdAsync(string id);
    }
}
