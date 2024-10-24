using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.API.DTOs.OrdersDtos;
using Talabat.API.Errors;
using Talabat.Core.Entities.OrderEntities;
using Talabat.Core.ServicesInterfaces;

namespace Talabat.API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderingService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderingService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var reqAddress =_mapper.Map<OrderAddressDto, OrderAddress>(orderDto.ShippingAddress);
            var order = await _orderService.CreateOrderAsync(orderDto.BasketId,orderDto.BuyerEmail, orderDto.DeliveryMethodId, reqAddress);
            if (order == null) return BadRequest(new ApiResponse(400));
            return Ok(order);
        }
    }
}
