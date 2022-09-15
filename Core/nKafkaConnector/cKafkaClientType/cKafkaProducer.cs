using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.nKafkaConnector.cKafkaClientType
{
    public  class cKafkaProducer
    {
        cKafkaConnector m_KafkaConnector { get; set; }
        private IProducer<Null, string> Producer { get; set; }
        public cKafkaProducer(cKafkaConnector _KafkaConnector)
        {
            m_KafkaConnector = _KafkaConnector;
        }

        public void Init()
        {
            var __Config = new ProducerConfig
            {
                BootstrapServers = m_KafkaConnector.KafkaServer,
                ClientId = Dns.GetHostName(),
            };

            Producer = new ProducerBuilder<Null, string>(__Config).Build();
        }


        public void Produce(string _Value)
        {
            var __Task = Producer.ProduceAsync(m_KafkaConnector.Channel, new Message<Null, string> { Value = _Value });
            __Task.ContinueWith(__TaskItem => {
                if (__TaskItem.IsFaulted)
                {

                }
                else
                {

                    Console.WriteLine($"Wrote to offset: {__TaskItem.Result.Offset}");
                }
            });
        }
    }
}
