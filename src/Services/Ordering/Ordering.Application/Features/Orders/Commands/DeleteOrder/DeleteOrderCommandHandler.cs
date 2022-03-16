using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        readonly IOrderRepository _repository;
        readonly ILogger<UpdateOrderCommandHandler> _logger;
        readonly Mapper _mapper;

        public DeleteOrderCommandHandler(IOrderRepository repository, ILogger<UpdateOrderCommandHandler> logger, Mapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = _repository.GetByIdAsync(request.Id);
            if (orderToDelete != null)
            {
                var orderEntity = _mapper.Map<Order>(request);
                await _repository.DeleteAsync(orderEntity);
                _logger.LogInformation(Messages.Instance().INFO_DELETE_SUCCES, "Order", orderEntity.Id);
            }
            else
            {
                _logger.LogError(Messages.Instance().ERROR_NOT_EXISTS_IN_DATABASE, "Order");
            }

            return Unit.Value;
        }
    }
}
