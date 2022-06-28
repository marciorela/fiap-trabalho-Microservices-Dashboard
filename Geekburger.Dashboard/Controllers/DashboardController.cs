using Geekburger.Dashboard.Contract.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Geekburger.Dashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        [HttpGet("usersWithLessOffer")]
        public List<UsersWithLessOfferResponse> GetUsers()
        {
            return new List<UsersWithLessOfferResponse>()
            {
                new UsersWithLessOfferResponse() { Users = 2, Restrictions = "soy, dairy, peanut"},
                new UsersWithLessOfferResponse() { Users = 1, Restrictions = "soy, dairy"}
            };
        }

        [HttpGet("Sales")]
        public SalesResponse GetSales([FromQuery] SalesRequest sales)
        {

            return new SalesResponse()
            {
                StoreName = "Paulista-" + sales.Per,
                Total = sales.Hour,
                Value = 4092.00
            };
        }
    }
}
