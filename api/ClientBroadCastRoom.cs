using Fleck;
using lib;
using System.Text.Json;


namespace WebSocket;

public class ClientBroadcastRoomDto : BaseDto
{

    public string message {  get; set; }
    public int roomId { get; set; }
}

public class ClientBroadcastRoom : BaseEventHandler<ClientBroadcastRoomDto>
{
    public override Task Handle(ClientBroadcastRoomDto dto, IWebSocketConnection socket)
    {
        var message = new ServerBrocastMessageWithUsername()
        {
            message = dto.message,
            username = StateService.Connections[socket.ConnectionInfo.Id].Username
        };
        Console.WriteLine(message);

        StateService.BroadcastToRoom(dto.roomId, JsonSerializer.Serialize(message));

        return Task.CompletedTask;
    }
}

public class ServerBrocastMessageWithUsername : BaseDto
{
    public string message { get; set;}
    public string username { get; set;}
}

