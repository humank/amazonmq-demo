// See https://aka.ms/new-console-template for more information

using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace amazonmq_consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            string brokerUri = "ssl://b-a0f648ab-44c6-4873-87d3-22fbe1049811-2.mq.us-west-2.amazonaws.com:61617"; // 替換成你的 AmazonMQ broker URL
            string userName = "activemq"; // 替換成你的 AmazonMQ 用戶名
            string password = "activemq2023"; // 替換成你的 AmazonMQ 密碼
            string topicName = "demo-topic"; // 替換成你想要發送消息的主題名
            string clientId = "consumer-kim-local"; // 客戶端ID
            string subscriptionName = "consumer-kim-subscription"; // 持久訂閱名稱
            
            // 建立連接工廠
            IConnectionFactory factory = new ConnectionFactory(brokerUri);

            // 創建並啟動連接
            using (IConnection connection = factory.CreateConnection(userName, password))
            {
                connection.Start();

                // 創建會話
                using (ISession session = connection.CreateSession())
                {
                    // 創建主題
                    IDestination destination = session.GetTopic(topicName);

                    // 創建消費者
                    using (IMessageConsumer consumer = session.CreateConsumer(destination))
                    {
                        Console.WriteLine("Listening for messages on topic: " + topicName);

                        // 無限循環，等待消息
                        while (true)
                        {
                            // 等待接收消息
                            IMessage message = consumer.Receive();
                            if (message is ITextMessage textMessage)
                            {
                                Console.WriteLine("Received message: " + textMessage.Text);
                            }
                            else
                            {
                                Console.WriteLine("Received non-text message");
                            }
                        }
                    }
                }
            }
        }
    }   
}



