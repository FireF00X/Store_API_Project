using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs;
using Talabat.API.Errors;
using Talabat.Core.Entities.BasketEntities;
using Talabat.Core.RepositoryInterfaces;

namespace Talabat.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IRedisRepository _redis;
        private readonly IMapper _mapper;

        public BasketController(IRedisRepository redis,IMapper mapper)
        {
            _redis = redis;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _redis.GetByIdAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateOrAdd (CustomerBasketDto basket)
        {
            var mappedBasket = _mapper.Map<CustomerBasket>(basket);
            var addedOrUpdatedBasket = await _redis.UpdateBasketAsync(mappedBasket);
            if (addedOrUpdatedBasket == null) return BadRequest(new ApiResponse(400));
            return Ok(addedOrUpdatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            await _redis.DeleteByIdAsync(id);
        }
    }
}
