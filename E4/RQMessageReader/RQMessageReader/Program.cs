using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


namespace RQMessageReader
{
    public class QueueConsumer
    {
        private IConnection? _connection;
        private IChannel? _channel;

        public async Task StartAsync()
        {
            // Configurar los datos de conexion (deberia ir en un config)
            var factory = new ConnectionFactory
            {
                HostName = "localhost", // HostName o ip
                Port = 5672,            // Puerto por defecto
                UserName = "guest",     // Usuario
                Password = "guest"      // Contraseña
            };

            // Crear la conexión
            _connection = await factory.CreateConnectionAsync();

            // Crear un canal de comunicación 
            _channel = await _connection.CreateChannelAsync();

            // Declarar la cola con las propiedades indicadas
            await _channel.QueueDeclareAsync(
                queue: "sensorQueue",
                durable: true,    // Persistencia en disco
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            // Crear un consumidor asíncrono
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    Console.WriteLine($"[Consumidor] Recibido: {message}");

                    /// TODO: Logica de procesamiento

                    // Confirmar que el mensaje fue procesado correctamente
                    await _channel.BasicAckAsync(ea.DeliveryTag, multiple: false);
                }
                catch (Exception ex)
                {
                    // Manejo de errores: rechaza el mensaje y lo reencola para reintento
                    Console.WriteLine($"[Error] {ex.Message}");
                    await _channel.BasicNackAsync(ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            // Iniciar la suscripción a la cola
            await _channel.BasicConsumeAsync(
                queue: "sensorQueue",
                autoAck: false,  // Se desactiva el auto-ack para controlar la confirmación manualmente
                consumer: consumer
            );

            Console.WriteLine("[*] Oprime cualquier tecla para salir.");
            Console.ReadLine();

            // Opcional: Cerrar canal y conexión al salir
            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }

    }

    internal class Program
    {
        static async Task Main(string[] args)
        {
            await new QueueConsumer().StartAsync();
        }
    }
}
