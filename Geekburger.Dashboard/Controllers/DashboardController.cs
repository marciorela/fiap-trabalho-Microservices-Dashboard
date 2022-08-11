using Geekburger.Dashboard.Contract.DTOs;
using Geekburger.Dashboard.Data;
using Geekburger.Dashboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace Geekburger.Dashboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly RestrictionService _restrictionService;
        private readonly ISalesService _salesService;

        public DashboardController(ILogger<DashboardController> logger, RestrictionService restrictionService, ISalesService salesService)
        {
            _logger = logger;
            _restrictionService = restrictionService;
            _salesService = salesService;
        }

        [HttpGet("usersWithLessOffer")]
        public async Task<List<UsersWithLessOfferResponse>> GetUsers()
        {
            return await _restrictionService.GetAll();
        }

        [HttpGet("Sales")]
        public async Task<IEnumerable<SalesResponse>> GetSales([FromQuery] SalesRequest sales)
        {
            return await _salesService.GetSales(sales.Per, sales.Hour);
        }
    }
}
