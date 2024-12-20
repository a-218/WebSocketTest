using Fleck;
using lib;

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

            return Task.CompletedTask;
        }
    }

