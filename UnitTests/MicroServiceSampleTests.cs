using Core.nDTOs.nEvent.nEventItem;
using Core.nKafkaConnector;
using MicroServiceSample.nWebGraph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Unity.Injection;

namespace UnitTests
{
    [TestClass]
    public class MicroServiceSampleTests : cBaseTest, IMessageReceiver
    {
        static cKafkaConnector KafkaConnectorSender { get; set; }

        static cKafkaConnector KafkaConnectorReceiever { get; set; }
        static string KAFKA_TEST = "KAFKA_TEST";

        static string KafkaTestString = "";

        private bool ControlKafka()
        {
            string __Output = "";
            using (Process __Process = new Process())
            {
                __Process.StartInfo.FileName = "docker";
                __Process.StartInfo.UseShellExecute = false;
                __Process.StartInfo.Arguments = "ps -a";
                __Process.StartInfo.RedirectStandardOutput = true;
                __Process.StartInfo.CreateNoWindow = true;
                __Process.StartInfo.RedirectStandardError = true;
                __Process.Start();


                StreamReader __Reader = __Process.StandardOutput;
                __Output = __Reader.ReadToEnd();


                __Process.WaitForExit();
            }

            Match __Match = Regex.Match(__Output, "kafka1");
            Match __Match2 = Regex.Match(__Output, "zoo1");

            
            return __Match.Success && __Match2.Success;

        }



        public void ReceiveMessage(string _Message)
        {
            KafkaTestString = _Message;
        }

        [TestMethod]        
        public void Test1_KafkaTest_KafkaIsExistTest()
        {
            if (!ControlKafka())
            {
                //throw new Exception("Öncelikle kafka servisini ayaða kaldýrmanýz gerekiyor! \"docker-compose -f docker-compose.yml up\"");
                Assert.Fail("Öncelikle kafka servisini ayaða kaldýrmanýz gerekiyor! \"docker-compose -f docker-compose.yml up\"");
            }
        }

        [TestMethod]
        public void Test2_KafkaTest_Consumer()
        {
            KafkaTestString = "";
            KafkaConnectorSender = new cKafkaConnector("127.0.0.1:9092", "topic");
            ///
            /// Þuanda ayaða kalkan kafka servisi ayný grup ID ile tek consumer üzerinden çalýþýyor.
            /// Birden fazla peþpeþe test sýrasýnda testler baþarýsýz çýktýðý için random bir grup oluþturuluyor.
            /// Bu gruba belirli bir süre istek olmayýnca kafka tarafýndan düþürüldüðü için poroblem olmuyor.
            //
            KafkaConnectorSender.Consumer.StartListener("UnitTest" + DateTime.Now.ToFileTimeUtc().ToString().ToString(), this);
            Thread.Sleep(10000);
        }


        [TestMethod]
        public void Test3_KafkaTest_ProduceAndControl()
        {
            KafkaConnectorReceiever = new cKafkaConnector("127.0.0.1:9092", "topic");
            KafkaConnectorReceiever.Producer.Init();
            KafkaConnectorReceiever.Producer.Produce("KAFKA_TEST");

            int __Counter = 0;
            while(__Counter < 30)
            {
                if (KafkaTestString == KAFKA_TEST)
                {
                    break;
                }
                Thread.Sleep(1000);
                __Counter++;
            }

            Assert.AreEqual(KafkaTestString, KAFKA_TEST);
        }

        [TestMethod]
        public void Test4_EventTest()
        {
            cEventGraph __EventGraph = new cEventGraph();
            WebApiController __ForTest = new WebApiController(__EventGraph);
            JObject __JsonObject = new JObject();
            JArray __JsonArray = new JArray();
            string __SampleJSON = "{\"app\":\"277cdc8c-b0ea-460b-a7d2-592126f5bbb0\",\"type\":\"HOTEL_CREATE\",\"time\":\"2020-02-10T13:40:27.650Z\",\"isSucceeded\":true,\"meta\":{},\"user\":{\"isAuthenticated\":true,\"provider\":\"b2c-internal\",\"email\":\"test1@hotmail.com\",\"id\":1},\"attributes\":{\"hotelId\":1,\"hotelRegion\":\"Kýbrýs\",\"hotelName\":\"Rixos\"}}";
            __JsonArray.Add(JObject.Parse(__SampleJSON));
            __JsonObject["events"] = __JsonArray;
            __ForTest.Events = __JsonObject;
            __EventGraph.Interpret(__ForTest);

            int __Counter = 0;
            while (__Counter < 30)
            {
                if (KafkaTestString != "" && KafkaTestString != KAFKA_TEST)
                {
                    break;
                }
                Thread.Sleep(1000);
                __Counter++;
            }

            cEventItem __EventItem = JsonConvert.DeserializeObject<cEventItem>(KafkaTestString);


            Assert.AreEqual(__EventItem.app.ToString(), "277cdc8c-b0ea-460b-a7d2-592126f5bbb0");
        }
    }
}