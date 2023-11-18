// See https://aka.ms/new-console-template for more information

using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace amazonmq_producer
{
    class Program
    {
        static void Main(string[] args)
        {
            string brokerUri =
                "ssl://b-a0f648ab-44c6-4873-87d3-22fbe1049811-2.mq.us-west-2.amazonaws.com:61617"; // 替換成你的 AmazonMQ broker URL
            string userName = "activemq"; // 替換成你的 AmazonMQ 用戶名
            string password = "activemq2023"; // 替換成你的 AmazonMQ 密碼
            string topicName = "demo-topic"; // 替換成你想要發送消息的主題名

            // 建立連接工廠
            IConnectionFactory factory = new ConnectionFactory(brokerUri);

            // 創建並啟動連接
            using IConnection connection = factory.CreateConnection(userName, password);
            connection.Start();

            // 創建會話
            using var session = connection.CreateSession();
            // 創建目標主題
            IDestination destination = session.GetTopic(topicName);
            // 創建生產者
            using var producer = session.CreateProducer(destination);
            // 創建消息

            for (int i = 0; i < 100; i++)
            {
                var message = session.CreateTextMessage("Hello from C# to ActiveMQ! ... kim sent the " + i + " time message");

                // 發送消息
                producer.Send(message);
                Console.WriteLine("Message sent: " + message.Text);
            }
        }
    }
}