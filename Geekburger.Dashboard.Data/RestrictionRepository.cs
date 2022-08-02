using Geekburger.Dashboard.Database;
using Geekburger.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Data
{
    public class RestrictionRepository : RepositoryBase
    {
        public RestrictionRepository(DashboardDbContext ctx) : base(ctx)
        {
        }

        public async Task Add(Restriction restriction)
        {
            await _ctx.AddAsync(restriction);
            await _ctx.SaveChangesAsync();
        }

        public async Task AddRange(IEnumerable<Restriction> restrictions)
        {
            await _ctx.AddRangeAsync(restrictions);
            await _ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restriction>> GetAll()
        {
            return await _ctx.Restrictions
                .OrderBy(x => x.UserId)
                .ThenBy(x => x.Name)
                .ToListAsync();
        }
    }
}
