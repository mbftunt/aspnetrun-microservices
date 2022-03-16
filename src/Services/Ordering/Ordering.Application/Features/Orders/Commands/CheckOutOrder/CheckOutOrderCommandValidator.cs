using FluentValidation;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckOutOrder
{
    public class CheckOutOrderCommandValidator : AbstractValidator<CheckOutOrderCommand>
    {
        public CheckOutOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage(string.Format(Messages.Instance().ERROR_IS_REQUIRED, "UserName"))
                .NotNull()
                .MaximumLength(50).WithMessage(string.Format(Messages.Instance().ERROR_MAX_LENGTH, "UserName", "50"));

            RuleFor(p => p.EmailAddress)
                .NotEmpty().WithMessage(string.Format(Messages.Instance().ERROR_IS_REQUIRED, "EmailAddress"))
                .EmailAddress().WithMessage(string.Format(Messages.Instance().ERROR_VALUE_FORMAT_EMAIL));


            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage(string.Format(Messages.Instance().ERROR_IS_REQUIRED, "TotalPrice"))
                .GreaterThan(0).WithMessage(string.Format(Messages.Instance().ERROR_GREATER, "TotalPrice", "0"));

        }
    }
}
