using GameVaultJs.Data;
using GameVaultJs.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameVaultJs.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;
        public GenreController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GenreList()
        {
            var genre = _context.Genres.ToList();
            return new JsonResult(genre);
        }


        [HttpPost]
        public JsonResult AddGenre(Genre genre)
        {
            var g = new Genre()
            {
                Name = genre.Name,
                Description = genre.Description,
                GameCount=genre.GameCount
            };
            _context.Genres.Add(g);
            _context.SaveChanges();
            return new JsonResult("Data saved");
        }


        [HttpGet]
        public JsonResult Edit(int id)
        {
            var data = _context.Genres.Where(m => m.Id == id).SingleOrDefault();
            return new JsonResult(data);
        }

        [HttpPost]
        public JsonResult Update(Genre genre)
        {
            _context.Update(genre);
            _context.SaveChanges();
            return new JsonResult("Data updated");
        }

        public JsonResult Delete(int id)
        {
            var data = _context.Genres.Where(m => m.Id == id).SingleOrDefault();
            _context.Genres.Remove(data);
            _context.SaveChanges();
            return new JsonResult("Data deleted");
        }
    }
}
