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
    public class PlayerGameInfo
    {
        public string? PlayerName { get; set; }
        public string? GameId { get; set; }
    }
    public class FleetsData
    {
        public List<List<int>>? FleetsPalcements { get; set; }
        public string? PlayedBy { get; set; }
        public string? GameId { get; set; }
    }
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
        public async Task<IActionResult> AddPlayerToGame()
        {
            
            try
            {
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string body = await reader.ReadToEndAsync();
                Console.WriteLine(body);
                PlayerGameInfo playerGameInfo  = JsonConvert.DeserializeObject<PlayerGameInfo>(body);
                string? gameId = playerGameInfo.GameId;
                string? playerName = playerGameInfo.PlayerName;
                var game = await _playersDBContext.RetrieveGameByGameId(gameId);
                if (game == null)
                {
                    return NotFound($"Game with gameId '{gameId}' not found.");
                }
                game.Game.Players ??= new List<Player>();
                game.Game.Players.Add(new Player { PlayerName = playerName });
                await _playersDBContext.UpdateGame(game);
                return Ok($"Player '{playerName}' added to game with gameId '{gameId}'.");
         
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> SetFleets()
        {
            try
            {
                using StreamReader reader = new(Request.Body, Encoding.UTF8);
                string requestBody = await reader.ReadToEndAsync();
                FleetsData recievedPlacement = JsonConvert.DeserializeObject<FleetsData>(requestBody);
                List<List<int>>? fleet = recievedPlacement?.FleetsPalcements;
                string? playerName = recievedPlacement?.PlayedBy;
                string? gameId = recievedPlacement?.GameId;
                await _playersDBContext.UpdateFleet(gameId, playerName, fleet);
                return Ok($"{playerName} move: {fleet} recorded");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        
    }
}

