using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Core.RepositoryInterfaces;

namespace Talabat.Repository.RepositoryLogics
{
    public class RedisRepository : IRedisRepository
    {
        private readonly IDatabase _database;

        public RedisRepository(IConnectionMultiplexer database)
        {
            _database = database.GetDatabase();
        }
        public async Task<bool> DeleteByIdAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetByIdAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return JsonSerializer.Deserialize<CustomerBasket>(basket) ?? null;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var updatedBasket = await _database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromMinutes(1));
            if (updatedBasket is false) return null;
            return await GetByIdAsync(basket.Id);
        }
    }
}
