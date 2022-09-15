using Core.nKafkaConnector;
using ElasticSearchApp.nKafkaListener;
using System.Text;

namespace ElasticSearchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            cKafkaListener __KafkaListener = new cKafkaListener();

            //cKafkaConnector __KafkaConnector = new cKafkaConnector("127.0.0.1:9092", "topic");
            cKafkaConnector __KafkaConnector = new cKafkaConnector("host.docker.internal:29092,host.docker.internal:29093,host.docker.internal:29094", "topic");
            __KafkaConnector.Consumer.StartListener("ElasticSearch", __KafkaListener);

            Console.WriteLine("Elactic Search App Started");
            string __Code = Console.ReadLine();

            while (__Code != "exit")
            {
                __Code = Console.ReadLine();
            }

            __KafkaConnector.Consumer.Stop();
        }
    }
}