using Core.nKafkaConnector;
using PostgresApp.nKafkaListener;
using System.Text;

namespace PostgresApp
{
    class Program
    {
        static void Main(string[] args)
        {
            cKafkaListener __KafkaListener = new cKafkaListener();

            /*string KAFKA1_SERVICE_HOST = Environment.GetEnvironmentVariable("KAFKA1_SERVICE_HOST");
            string KAFKA1_PORT_29092_TCP_PORT = Environment.GetEnvironmentVariable("KAFKA1_PORT_29092_TCP_PORT");

            string KAFKA2_SERVICE_HOST = Environment.GetEnvironmentVariable("KAFKA2_SERVICE_HOST");
            string KAFKA2_PORT_29093_TCP_PORT = Environment.GetEnvironmentVariable("KAFKA2_PORT_29093_TCP_PORT");

            string KAFKA3_SERVICE_HOST = Environment.GetEnvironmentVariable("KAFKA3_SERVICE_HOST");
            string KAFKA3_PORT_29094_TCP_PORT = Environment.GetEnvironmentVariable("KAFKA3_PORT_29094_TCP_PORT");*/


            //cKafkaConnector __KafkaConnector = new cKafkaConnector("127.0.0.1:9092", "eventlog");
            //cKafkaConnector __KafkaConnector = new cKafkaConnector(KAFKA1_SERVICE_HOST + ":" + KAFKA1_PORT_29092_TCP_PORT + "," + KAFKA2_SERVICE_HOST + ":" + KAFKA2_PORT_29093_TCP_PORT + "," + KAFKA3_SERVICE_HOST + ":" + KAFKA3_PORT_29094_TCP_PORT, "topic");
            cKafkaConnector __KafkaConnector = new cKafkaConnector("host.docker.internal:29092,host.docker.internal:29093,host.docker.internal:29094", "topic");

            __KafkaConnector.Consumer.StartListener("Postgres", __KafkaListener);

            Console.WriteLine("Postgres App Started");
            string __Code = Console.ReadLine();

            while (__Code != "exit")
            {
                __Code = Console.ReadLine();
            }

            __KafkaConnector.Consumer.Stop();
        }
    }
}