using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {

            List<Article> articles = _context.Article.OrderByDescending(item => item.ViewCount).Take(5).ToList();
            List<Video> videos = _context.Videos.OrderByDescending(item => item.ViewCount).Take(5).ToList();
            List<Podcast> podcasts = _context.Podcasts.OrderByDescending(item => item.ViewCount).Take(5).ToList();

            List<Object> cmonbined = new List<Object>();
            cmonbined.Add(articles);
            cmonbined.Add(videos);
            cmonbined.Add(podcasts);

            //articles.Select(a => a.ViewCount)
            //List<int> test = _context.Article.Select(p => p.ViewCount);
            //ViewBag.Students = GetStudents();
            return View();
        }

        public IActionResult buttons()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}