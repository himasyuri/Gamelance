using Gamelance.Models;
using Gamelance.Models.Dto;

namespace Gamelance.Services.GameServices
{
    public interface IGameService
    {
        ValueTask<List<Game>> GetGameWithCategoriesAsync();
        ValueTask<List<Game>> GetAllGamesAsync();

        ValueTask<Game> GetGameAsync(long id);

        ValueTask<Game> GetGameAsync(string name);

        ValueTask<Game> CreateGameAsync(GameDto game);

        ValueTask<Game> EditGameAsync(GameDto game, long gameId);

        ValueTask DeleteGame(long gameId);

        ValueTask<List<OfferCategory>> GetCategoriesAsync(long gameId);

        ValueTask<List<OfferCategory>> GetCategoriesAsync(string gameName);

        ValueTask<OfferCategory> GetCategoryAsync(int categoryId, long gameId);

        ValueTask<OfferCategory> GetCategoryAsync(int categoryId, string gameName);

        ValueTask<OfferCategory> CreateCategoryAsync(OfferCategoryDto category, long gameId);

        ValueTask<OfferCategory> EditCategoryAsync(OfferCategoryDto category, int id, long gameId);

        ValueTask DeleteCategory(int id, long gameId);
    }
}
