using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ProcessarSolicitacaoAluguel
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConnection _rabbitConnection;
        public Worker(ILogger<Worker> logger,
            IConnection rabbitConnection)
        {
            _logger = logger;
            _rabbitConnection = rabbitConnection;
            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (var canal = _rabbitConnection.CreateModel())
            {
                var consumidor = new EventingBasicConsumer(canal);
                consumidor.Received += (model, ea) =>
                {
                    
                    var corpo = ea.Body.ToArray();
                    var mensagem = Encoding.UTF8.GetString(corpo);
                    //ClientDto clienteDto = JsonSerializer.Deserialize<ClientDto>(mensagem);

                    canal.BasicAck(
                        deliveryTag: ea.DeliveryTag,
                        multiple: false
                        );
                };

                canal.BasicConsume(
                    queue: "qu.solicitacao.cadastro.cliente",
                    autoAck: false,
                    consumer: consumidor
                    );

                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(1000, stoppingToken);
                }
            }
        }
    }
}
