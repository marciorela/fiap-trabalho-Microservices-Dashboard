using Geekburger.Dashboard.Data;
using Geekburger.Dashboard.Services;
using Geekburger.Order.Contract.Messages;
using Messages.Service;
using Messages.Service.Messages;
using Messages.Service.Models;
using Newtonsoft.Json;
using System.Text;

namespace Geekburger.Dashboard.WS
{
    public class WorkerMessageOrderChanged : BackgroundService
    {
        private readonly ILogger<WorkerMessageOrderChanged> _logger;
        private readonly SalesService _messageOrderChangedService;
        private readonly IConfiguration _config;

        public WorkerMessageOrderChanged(ILogger<WorkerMessageOrderChanged> logger, IConfiguration config, SalesService messageOrderChangedService)
        {
            _logger = logger;
            _messageOrderChangedService = messageOrderChangedService;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var message = new MessageOrderChanged(_config.GetValue<string>("SubscriptionName"));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var readed = await message.Receive();
                if (readed != null)
                {
                    await Processar(readed);
                }

                //await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task Processar(MessageData received)
        {
            Console.WriteLine("Recebido:");
            Console.WriteLine(received.MessageId.ToString());

            var x = Encoding.UTF8.GetString(received.Body);
            var orderChanged = JsonConvert.DeserializeObject<OrderChanged>(x);

            // ASSIM QUE RECEBER A MENSAGEM, GRAVA NO BANCO
            if (orderChanged != null)
            {
                await _messageOrderChangedService.Add(orderChanged);
            }
        }
    }
}