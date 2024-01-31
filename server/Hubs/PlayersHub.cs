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
		
	}
}

