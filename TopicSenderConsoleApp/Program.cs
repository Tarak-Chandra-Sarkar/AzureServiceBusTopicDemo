namespace TopicSenderConsoleApp
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Azure.ServiceBus;
    using Newtonsoft.Json;

    class Program
    {
        const string ServiceBusConnectionString = "<ServiceBusConnectionString>";
        const string TopicName = "asb-topic";
        static ITopicClient topicClient;

        public static async Task Main(string[] args)
        {
            const int numberOfMessages = 3;
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after sending all the messages.");
            Console.WriteLine("======================================================");

            // Send messages.
            await SendMessagesAsync(numberOfMessages);

            Console.ReadKey();

            await topicClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 1; i <= numberOfMessagesToSend; i++)
                {                    
                    AlertDetails alertDetails = new AlertDetails
                    {
                        ID = i,
                        Name = $"Message_{i}",
                        Code = $"Code_{i}",
                        Description = $"Description_{i}",
                        FromEmail = "fname.lname@hotmail.com",
                        ToEmail = "fname.lname@hotmail.com",
                        Subject = $"Subject of Alert_{i}",
                        Content = $"This is to notify that Alert_{i} has been generated!" + Environment.NewLine + "Please take action."

                    };

                    // Serialized the AlertDetails object to JSON string
                    string jsonString = JsonConvert.SerializeObject(alertDetails);

                    // Create a new message to send to the topic
                    var message = new Message(Encoding.UTF8.GetBytes(jsonString));

                    // Add some UserProperty to be used as for Filter at Topic Subscription
                    // message.UserProperties["Sender"] = "CSharpSenderApp";
                    message.UserProperties["TriggerFunction"] = "azure";

                    // Write the body of the message to the console
                    Console.WriteLine();
                    Console.WriteLine($"Sending message: {jsonString}");
                    Console.WriteLine();

                    // Send the message to the topic
                    await topicClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
