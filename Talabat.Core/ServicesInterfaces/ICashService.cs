using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.ServicesInterfaces
{
    public interface ICashService
    {
        // cashing
        Task CashDataAsync(string cashKey, object response, TimeSpan expiringDate);
        // get the cashed
        Task<string?> GetCashedData(string cashKey);
    }
}
