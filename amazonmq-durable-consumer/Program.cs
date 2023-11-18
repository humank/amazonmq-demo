﻿// See https://aka.ms/new-console-template for more information

using Apache.NMS;
using Apache.NMS.ActiveMQ;

namespace amazonmq_durable_consumer
{
    class Program
    {
        

        static void Main(string[] args)
        {
            
            Console.WriteLine("args[0]" + args[0]);
            Console.WriteLine("args[1]" + args[1]);
            Console.WriteLine("Chaos test " + args[2]);
            string brokerUri =
                "ssl://b-a0f648ab-44c6-4873-87d3-22fbe1049811-2.mq.us-west-2.amazonaws.com:61617"; // 替換成你的 AmazonMQ broker URL

            string userName = "activemq"; // 替換成你的 AmazonMQ 用戶名
            string password = "activemq2023"; // 替換成你的 AmazonMQ 密碼
            string topicName = "demo-topic"; // 替換成你想要發送消息的主題名
            //string clientId = "consumer-kim-local"; // 客戶端ID
            string clientId = args[0]; // 客戶端ID
            //string subscriptionName = "consumer-kim-subscription"; // 持久訂閱名稱
            string subscriptionName = args[1]; // 持久訂閱名稱
            string dlqName = "ActiveMQ.DLQ"; // 死信隊列的名稱
            bool chaosTest = args[2].Equals("Y") ? true : false ;

            // 建立連接工廠
            IConnectionFactory factory = new ConnectionFactory(brokerUri);

            // 創建連接，並設置客戶端ID
            using IConnection connection = factory.CreateConnection(userName, password);
            connection.ClientId = clientId;
            connection.Start();

            // 創建會話
            using ISession session = connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            // 創建主題
            ITopic destination = session.GetTopic(topicName);

            // 創建持久訂閱消費者
            using IMessageConsumer consumer = session.CreateDurableConsumer(destination, subscriptionName, null, false);
            Console.WriteLine($"Listening for messages on topic '{topicName}' with subscription '{subscriptionName}'");

            // 創建目標主題和死信隊列目標
            IDestination dlqDestination = session.GetQueue(dlqName);

            // 創建消費者和死信隊列生產者
            using var dlqProducer = session.CreateProducer(dlqDestination);
            while (true) // 持續監聽消息
            {
                var message = consumer.Receive();
                // 在這裡處理消息
                message = consumer.Receive();
                if (message is ITextMessage textMessage)
                {
                    Console.WriteLine($"Received message: {textMessage.Text}");
                    // 如果成功處理，則繼續下一個循環

                    if ( chaosTest && DateTime.Now.Second % 5 != 0) continue;
                    Console.WriteLine("處理消息時出現異常");
                    // 處理失敗，將消息發送到死信隊列
                    dlqProducer.Send(message);
                }
                else
                {
                    Console.WriteLine("Received non-text message");
                }
            }
        }
    }
}