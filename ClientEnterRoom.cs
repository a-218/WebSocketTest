using Fleck;
using lib;
using System.Text.Json;


namespace WebSocket;

public class ClientEnterRoomDto : BaseDto
{
    public int roomId { get; set; }
}
public class ServerAddClientRoom : BaseDto
{
    public string message { get; set; }
}

public class ClientEnterRoom : BaseEventHandler<ClientEnterRoomDto>
{

    public override Task Handle(ClientEnterRoomDto dto, IWebSocketConnection socket)
    {
        var isSuccess = StateService.AddToRoom(socket, dto.roomId);
        socket.Send(JsonSerializer.Serialize(new ServerAddClientRoom()
        {
            message = "you were add to Room" + dto.roomId
        }));
        return Task.CompletedTask;
    }
}

  
