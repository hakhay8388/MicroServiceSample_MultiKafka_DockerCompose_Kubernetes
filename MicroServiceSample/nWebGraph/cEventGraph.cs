
using Core.nDTOs.nEvent;
using Core.nDTOs.nEvent.nEventItem;
using Core.nKafkaConnector;
using Core.nUtils.nJsonConverter;
using MicroServiceSample.nWebGraph.nValidatorManager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;


namespace MicroServiceSample.nWebGraph
{ 
    public class cEventGraph 
    {
        protected cValidatorManager ValidatorManager { get; set; }
        protected cKafkaConnector KafkaConnector { get; set; }
        public cEventGraph()
            :base()
        {
            ValidatorManager = new cValidatorManager();
            KafkaConnector = new cKafkaConnector("127.0.0.1:9092,127.0.0.1:9093,127.0.0.1:9094", "topic");
            //KafkaConnector = new cKafkaConnector("host.docker.internal:29092", "topic");
            KafkaConnector.Producer.Init();
        }

        public void Interpret(cBaseController _Controller)
        {
            if (_Controller.Events.ContainsKey("events"))
            {
                JToken __Events = _Controller.Events["events"];
                JArray __CommandItem = (JArray)__Events;

                foreach (var __EventItem in __Events)
                {
                    List<string> __ValidationErrors = ValidatorManager.Validate<cEventItem>((JObject)__EventItem);
                    if (__ValidationErrors.Count == 0)
                    {
                        KafkaConnector.Producer.Produce(__EventItem.ToString());
                    }
                    else
                    {
                        foreach (string __ValidationError in __ValidationErrors)
                        {
                            Console.WriteLine($"Error {__ValidationError}, message can not validate! message is :  {__EventItem.ToString()}");
                        }
                    }
                }

            }
        }

    }
}
