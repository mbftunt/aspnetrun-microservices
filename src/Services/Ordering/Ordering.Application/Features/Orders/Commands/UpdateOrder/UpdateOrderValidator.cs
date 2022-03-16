using FluentValidation;
using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderValidator()
        {
            RuleFor(p => p.UserName)
                .NotNull()
                .NotEmpty().WithMessage(string.Format(Messages.Instance().ERROR_NOT_EMPTY, "UserName"))
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
