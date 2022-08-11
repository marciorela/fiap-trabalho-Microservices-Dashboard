using Geekburger.Order.Contract.Messages;
using Messages.Service.Messages;
using Messages.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace Geekburger.Dashboard.Services
{
    public static class MessageOrderChangedService
    {
        private static IServiceProvider? _services;

        public async static Task<IApplicationBuilder> HandleMessageOrderChanged(this IApplicationBuilder app)
        {
            _services = app.ApplicationServices;

            var message = new MessageOrderChanged();
            await message.Receive(ProcessaOrderChanged);

            return app;
        }

        private async static Task ProcessaOrderChanged(MessageData received)
        {
            Console.WriteLine("Recebido:");
            Console.WriteLine(received.MessageId.ToString());

            var x = Encoding.UTF8.GetString(received.Body);
            var orderChanged = JsonConvert.DeserializeObject<OrderChanged>(x);

            // ASSIM QUE RECEBER A MENSAGEM, GRAVA NO BANCO
            if (orderChanged is not null)
            {
                var factory = _services?.GetService<IServiceScopeFactory>();

                if (factory is not null)
                {
                    using var scope = factory.CreateScope();

                    var _salesService = scope.ServiceProvider.GetService<ISalesService>();
                    if (_salesService is not null)
                    {
                        if (_salesService is not null)
                        {
                            await _salesService.Add(orderChanged);
                        }
                    }
                }
            }
        }
    }
}
