using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using server.Hubs;
namespace server.Controllers
{
    public class Battleship : Controller
    {
        private IHubContext<PlayersHub> _hub;
        public Battleship(IHubContext<PlayersHub> hub)
        {
            _hub = hub;
        }
        
        [HttpGet]
        public JsonResult Home()
        {
            var res=new JsonResult(new { message = "Welcome to Battleship server" });
            return res;
        }
        [HttpPost]
        public async Task<IActionResult> GetFleets()
        {
            try
            {
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string requestBody = await reader.ReadToEndAsync();
                Console.WriteLine(requestBody);
                return Ok("Request body received successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}

