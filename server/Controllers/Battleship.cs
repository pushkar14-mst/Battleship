using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.SignalR;
using server.Hubs;
using server.Models;
using server.Data;
using MongoDB.Bson;
using Newtonsoft.Json;
namespace server.Controllers
{
    public class Battleship : Controller
    {
        private IHubContext<PlayersHub> _hub;
        private readonly MongoDBServices _playersDBContext;
        public Battleship(IHubContext<PlayersHub> hub, MongoDBServices playersDBContext)
        {
            _hub = hub;
            _playersDBContext = playersDBContext;
        }
        [HttpGet]
        public async Task<IActionResult> RetrieveGame([FromQuery] string gameCode)
        {
            Console.WriteLine("Here");
            try
            {
                PlayersModel retrievedGame = await _playersDBContext.RetrieveGameByGameId(gameCode);
                if (retrievedGame == null)
                {
                    return NotFound($"Game with gameId '{gameCode}' not found.");
                }
                return Ok(retrievedGame);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> NewGame()
        {
            try
            {
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string body = await reader.ReadToEndAsync();
                Console.WriteLine(body);
                PlayersModel newGame = JsonConvert.DeserializeObject<PlayersModel>(body);
                Console.WriteLine("newGame:", newGame);
                await _playersDBContext.CreateAsync(newGame);
                return Ok("New Game created successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
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

