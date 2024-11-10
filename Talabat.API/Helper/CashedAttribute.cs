using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using Talabat.Core.ServicesInterfaces;

namespace Talabat.API.Helper
{
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _expireInSeconds;

        public CashedAttribute(int expireInSeconds)
        {
            _expireInSeconds = expireInSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var getCashService =  context.HttpContext.RequestServices.GetRequiredService<ICashService>();
            var cashKey = GenerateCashKeyFromRequest(context.HttpContext.Request);
            var cashResponse = await getCashService.GetCashedData(cashKey);

            if(!string.IsNullOrEmpty(cashResponse))
            {
                var contentResult = new ContentResult()
                {
                    Content = cashResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;
            }
            var excutionEndPoint = await next.Invoke();
            if(excutionEndPoint.Result is OkObjectResult result)
            {
                await getCashService.CashDataAsync(cashKey, result.Value, TimeSpan.FromSeconds(_expireInSeconds));
            }

        }

        private string GenerateCashKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append(request.Path);
            foreach (var (key,value) in request.Query.OrderBy(o=>o.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }
            return keyBuilder.ToString();
        }
    }
}
