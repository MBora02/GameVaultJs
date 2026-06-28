using GameVaultJs.Data;
using GameVaultJs.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameVaultJs.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public NewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult NewsList()
        {
            var news = _context.News.ToList();
            return new JsonResult(news);
        }

        [HttpPost]
        public JsonResult AddNews(News news)
        {
            var n = new News()
            {
                Title = news.Title,
                Content = news.Content,
                PublishDate = news.PublishDate,
            };
            _context.News.Add(n);
            _context.SaveChanges();
            return new JsonResult("Data saved");
        }




        [HttpGet]
        public JsonResult Edit(int id)
        {
            var data = _context.News.Where(m => m.Id == id).SingleOrDefault();
            return new JsonResult(data);
        }

        [HttpPost]
        public JsonResult Update(News news)
        {
            _context.Update(news);
            _context.SaveChanges();
            return new JsonResult("Data updated");
        }

        public JsonResult Delete(int id)
        {
            var data = _context.News.Where(m => m.Id == id).SingleOrDefault();
            _context.News.Remove(data);
            _context.SaveChanges();
            return new JsonResult("Data deleted");
        }
    }
}
