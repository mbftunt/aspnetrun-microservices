using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        readonly IDiscountRepository _repository;
        readonly ILogger<DiscountService> _logger;
        readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);

            if (coupon == null)
            {
                _logger.LogError($"Discount with productName = {request.ProductName} is not found");
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with productName = {request.ProductName} is not found"));
            }

            _logger.LogInformation("Discount is retrieved for Product Name: {productName}, Amount: {ammount}", coupon.ProductName, coupon.Amount);

            return _mapper.Map<CouponModel>(coupon);
        }
        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {

            var coupon = await _repository.CreateDiscount(_mapper.Map<Coupon>(request.Coupon));
            _logger.LogInformation("Discount is successfully created. ProductName: {ProductName}", request.Coupon.ProductName);
            return _mapper.Map<CouponModel>(coupon);
        }
        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.UpdateDiscount(_mapper.Map<Coupon>(request.Coupon));
            _logger.LogInformation("Discount is successfully updated. ProductName: {ProductName}", request.Coupon.ProductName);
            return _mapper.Map<CouponModel>(coupon);
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);
            return new DeleteDiscountResponse() { Success = deleted };
        }
    }
}
