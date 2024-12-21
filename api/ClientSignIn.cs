using Fleck;
using lib;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;

namespace WebSocket;


    public class ClientSignInDto : BaseDto
    {
        public string Username { get; set; }
    }

    public class ClientSignIn : BaseEventHandler<ClientSignInDto>
    {
        public override Task Handle(ClientSignInDto dto, IWebSocketConnection socket)
        {
            StateService.Connections[socket.ConnectionInfo.Id].Username = dto.Username;
            socket.Send(JsonSerializer.Serialize(new ServerWelcomeUser()));
            return Task.CompletedTask;
        }
    }

public class ServerWelcomeUser: BaseDto
{

}

