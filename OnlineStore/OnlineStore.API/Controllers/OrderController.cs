using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.API.ViewModels;
using OnlineStore.Business.DTOs;
using OnlineStore.Business.Mediator.Requests.Commands;
using OnlineStore.Business.Mediator.Requests.Queries;
using OnlineStore.Business.Mediator.Requests.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineStore.DataAccess.Models.AppDbContext;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.API.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly OnlineStoreDbContext db;

        public OrderController(IMediator mediator, IMapper mapper, OnlineStoreDbContext db)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.db = db;
        }

        [HttpPost]
        public async Task<ActionResult> OrderProducts(string userId)
        {
            await mediator.Send(new OrderCommand(userId));
            return Ok();
        }

        [HttpPost("process")]
        public async Task<ActionResult> ProcessOrder(ProcessOrderVM model)
        {
            var order = db.Orders.FirstOrDefault(x => x.Id == model.OrderId);
            order.IsProcessed = true;

            await db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("~/cart/add-to-cart")]
        public async Task<ActionResult> AddToCart(string userId, CartProductVM cartProductVm)
        {
            try
            {
                var cartProductDto = mapper.Map<CartProductDTO>(cartProductVm);
                await mediator.Send(new AddProductToCartCommand(cartProductDto, userId));
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("~/cart/view-items")]
        public async Task<ActionResult<IEnumerable<CartProductVM>>> ViewCartItems(string userId)
        {
            try
            {
                var cartProductDtos = await mediator.Send(new GetCartProductsByUserIdQuery(userId));
                var cartProductVms = mapper.Map<IEnumerable<CartProductVM>>(cartProductDtos);
                return Ok(cartProductVms);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderMobileOverviewDTO>>> Orders(bool isProcessed = false)
        {
            try
            {
                var orders = await db.Orders
                    .Include(x => x.Products).ThenInclude(x => x.Product)
                    .Where(x => x.IsProcessed == isProcessed)
                    .ToListAsync();


                ICollection<OrderMobileOverviewDTO> dtos = new List<OrderMobileOverviewDTO>();
                foreach (var order in orders)
                {
                    var dto = new OrderMobileOverviewDTO()
                    {
                        OrderDate = order.DateOfOrder,
                        OrderId = order.Id,
                        Products = order.Products.Select(x => new ProductQuantity()
                        {
                            Name = x.Product.Name,
                            Quantity = x.Quantity
                        })
                    };

                    dtos.Add(dto);
                }

                return Ok(dtos);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("history")]
        public async Task<ActionResult<IEnumerable<OrderVM>>> ViewOrderHistory(string userId)
        {
            var orders = await mediator.Send(new GetOrderHistoryByUserIdQuery(userId));
            return Ok(mapper.Map<IEnumerable<OrderVM>>(orders));
        }
    }
}