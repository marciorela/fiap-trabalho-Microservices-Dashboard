using Geekburger.Dashboard.Contract.DTOs;
using Geekburger.Dashboard.Data;
using Geekburger.Order.Contract.Messages;

namespace Geekburger.Dashboard.Services
{
    public class SalesService : ISalesService
    {
        private readonly OrderRepository _orderRepository;

        public SalesService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Add(OrderChanged orderChanged)
        {
            if (orderChanged.State == "Paid")
            {
                await _orderRepository.Add(new Domain.Entities.Order()
                {
                    OrderId = orderChanged.OrderId,
                    StoreName = orderChanged.StoreName,
                    Value = orderChanged.Total
                });
            }
        }

        public async Task<IEnumerable<SalesResponse>> GetSales(string per, int value)
        {
            var list = await _orderRepository.Get(per, value);
            return list.Select(x => new SalesResponse()
            {
                StoreName = x.StoreName,
                Total = x.Total,
                Value = x.Value
            });
        }
    }
}