using System.Text.Json;
using Websocket.Client;
using WebSocket;

namespace Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            StartUp.Start(null);
        }

        [Test]
        public void Test1()
        {
            var wsClient = new WebsocketClient(new Uri("ws://localhost:8181"));
            wsClient.MessageReceived.Subscribe(msg =>
            {
                Console.WriteLine("what we got from server" + msg.Text);
            });
            wsClient.Start();

            var message = new ClientEchoServerDto()
            {
                messageContent = "hey"
            };
            wsClient.Send(JsonSerializer.Serialize(message));
            Task.Delay(5000).Wait();
            wsClient.Send("hey");



        }
    }
}