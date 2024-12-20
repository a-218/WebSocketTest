using Fleck;
using lib;
using System.Reflection;
using WebSocket;


var builder = WebApplication.CreateBuilder(args);

var clientEventHandler = builder.FindAndInjectClientEventHandlers(Assembly.GetExecutingAssembly());

var app = builder.Build();

var server = new WebSocketServer(location: "ws://0.0.0.0:8181");


server.Start(ws=>
{
    ws.OnOpen = () =>
    {
        StateService.AddConnection(ws);
    };
    ws.OnMessage = async message =>
    {
        try
        {
           await app.InvokeClientEventHandler(clientEventHandler, ws, message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
           Console.WriteLine(e.InnerException);
            Console.WriteLine(e.StackTrace);
        }

    };

});
Console.ReadLine();