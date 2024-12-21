using Fleck;
using lib;
using System;
using System.Text.Json;



namespace WebSocket;

public class ClientEchoServerDto : BaseDto
{
    public string messageContent { get; set; }

}
public class ClientEchoServer : BaseEventHandler<ClientEchoServerDto>
{ 

    public override Task Handle(ClientEchoServerDto dto, IWebSocketConnection socket)
    {
       
        var echo = new ServerEchoClient()
        {
            echoValue = "echo: " + dto.messageContent
        };

       
        var messageToClient = JsonSerializer.Serialize(echo);
        Console.WriteLine("message back to client");
        Console.WriteLine(messageToClient);
        socket.Send(messageToClient);
        return Task.CompletedTask;
    }

}

public class ServerEchoClient: BaseDto
{
    public string echoValue { get; set; }
}
