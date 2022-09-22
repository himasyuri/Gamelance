using Gamelance.Data;
using Gamelance.Models;
using Gamelance.Models.Dto;

namespace Gamelance.Services.GameServices
{
    public class GameService : IGameService
    {
        private readonly DataContext _context;

        public GameService(DataContext context)
        {
            _context = context;
        }

        public async ValueTask<OfferCategory> CreateCategoryAsync(OfferCategoryDto model, long gameId)
        {
            Game? game = await _context.Games.FindAsync(gameId);
            
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game not found");
            }

            OfferCategory offerCategory = new OfferCategory
            {
                CategoryName = model.Name,
                Game = game,
            };

            await _context.OfferCategories.AddAsync(offerCategory);
            await _context.SaveChangesAsync(); 
            
            return offerCategory;
        }

        public async ValueTask<Game> CreateGameAsync(GameDto model)
        {
            Game game = new Game
            {
                Name = model.Name,
            };

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();

            return game;
        }

        public ValueTask DeleteCategory(int id, long gameId)
        {
            OfferCategory? category =  _context.OfferCategories.
                                     FirstOrDefault(p => p.GameId == gameId && p.CategoryId == id);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Game category not found");
            }
            
            _context.OfferCategories.Remove(category);
             _context.SaveChanges();

            return ValueTask.CompletedTask;
        }

        public ValueTask DeleteGame(long gameId)
        {
            Game? game = _context.Games.Find(gameId);

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game  not found");
            }

            _context.Games.Remove(game);
             _context.SaveChanges();

            return ValueTask.CompletedTask;
        }

        public async ValueTask<OfferCategory> EditCategoryAsync(OfferCategoryDto model, int id, long gameId)
        {
            OfferCategory? category = await _context.OfferCategories.
                                     FirstOrDefaultAsync(p => p.GameId == gameId && p.CategoryId == id);
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Game category not found");
            }

            category.CategoryName = model.Name;

            await _context.SaveChangesAsync();

            return category;
        }

        public async ValueTask<Game> EditGameAsync(GameDto model, long gameId)
        {
            Game? game = await _context.Games.FindAsync(gameId);

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game not found");
            }

            if (model.Name != null)
            {
                game.Name = model.Name;
            }

            await _context.SaveChangesAsync();

            return game;
        }

        public async ValueTask<List<Game>> GetAllGamesAsync()
        {
            return await _context.Games.ToListAsync();
        }

        public async ValueTask<List<OfferCategory>> GetCategoriesAsync(long gameId)
        {
            return await _context.OfferCategories.Where(p => p.GameId == gameId)
                                                 .ToListAsync();
        }

        public async ValueTask<List<OfferCategory>> GetCategoriesAsync(string gameName)
        {
            Game? game = await _context.Games.FindAsync(gameName);

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game not found");
            }

            return await _context.OfferCategories.Where(p => p.Game == game).ToListAsync();
        }

        public async ValueTask<OfferCategory> GetCategoryAsync(int categoryId, long gameId)
        {
            return await _context.OfferCategories.
                FirstOrDefaultAsync(p => p.GameId == gameId && p.CategoryId == categoryId) ??
                throw new ArgumentException("Game Category not found");
        }

        public async ValueTask<OfferCategory> GetCategoryAsync(int categoryId, string gameName)
        {
            Game? game = await _context.Games.FirstOrDefaultAsync(p => p.Name == gameName);

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game not foud");
            }

            return await _context.OfferCategories.
                FirstOrDefaultAsync(p => p.Game == game && p.CategoryId == categoryId)
                ?? throw new ArgumentException("Game category not found");
        }

        public async ValueTask<Game> GetGameAsync(long id)
        {
            Game? game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game not found");
            }

            game.OfferCategories = await _context.OfferCategories.Where(p => p.GameId == id)
                                                                 .ToListAsync();
            return game;
        }

        public async ValueTask<Game> GetGameAsync(string name)
        {
            Game? game = await _context.Games.FirstOrDefaultAsync(p => p.Name == name);

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game), "Game not found");
            }

            game.OfferCategories = await _context.OfferCategories.Where(p => p.GameId == game.GameId)
                                                                 .ToListAsync();

            return game;
        }

        public async ValueTask<List<Game>> GetGameWithCategoriesAsync()
        {
            List<Game> games = await _context.Games.ToListAsync();

            foreach (var game in games)
            {
                game.OfferCategories = await _context.OfferCategories.Where(p => p.GameId == game.GameId)
                                                                    .ToListAsync();
            }

            return games;
        }
    }
}
