using lib;
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
        public async Task Test1()
        {

            var wsClient = await new WebSocketTestClient().ConnectAsync();

            var wsClient2 = await new WebSocketTestClient().ConnectAsync();

            await wsClient.DoAndAssert(new ClientSignInDto()
            {
                Username = "bob"
            }, r => r.Count(dto => dto.eventType == nameof(ServerWelcomeUser)) == 1);
            

            await wsClient2.DoAndAssert(new ClientSignInDto()
            {
                Username = "Alice"
            }, r => r.Count(dto => dto.eventType == nameof(ServerWelcomeUser)) == 1);

            await wsClient.DoAndAssert(new ClientEnterRoomDto()
            {
                roomId = 1
            }, r => r.Count(dto => dto.eventType == nameof(ServerAddClientRoom)) == 1);

            await wsClient2.DoAndAssert(new ClientEnterRoomDto()
            {
                roomId = 1
            }, r => r.Count(dto => dto.eventType == nameof(ServerAddClientRoom)) == 1);


            await wsClient.DoAndAssert(new ClientBroadcastRoomDto()
            {
                roomId = 1,
                message = "hey alice"
            }, r => r.Count(dto => dto.eventType == nameof(ServerBrocastMessageWithUsername)) == 1);

            await wsClient2.DoAndAssert(new ClientBroadcastRoomDto()
            {
                roomId = 1,
                message = "hey bob"
            }, r => r.Count(dto => dto.eventType == nameof(ServerBrocastMessageWithUsername)) == 2);

   
        }
    }
}