using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandHandler : IRequestHandler<CheckOutOrderCommand, int>
    {
        readonly Mapper _mapper;
        readonly IOrderRepository _repository;
        readonly ILogger<CheckOutOrderCommandHandler> _logger;
        readonly IEmailService _emailService;

        public CheckOutOrderCommandHandler(Mapper mapper, IOrderRepository repository, IEmailService emailService, ILogger<CheckOutOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckOutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _repository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created");

            await SendMail(newOrder);

            return newOrder.Id;
        }
        public async Task SendMail(Order order)
        {
            try
            {
                var email = new Email() { To = Constants.MailTo, Subject = $"Order {order.Id} is successfully created", Body = "" };
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {

                _logger.LogError($"Order {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}
