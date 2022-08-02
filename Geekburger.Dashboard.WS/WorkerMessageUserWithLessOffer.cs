using Geekburger.Dashboard.Data;
using Geekburger.Dashboard.Domain;
using Geekburger.Dashboard.Services;
using Geekburger.Order.Contract.Messages;
using Messages.Service;
using Messages.Service.Messages;
using Messages.Service.Models;
using Newtonsoft.Json;
using System.Text;

namespace Geekburger.Dashboard.WS
{
    public class WorkerMessageUserWithLessOffer : BackgroundService
    {
        private readonly ILogger<WorkerMessageOrderChanged> _logger;
        private readonly RestrictionService _restrictionService;
        private readonly IConfiguration _config;

        public WorkerMessageUserWithLessOffer(ILogger<WorkerMessageOrderChanged> logger, IConfiguration config, RestrictionService restrictionService)
        {
            _logger = logger;
            _restrictionService = restrictionService;
            _config = config;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var message = new MessageUserWithLessOffer(_config.GetValue<string>("SubscriptionName"));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var readed = await message.Receive();
                if (readed != null)
                {
                    await Processar(readed);
                }

                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task Processar(MessageData received)
        {
            Console.WriteLine("Recebido:");
            Console.WriteLine(received.MessageId.ToString());

            var x = Encoding.UTF8.GetString(received.Body);
            var userWithLessOffer = JsonConvert.DeserializeObject<UserWithLessOffer>(x);

            // ASSIM QUE RECEBER A MENSAGEM, GRAVA NO BANCO
            if (userWithLessOffer != null)
            {
                await _restrictionService.Add(userWithLessOffer);
            }
        }
    }
}