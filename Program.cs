using Fleck;

var server = new WebSocketServer(location: "ws://0.0.0.0:8181");

server.Start(ws=>
{

    var wsConnections = new List<IWebSocketConnection>();

    ws.OnOpen = () =>
    {
        wsConnections.Add(ws);
    };
    ws.OnMessage = message =>
    {
        foreach (var webSocketConnection in wsConnections)
        {
            Console.WriteLine(message);
            webSocketConnection.Send(message);
        }

    };

});

WebApplication.CreateBuilder(args).Build().Run();