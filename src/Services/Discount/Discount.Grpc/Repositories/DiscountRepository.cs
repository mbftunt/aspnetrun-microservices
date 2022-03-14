using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<String>("DatabaseSettings:ConnectionString"));

            CommandDefinition command = new("INSERT INTO Coupon(ProductName, Description, Amount) VALUES( @ProductName, @Description, @Amount)",
                new
                {
                    coupon.ProductName,
                    coupon.Description,
                    coupon.Amount
                });

            var affected = await connection.ExecuteAsync(command);

            return affected != 0;

        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            CommandDefinition command = new("DELETE FROM Coupon WHERE ProductName = @ProductName",
                new { ProductName = productName });

            var affected = await connection.ExecuteAsync(command);

            return affected != 0;
        }
        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            CommandDefinition command = new("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { ProductName = productName });

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(command);

            return coupon ?? new Coupon() { ProductName = "No discount", Description = "No discount", Amount = 0 };

        }



        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            CommandDefinition command = new("UPDATE Coupon SET Set ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE ID = @ID  ", new
            {
                coupon.ProductName,
                coupon.Description,
                coupon.Amount,
                coupon.ID
            });

            var affected = await connection.ExecuteAsync(command);

            return affected != 0;
        }
    }
}
