﻿using System.Collections.Generic;
using MediatR;
using OnlineStore.Business.DTOs;

namespace OnlineStore.Business.Mediator.Requests.Queries
{
    public record GetOrderHistoryByUserIdQuery(string userId) : IRequest<IEnumerable<OrderDTO>>;

}