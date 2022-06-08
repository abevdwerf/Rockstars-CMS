using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
            List<DashboardContent> sortedList = GetTopFiveContent();
            if (sortedList.Count == 5)
            {
                ViewData["content"] = sortedList;
                if ((double)sortedList[0].ViewCount < 100)
                {
                    ViewData["percentageObject1"] = 100;
                }
                else
                {
                    ViewData["percentageObject1"] = (double)sortedList[0].ViewCount;
                }

                ViewData["percentageObject2"] = (((double)sortedList[1].ViewCount / (double)sortedList[0].ViewCount) * 100).ToString(CultureInfo.InvariantCulture);
                ViewData["percentageObject3"] = (((double)sortedList[2].ViewCount / (double)sortedList[0].ViewCount) * 100).ToString(CultureInfo.InvariantCulture);
                ViewData["percentageObject4"] = (((double)sortedList[3].ViewCount / (double)sortedList[0].ViewCount) * 100).ToString(CultureInfo.InvariantCulture);
                ViewData["percentageObject5"] = (((double)sortedList[4].ViewCount / (double)sortedList[0].ViewCount) * 100).ToString(CultureInfo.InvariantCulture);
            }

            ViewData["conceptContent"] = GetNumberConceptContent();
            ViewData["publishedContent"] = GetNumberPublishedContent();
            return View();
        }


        private int GetNumberConceptContent()
        {
            int conceptContent = 0;
            conceptContent += _context.Article.Count(item => item.PublishedStatus == false);
            conceptContent += _context.Videos.Count(item => item.PublishedStatus == false);
            conceptContent += _context.Podcasts.Count(item => item.PublishedStatus == false);
            return conceptContent;
        }

        private int GetNumberPublishedContent()
        {
            int publishedContent = 0;
            publishedContent = _context.Article.Count(item => item.PublishedStatus == true);
            publishedContent = _context.Videos.Count(item => item.PublishedStatus == true);
            publishedContent = _context.Podcasts.Count(item => item.PublishedStatus == true);
            return publishedContent;
        }

        private List<DashboardContent> GetTopFiveContent()
        {
            List<DashboardContent> content = new List<DashboardContent>();
            if (_context.Article.Any())
            {
                List<Article> articles = _context.Article.OrderByDescending(item => item.ViewCount).Take(5).ToList();
                foreach (Article article in articles)
                {
                    DashboardContent dc = new DashboardContent();
                    dc.SVGLocation = "/icons/Article.svg";
                    dc.Id = article.ArticleId;
                    dc.Controller = "Article";
                    dc.ModelName = "Artikel";
                    dc.Content = article;
                    dc.ViewCount = article.ViewCount;
                    content.Add(dc);
                }
            }
            if (_context.Podcasts.Any())
            {
                List<Video> videos = _context.Videos.OrderByDescending(item => item.ViewCount).Take(5).ToList();
                foreach (Video video in videos)
                {
                    DashboardContent dc = new DashboardContent();
                    dc.SVGLocation = "/icons/Video.svg";
                    dc.Id = video.VideoId;
                    dc.Controller = "Video";
                    dc.ModelName = "Video";
                    dc.Content = video;
                    dc.ViewCount = video.ViewCount;
                    content.Add(dc);
                }
            }
            if (_context.Podcasts.Any())
            {
                List<PodcastEpisode> podcasts = _context.PodcastEpisodes.OrderByDescending(item => item.ViewCount).Take(5).ToList();
                foreach (PodcastEpisode podcast in podcasts)
                {
                    DashboardContent dc = new DashboardContent();
                    dc.SVGLocation = "/icons/Mic.svg";
                    dc.Id = podcast.PodcastEpisodeId;
                    dc.Controller = "Podcast";
                    dc.ModelName = "Podcast";
                    dc.Content = podcast;
                    dc.ViewCount = podcast.ViewCount;
                    content.Add(dc);
                }
            }

            List<DashboardContent> sortedList = content.OrderByDescending(o => o.ViewCount).Take(5).ToList();
            return sortedList;
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