using Geekburger.Dashboard.Contract.DTOs;
using Geekburger.Order.Contract.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Services
{
    public interface ISalesService
    {
        Task Add(OrderChanged orderChanged);
        Task<IEnumerable<SalesResponse>> GetSales(string per, int value);
    }
}
