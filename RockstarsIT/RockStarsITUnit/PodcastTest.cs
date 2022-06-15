using System.Linq;
using RockstarsIT.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;


namespace RockStarsITUnit
{
    public class PodcastTest
    {
        private TestDatabaseContext _context;
        public PodcastTest()
        {
            var options = new DbContextOptionsBuilder<TestDatabaseContext>()
           .UseInMemoryDatabase("ConnectionStrings.TestDatabaseConnection")
           .Options;
            this._context = new TestDatabaseContext(options);
        }
        [Fact]
        public void AddPodcastTest()
        {
            var podcast = new Podcast()
            {
                PodcastId = 5,
                URL       = "https://open.spotify.com/show/1ijid0felknQbJ3vhGm9Et",
                TribeId   = 1,
            };
            this._context.Add(podcast);
            //Act
            this._context.SaveChangesAsync();
            //Assert
            var result = _context.Podcasts.Where(p => p.PodcastId == podcast.PodcastId).First();
            Assert.NotNull(result);
            Assert.NotNull(result.URL);
            Assert.NotNull(result.TribeId);
            Assert.Equal(result.PodcastId, podcast.PodcastId);
            Assert.Equal(result.URL, podcast.URL);
            Assert.Equal(result.TribeId, podcast.TribeId);
        }
        [Fact]
        private void EditPodcastTest()
        {
            var newPodcast = new Podcast()
            {
                PodcastId = 5,
                URL       = "https://open.spotify.com/show/1ijid0felknQbJ3vhGm9Et",
                TribeId   = 2,
            };
            this._context.Add(newPodcast);
            //Act
            this._context.SaveChangesAsync();
            //Assert
            var result = _context.Podcasts.Where(p => p.PodcastId == newPodcast.PodcastId).First();
            Assert.NotNull(result);
            Assert.NotNull(result.URL);
            Assert.NotNull(result.TribeId);
            Assert.Equal(result.PodcastId, newPodcast.PodcastId);
            Assert.Equal(result.URL, newPodcast.URL);
            Assert.Equal(result.TribeId, newPodcast.TribeId);
        }
        [Fact]
        private void DeletePodcastTest()
        {
            //Arrange
            var podcast = new Podcast()
            {
                PodcastId = 5,
                URL       = "https://open.spotify.com/show/1ijid0felknQbJ3vhGm9Et",
                TribeId   = 1,
            };
            _context.Remove(podcast);
            //Act
            _context.SaveChangesAsync();
            //Assert
            Assert.Empty(_context.Podcasts.Where(p => p.PodcastId == podcast.PodcastId));
        }
    }
}
