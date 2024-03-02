using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
namespace server.Hubs
{
	public class PlayersHub:Hub
	{
		public async Task SendFleetPlacements(object data)
		{
			Console.WriteLine(Context.ConnectionId);
			string connectionId = Context.ConnectionId;
			Console.WriteLine($"socket data:{data}");
            await Clients.Client(connectionId).SendAsync("ReceiveFleetsFromServer", $"Fleets recieved from {connectionId} are", data);
        }
        public async Task CreateGameRoom(string gameId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
            //Console.WriteLine($"Game: {Clients.Group(gameId)}");
            await Clients.Group(gameId).SendAsync("GameRoomCreated", gameId);
      
        }
        public async Task JoinGameRoom(string gameId)
        {
            Console.WriteLine(Context.User);
            await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        }
    }
}

