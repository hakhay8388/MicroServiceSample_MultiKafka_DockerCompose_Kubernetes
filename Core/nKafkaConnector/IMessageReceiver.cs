using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.nKafkaConnector
{
    public interface IMessageReceiver
    {
        public void ReceiveMessage(string _Message);
    }
}
