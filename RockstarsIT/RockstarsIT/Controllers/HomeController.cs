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
using RockstarsIT.Classes;
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
            List<DashboardContent> content = new List<DashboardContent>();

            if (_context.Article.Any())
            {
                List<Article> articles = _context.Article.OrderByDescending(item => item.ViewCount).Take(5).ToList();
                //listOfObjects.AddRange(articles);
                foreach (Article article in articles)
                {
                    DashboardContent dc = new DashboardContent();
                    dc.Content = article;
                    dc.ViewCount = article.ViewCount;
                    content.Add(dc);
                }
            }
            if (_context.Podcasts.Any())
            {
                List<Video> videos = _context.Videos.OrderByDescending(item => item.ViewCount).Take(5).ToList();
                //listOfObjects.AddRange(videos);
                foreach (Video video in videos)
                {
                    DashboardContent dc = new DashboardContent();
                    dc.Content = video;
                    dc.ViewCount = video.ViewCount;
                    content.Add(dc);
                }
            }
            if (_context.Podcasts.Any())
            {
                List<Podcast> podcasts = _context.Podcasts.OrderByDescending(item => item.ViewCount).Take(5).ToList();
                //listOfObjects.AddRange(podcasts);
                foreach (Podcast podcast in podcasts)
                {
                    DashboardContent dc = new DashboardContent();
                    dc.Content = podcast;
                    dc.ViewCount = podcast.ViewCount;
                    content.Add(dc);
                }
            }

            List<DashboardContent> sortedList = content.OrderByDescending(o => o.ViewCount).Take(5).ToList();

            //List<Podcast> podcasts = _context.Podcasts.OrderByDescending(item => item.ViewCount).Take(5).ToList();

            //List<Object> list = new List<Object>()

            //int test = 0;
            //listOfObjects.;
            //int temp = 0;
            foreach (var item in listOfObjects)
            {
                int iets = (int)item.GetType().GetProperty("ViewCount").GetValue(item);
        

                //foreach (var propertyInfo in item)
                //{
                //    var iets = propertyInfo;
                //}
            }
            //for (int j = 0; j <= listOfObjects.Count - 2; j++)
            //{
            //    for (int i = 0; i <= listOfObjects.Count - 2; i++)
            //    {
            //        if ((int)listOfObjects[i].GetType().GetProperty("ViewCount").GetValue(i) > (int)listOfObjects[i + 1].GetType().GetProperty("ViewCount").GetValue(i))
            //        {
            //            temp = (int)listOfObjects[i + 1].GetType().GetProperty("ViewCount").GetValue(i);
            //            listOfObjects[i + 1] = (int)listOfObjects[i].GetType().GetProperty("ViewCount").GetValue(i);
            //            listOfObjects[i] = temp;
            //        }
            //    }
            //}
            //List<Object> cmonbined = new List<Object>();
            //cmonbined.Add(articles);
            //cmonbined.Add(videos);
            //cmonbined.Add(podcasts);

            //articles.Select(a => a.ViewCount)
            //List<int> test = _context.Article.Select(p => p.ViewCount);
            //ViewBag.Students = GetStudents();

            ViewData["content"] = sortedList;

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