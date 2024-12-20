using Fleck;

namespace WebSocket;

public class WebSocketMetaData(IWebSocketConnection connection)
{
    public IWebSocketConnection Connection { get; set; } = connection;

    public string Username { get; set; }

}
public class StateService
{
    public static Dictionary<Guid, WebSocketMetaData> Connections = new();

    public static Dictionary<int, HashSet<Guid>> Rooms = new();

    public static bool AddConnection(IWebSocketConnection ws)
    {
        return Connections.TryAdd(ws.ConnectionInfo.Id, new WebSocketMetaData(ws));

    }

    public static bool AddToRoom(IWebSocketConnection ws, int room)
    {
        if (!Rooms.ContainsKey(room))
        {
            Console.WriteLine("Creating room " + room);
            Rooms.Add(room, new HashSet<Guid>());
        }
       return Rooms[room].Add(ws.ConnectionInfo.Id);
    }

    public static void BroadcastToRoom(int room, string message)
    {
        Console.WriteLine("message room " + message);
        if (Rooms.TryGetValue(room, out var guids))
            foreach (var guid in guids)
            {
                if (Connections.TryGetValue(guid, out var ws))
                    ws.Connection.Send(message);
            }
    }
}
