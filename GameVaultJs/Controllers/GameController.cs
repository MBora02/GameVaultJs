using GameVaultJs.Data;
using GameVaultJs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GameVaultJs.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GameList(Game games)
        {
            return new JsonResult(games);
        }


        [HttpPost]
        public JsonResult AddGame(Game game)
        {
            var g = new Game()
            {
                Name = game.Name,
                Developer = game.Developer,
                Platform = game.Platform,
                ReleaseDate = game.ReleaseDate,
                Price = game.Price,
                Description= game.Description
            };
            _context.Games.Add(g);
            _context.SaveChanges();
            return new JsonResult("Data saved");
        }


        [HttpGet]
        public JsonResult Edit(int id)
        {
            var data = _context.Games.Where(m => m.Id == id).SingleOrDefault();
            return new JsonResult(data);
        }

        [HttpPost]
        public JsonResult Update(Game game)
        {
            _context.Update(game);
            _context.SaveChanges();
            return new JsonResult("Data updated");
        }

        public JsonResult Delete(int id)
        {
            var data = _context.Games.Where(m => m.Id == id).SingleOrDefault();
            _context.Games.Remove(data);
            _context.SaveChanges();
            return new JsonResult("Data deleted");
        }
    }
}
