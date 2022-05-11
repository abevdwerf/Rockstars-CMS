using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RockstarsIT.Models;

namespace RockstarsIT.Controllers
{
    [Authorize]
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
            //var test = _context.Videos.Select(a => a.ViewCount).Union(_context.Article.Select(v => v.ViewCount)).Union(_context.Podcasts.Select(p => p.ViewCount)).Take(5).ToList();
            //var articles = _context.Article.Select(item => new Dictionary<string, int>() { "test", item.ArticleId }).ToList();
            //var videos = _context.Videos.Select(item => new List<int> { item.ViewCount, item.VideoId }).ToList();
            //var podcasts = _context.Podcasts.Select(item => new List<string> { item.ViewCount.ToString(), item.PodcastId.ToString() }).ToList();

            //articles.AddRange(videos);
            //articles.AddRange(podcasts);
            ArrayList listOfObjects = new ArrayList();
            ArrayList finalList = new ArrayList();

            if (_context.Article.Any())
            {
                List<Article> articles = _context.Article.OrderByDescending(item => item.ViewCount).Take(5).ToList();
                listOfObjects.AddRange(articles);
            }
            if (_context.Podcasts.Any())
            {
                List<Video> videos = _context.Videos.OrderByDescending(item => item.ViewCount).Take(5).ToList(); 
                listOfObjects.AddRange(videos);
            }
            if (_context.Podcasts.Any())
            {
                List<Podcast> podcasts = _context.Podcasts.OrderByDescending(item => item.ViewCount).Take(5).ToList();
                listOfObjects.AddRange(podcasts);
            }

            //List<Podcast> podcasts = _context.Podcasts.OrderByDescending(item => item.ViewCount).Take(5).ToList();

            //List<Object> list = new List<Object>()

            int test = 0;
            //listOfObjects.Sort((o, a));
            foreach (var item in listOfObjects)
            {
                int iets = (int)item.GetType().GetProperty("ViewCount").GetValue(item);
                if (iets > test)
                {

                }

                //foreach (var propertyInfo in item)
                //{
                //    var iets = propertyInfo;
                //}
            }

            //List<Object> cmonbined = new List<Object>();
            //cmonbined.Add(articles);
            //cmonbined.Add(videos);
            //cmonbined.Add(podcasts);

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