# amazonmq-demo

This is a sample project to demonstrate how to send the messages to AmazonMQ - Active MQ TOPIC.
The TOPIC message consuming mechanism could support multiple consumers to execute the durable consuming process. In addtion, this sample also support the Dead-Letter-Queue design which provied the second chance to resolve the runtime exception issues.

## How to run the demo

Start the consumer.
Commandline arguments : 

args[0] - Specify the client id
args[1] - Specify the durable subscrition consumer name
args[2] - to enable runtime exception by "Y", otherwise set "N"

```
cd ~/amazonmq-demo/amazonmq-durable-consumer/bin/Debug/net7.0

dotnet amazonmq-durable-consumer.dll client-1 client-1-subscription Y
```

Start the producer.

```
cd ~/amazonmq-demo/amazonmq-producer/bin/Debug/net7.0

 dotnet amazonmq-producer.dll
```

## View the execution logs to check behaviors.

Producer
```
Message sent: Hello from C# to ActiveMQ! ... kim sent the 0 time message
Message sent: Hello from C# to ActiveMQ! ... kim sent the 1 time message
Message sent: Hello from C# to ActiveMQ! ... kim sent the 2 time message
Message sent: Hello from C# to ActiveMQ! ... kim sent the 3 time message
...
...
...

Message sent: Hello from C# to ActiveMQ! ... kim sent the 95 time message
Message sent: Hello from C# to ActiveMQ! ... kim sent the 96 time message
Message sent: Hello from C# to ActiveMQ! ... kim sent the 97 time message
Message sent: Hello from C# to ActiveMQ! ... kim sent the 98 time message
Message sent: Hello from C# to ActiveMQ! ... kim sent the 99 time message
```

Consumer
```
args[0]client-2
args[1]client-2-subscription
Chaos test Y
Listening for messages on topic 'demo-topic' with subscription 'client-2-subscription'
Received message: Hello from C# to ActiveMQ! ... kim sent the 0 time message
counter is 0
處理消息時出現異常
Received message: Hello from C# to ActiveMQ! ... kim sent the 1 time message
counter is 1
Received message: Hello from C# to ActiveMQ! ... kim sent the 2 time message
counter is 2
Received message: Hello from C# to ActiveMQ! ... kim sent the 3 time message
counter is 3
Received message: Hello from C# to ActiveMQ! ... kim sent the 4 time message
counter is 4
Received message: Hello from C# to ActiveMQ! ... kim sent the 5 time message
counter is 5
處理消息時出現異常

...
...

處理消息時出現異常
Received message: Hello from C# to ActiveMQ! ... kim sent the 96 time message
counter is 96
Received message: Hello from C# to ActiveMQ! ... kim sent the 97 time message
counter is 97
Received message: Hello from C# to ActiveMQ! ... kim sent the 98 time message
counter is 98
Received message: Hello from C# to ActiveMQ! ... kim sent the 99 time message
counter is 99
```
