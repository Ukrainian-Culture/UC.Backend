using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;

namespace Ukranian_Culture.Backend.Services;

public class OnlineUsersHub : Hub
{
    private static readonly ConcurrentDictionary<int, bool> OnlineUsers = new();
    private static int _onlineCount;

    public override async Task OnConnectedAsync()
    {
        _onlineCount += 1;
        if (!OnlineUsers.TryAdd(_onlineCount, true))
            OnlineUsers.TryUpdate(_onlineCount, true, false);

        await UpdateOnlineUsers();

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {

        if (!OnlineUsers.TryRemove(_onlineCount, out _))
            OnlineUsers.TryUpdate(_onlineCount, false, true);
        _onlineCount -= 1;

        await UpdateOnlineUsers();

        await base.OnDisconnectedAsync(exception);
    }

    private Task UpdateOnlineUsers()
    {
        var count = GetOnlineUsersCount();
        return Clients.All.SendAsync("UpdateOnlineUsers", count);
    }

    private static int GetOnlineUsersCount()
    {
        return OnlineUsers.Count(p => p.Value);
    }
}