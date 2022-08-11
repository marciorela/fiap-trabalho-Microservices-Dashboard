using Geekburger.Dashboard.Domain;
using Messages.Service.Messages;
using Messages.Service.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Dashboard.Services
{
    public static class MessageUserWithLessOfferService
    {
        private static IServiceProvider? _services;

        public async static Task<IApplicationBuilder> HandleMessageUserWithLessOffer(this IApplicationBuilder app)
        {
            _services = app.ApplicationServices;

            var message = new MessageUserWithLessOffer();
            await message.Receive(ProcessaUserWithLessOffer);

            return app;
        }

        private async static Task ProcessaUserWithLessOffer(MessageData received)
        {
            Console.WriteLine("Recebido:");
            Console.WriteLine(received.MessageId.ToString());

            var x = Encoding.UTF8.GetString(received.Body);
            var userWithLessOffer = JsonConvert.DeserializeObject<UserWithLessOffer>(x);

            // ASSIM QUE RECEBER A MENSAGEM, GRAVA NO BANCO
            if (userWithLessOffer is not null)
            {
                var factory = _services?.GetService<IServiceScopeFactory>();

                if (factory is not null)
                {
                    using var scope = factory.CreateScope();

                    var _restrictionService = scope.ServiceProvider.GetService<RestrictionService>();
                    if (_restrictionService is not null)
                    {
                        if (_restrictionService is not null)
                        {
                            await _restrictionService.Add(userWithLessOffer);
                        }
                    }
                }
            }
        }
    }
}
