using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.ServicesInterfaces;

namespace Talabat.Service
{
    public class CashService : ICashService
    {
        private readonly IDatabase _database;
        public CashService(IConnectionMultiplexer redice)
        {
            _database = redice.GetDatabase();
        }
        public async Task CashDataAsync(string cashKey, object response, TimeSpan expiringDate)
        {
            if (response is null) return;
            var serializingOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            await _database.StringSetAsync(cashKey,JsonSerializer.Serialize(response),expiringDate);
        }

        public async Task<string?> GetCashedData(string cashKey)
        {
            var responce = await _database.StringGetAsync(cashKey);
            if(responce.IsNullOrEmpty) return null;
            return responce;
        }
    }
}
