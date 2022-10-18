using GameLibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryAPI.Controllers
{
    [Route("[action]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameContext _gameContext;

        public GameController(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        // GET ALL
        [HttpGet]
        public JsonResult Get()
        {
            var result = _gameContext.Games.ToList();
            return result.Count == 0 ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
        }

        // GET BY GENRE
        [HttpGet(template:"{genre}")]
        public JsonResult GetByGenre(string genre)
        {
            var result = _gameContext.Games.Where(g => g.GenresString.Contains(genre));
            return result.Count() == 0 ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
        }

        // GET
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var result = _gameContext.Games.Find(id);
            return result == null ? new JsonResult(NotFound()) : new JsonResult(Ok(result));
        }

        // POST, PUT
        [HttpPost]
        public JsonResult CreateGame(Game game)
        {
            if (game.Id == 0)
            {
                _gameContext.Games.Add(game);
            } 
            else
            {
                var GameInDb = _gameContext.Games.Find(game.Id);
                if (GameInDb == null)
                    return new JsonResult(NotFound());
                GameInDb.Name = game.Name;
                GameInDb.Developer = game.Developer;
                GameInDb.GenresString = game.GenresString;
            }

            _gameContext.SaveChanges();
            return new JsonResult(Ok(game));
        }

        // DELETE
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            var result = _gameContext.Games.Find(id);
            if (result == null)
                return new JsonResult(NotFound());

            _gameContext.Games.Remove(result);
            _gameContext.SaveChanges();

            return new JsonResult(Ok(result));
        }
    }
}
