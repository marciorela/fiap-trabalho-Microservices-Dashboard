using Geekburger.Dashboard.Database;
using Geekburger.Dashboard.Domain.Entities;
using Geekburger.Dashboard.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Data
{
    public class OrderRepository : RepositoryBase
    {
        public OrderRepository(DashboardDbContext ctx) : base(ctx)
        {
        }

        public async Task Add(Order order)
        {
            await _ctx.Orders.AddAsync(order);
            await _ctx.SaveChangesAsync();
        }

        public async Task<List<SalesTotal>> Get(string per, int value)
        {
            return await _ctx.Orders
                .Where(x => (per == "hour" && x.Date.Hour == value) || string.IsNullOrEmpty(per))
                .GroupBy(x => x.StoreName)
                .Select(x => new SalesTotal { StoreName = x.Key, Total = x.Count(), Value = x.Sum(c => c.Value) })
                .ToListAsync();
        }
    }
}
