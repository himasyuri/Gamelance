using Gamelance.Models;
using Gamelance.Models.Dto;
using Gamelance.Services.GameServices;
using Microsoft.AspNetCore.Mvc;


namespace Gamelance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesAndCategoriesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesAndCategoriesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("GamesWithCategories")]
        public async Task<ActionResult> GetGamesWithCategories()
        {
            return Ok(await _gameService.GetGameWithCategoriesAsync());
        }

        [HttpGet("Games")]
        public async Task<List<Game>> GetGames()
        {
            return await _gameService.GetAllGamesAsync();
        }

        [HttpGet("Categories/{gameId}")]
        public async Task<List<OfferCategory>> GetCategories(long gameId)
        {
            return await _gameService.GetCategoriesAsync(gameId);
        }

        [HttpGet("Game/{id}")]
        public async Task<Game> GetGame(long id)
        {
            return await _gameService.GetGameAsync(id);
        }

        [HttpGet("Game")]
        public async Task<Game> GetGameByName(string name)
        {
            return await _gameService.GetGameAsync(name);
        }

        [HttpGet("Category/{gameId}/{id}")]
        public async Task<OfferCategory> GetCategory(int id, long gameId)
        {
            return await _gameService.GetCategoryAsync(id, gameId);
        }

        [HttpGet("Category-by-game-name/{name}/{id}")]
        public async Task<OfferCategory> GetCategoryByGameName(int id, string name)
        {
            return await _gameService.GetCategoryAsync(id, name);
        }

        [HttpPost("New-Game")]
        public async Task<ActionResult<Game>> CreateGame([FromBody] GameDto game)
        {
            return await _gameService.CreateGameAsync(game);
        }

        [HttpPost("New-Category{gameId}")]
        public async Task<ActionResult> CreateCategory([FromBody] OfferCategoryDto offerCategory, long gameId)
        {
            var result = await _gameService.CreateCategoryAsync(offerCategory, gameId);
            return Ok(
                new
                {
                    result.CategoryId,
                    result.CategoryName,
                    result.Game.Name
                });
        }

        [HttpPut("EditGame/{id}")]
        public async Task<ActionResult<Game>> EditGame(long id, [FromBody] GameDto game)
        {
            return await _gameService.EditGameAsync(game, id);
        }

        [HttpPut("EditCategory/{gameId}/{id}")]
        public async Task<ActionResult<OfferCategory>> EditCategory(long gameId, [FromBody] OfferCategoryDto category, int id)
        {
            return await _gameService.EditCategoryAsync(category, id, gameId);
        }

        [HttpDelete("DeleteGame{id}")]
        public async void DeleteGame(long id)
        {
            await _gameService.DeleteGame(id);
        }

        [HttpDelete("DeleteCategory{gameId}")]
        public async void DeleteCategory(long gameId, int id)
        {
            await _gameService.DeleteCategory(id, gameId);
        }
    }
}
