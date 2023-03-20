using Microsoft.AspNetCore.SignalR;
using SignalR_Asp.netCoreWebAppDemo.DBClass;
using SignalR_Asp.netCoreWebAppDemo.Model;
using System.Collections.Concurrent;
using System.Net;
using System.Text;

namespace SignalR_Asp.netCoreWebAppDemo.Hubs
{
    public class MessageHub : Hub
    {
        private static readonly ConcurrentDictionary<string, ClientInfo> _clients =
         new ConcurrentDictionary<string, ClientInfo>();

        private readonly ConnectionManager _connectionManager;

        public MessageHub(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public Task SendMessageToAll(string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", message);
        }


        public Task SendMessageToCaller(string message)
        {
            return Clients.Caller.SendAsync("ReceiveMessage", message);
        }

        public Task SendMessageToUser(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("ReceiveMessages", message);
        }

        public Task JoinGroup(string group)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public Task SendMessageToGroup(string group, string message)
        {
            return Clients.Group(group).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            string macAddress = Context.GetHttpContext().Request.Query["macAddress"];
            if (!string.IsNullOrEmpty(macAddress))
            {
                string connectionId = Context.ConnectionId;
                ClientInfo clientInfo = new ClientInfo { MacAddress = macAddress, ConnectionId = connectionId };
                _clients[macAddress] = clientInfo;

                if (!string.IsNullOrEmpty(macAddress))
                {
                    await _connectionManager.AddUserConnection(macAddress, connectionId);
                }
            }
            await Clients.All.SendAsync("UserConnected", Context.ConnectionId);
            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connectionId = Context.ConnectionId;
            if (!string.IsNullOrEmpty(connectionId))
            {
                await _connectionManager.RemoveUserConnection(connectionId);
            }
            await Clients.All.SendAsync("UserDisconnected", Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);

        }

           //for Response from Client
        public string ResponseMessage(string connectionId, string message)
        {
            return $"ConnectionId:{connectionId} Message:{message}";

        }

        /////////for Console app

        public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
        {
            while (true)
            {
                yield return DateTime.UtcNow;
                await Task.Delay(1000, cancellationToken);
            }
        }

        public void SendMessage(string macAddress, string message)
        {
            if (_clients.TryGetValue(macAddress, out ClientInfo? clientInfo))
            {
                Clients.Client(clientInfo.ConnectionId).SendAsync("ReceiveMessage", message);
            }
        }
    }
}
