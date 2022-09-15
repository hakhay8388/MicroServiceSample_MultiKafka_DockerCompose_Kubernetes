using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.nKafkaConnector.cKafkaClientType
{
    public class cKafkaConsumer
    {
        cKafkaConnector m_KafkaConnector { get; set; }
        IMessageReceiver m_MessageReceiver { get; set; }
        string m_GroupId { get; set; }
        bool m_Stop { get; set; }
        private Thread ReceiverThread { get; set; }
        public cKafkaConsumer(cKafkaConnector _KafkaConnector)
        {
            m_KafkaConnector = _KafkaConnector;
            m_Stop = false;
        }

        public void StartListener(string _GroupId, IMessageReceiver _MessageReceiver)
        {
            m_MessageReceiver = _MessageReceiver;
            m_GroupId = _GroupId;
            ReceiverThread = new Thread(new ThreadStart(ReceiverThreadFunction));
            ReceiverThread.Start();
        }

        public void Stop()
        {
            m_Stop = true;
        }

        private void ReceiverThreadFunction()
        {
            var __Config = new ConsumerConfig
            {
                BootstrapServers = m_KafkaConnector.KafkaServer,
                GroupId = m_GroupId,
                AutoOffsetReset = AutoOffsetReset.Latest
            };

            using (var __Consumer = new ConsumerBuilder<Ignore, string>(__Config).Build())
            {
                __Consumer.Subscribe(m_KafkaConnector.Channel);

                while (!m_Stop)
                {
                    var __ConsumeResult = __Consumer.Consume(CancellationToken.None);
                    if (m_MessageReceiver != null)
                    {
                        m_MessageReceiver.ReceiveMessage(__ConsumeResult.Message.Value);
                    }
                }

                __Consumer.Close();
            }
        }
    }
}
