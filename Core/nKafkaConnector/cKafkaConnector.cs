using Confluent.Kafka;
using Core.nKafkaConnector.cKafkaClientType;
using System;
using System.Net;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Core.nKafkaConnector
{
    public class cKafkaConnector
    {
        public cKafkaProducer Producer { get; set; }
        public cKafkaConsumer Consumer { get; set; }


        public string KafkaServer { get; set; }
        public string Channel { get; set; }

        public cKafkaConnector(string _KafkaServer, string _Channel)
        {
            KafkaServer = _KafkaServer;
            Channel = _Channel;
            Init();
        }

        private void Init()
        {
            Producer = new cKafkaProducer(this);
            Consumer = new cKafkaConsumer(this);
        }
      
        
    }
}
