using Geekburger.Dashboard.Database;

namespace Geekburger.Dashboard.Data
{
    public class RepositoryBase
    {
        protected readonly DashboardDbContext _ctx;

        public RepositoryBase(DashboardDbContext ctx)
        {
            _ctx = ctx;
        }

    }
}