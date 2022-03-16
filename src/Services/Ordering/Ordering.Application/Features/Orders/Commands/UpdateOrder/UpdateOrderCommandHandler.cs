using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using SendGrid.Helpers.Errors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        readonly IOrderRepository _repository;
        readonly ILogger<UpdateOrderCommandHandler> _logger;
        readonly Mapper _mapper;

        public UpdateOrderCommandHandler(IOrderRepository repository, ILogger<UpdateOrderCommandHandler> logger, IEmailService emailService, Mapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        async Task<Unit> IRequestHandler<UpdateOrderCommand, Unit>.Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {

            var orderToUpdate = _repository.GetByIdAsync(request.Id);

            if (orderToUpdate != null)
            {
                //_mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));
                var orderEntity = _mapper.Map<Order>(orderToUpdate);
                await _repository.UpdateAsync(orderEntity);
                _logger.LogInformation(Messages.Instance().INFO_UPDATE_SUCCES, "Order", orderEntity.Id);
            }
            else
            {
                _logger.LogError(Messages.Instance().ERROR_NOT_EXISTS_IN_DATABASE);
            }

            return Unit.Value;
        }
    }
}
