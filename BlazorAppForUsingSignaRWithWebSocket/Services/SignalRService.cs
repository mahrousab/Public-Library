using Microsoft.AspNetCore.SignalR.Client;
using System.Data.Common;

namespace BlazorAppForUsingSignaRWithWebSocket.Services
{
    public class SignalRService
    {
        private HubConnection? _hubConnection;

        public event Action<string, string>? OnMessageReceived;
        public async Task StartAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/hubs/notifications")
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                OnMessageReceived?.Invoke(user, message);
            });

            await _hubConnection.StartAsync();
        }

        public async Task SendAsync(string user, string message)
        {
            if (_hubConnection != null)
                await _hubConnection.InvokeAsync("SendMessage", user, message);
        }

       
    }
}
