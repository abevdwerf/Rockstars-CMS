using System.Linq;
using RockstarsIT.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace RockStarsITUnit
{
    public class VideoTest
    {
        private TestDatabaseContext _context;
        public VideoTest()
        {
            var options = new DbContextOptionsBuilder<TestDatabaseContext>()
           .UseInMemoryDatabase("ConnectionStrings.TestDatabaseConnection")
           .Options;
            this._context = new TestDatabaseContext(options);
        }

        [Fact]
        public void Test1AddVideo()
        {
            //Arrange
            var video = new Video()
            {
                VideoId     = 7,
                Title       = "UnitTest Explanied",
                Description = "Unit test using ef",
                Link        = "youtube.com/watch?v=BL5Mdx1sUpQ",
                TribeId     = 1,
                RockstarId  = 1,
            };
            _context.Add(video);
            //Act
            _context.SaveChangesAsync();
            //Assert
            var result = _context.Videos.Where(v => v.VideoId == video.VideoId).First();
            Assert.NotNull(result);
            Assert.NotNull(result.Title);
            Assert.NotNull(result.RockstarId);
            Assert.NotNull(result.Description);
            Assert.NotNull(result.Link);
            Assert.NotNull(result.TribeId);
            Assert.Equal(result.VideoId, video.VideoId);
            Assert.Equal(result.Title, video.Title);
            Assert.Equal(result.Description, video.Description);
            Assert.Equal(result.RockstarId, video.RockstarId);
            Assert.Equal(result.TribeId, video.TribeId);
            Assert.Equal(result.Link, video.Link);
        }
        [Fact]
        public void Test2EditVideo()
        {
            //Arrange
            var newVideo = new Video()
            {
                VideoId = 7,
                Title = "UnitTest s2 Explanied",
                Description = "Unit test using ef2",
                Link = "youtube.com/watch?v=BL5Mdx1sUpQ",
                TribeId = 1,
                RockstarId = 1,
            };
            _context.Update(newVideo);
            //Act
            _context.SaveChangesAsync();
            //Assert
            var result = _context.Videos.Where(v => v.VideoId == newVideo.VideoId).First();
            Assert.NotNull(result);
            Assert.NotNull(result.Title);
            Assert.NotNull(result.RockstarId);
            Assert.NotNull(result.Description);
            Assert.NotNull(result.Link);
            Assert.NotNull(result.TribeId);
            Assert.Equal(result.VideoId, newVideo.VideoId);
            Assert.Equal(result.Title, newVideo.Title);
            Assert.Equal(result.Description, newVideo.Description);
            Assert.Equal(result.RockstarId, newVideo.RockstarId);
            Assert.Equal(result.TribeId, newVideo.TribeId);
            Assert.Equal(result.Link, newVideo.Link);
        }
        [Fact]
        public void Test3DeleteVideo()
        {
            //Arrange
            var video = new Video()
            {
                VideoId = 7,
                Title = "UnitTest s2 Explanied",
                Description = "Unit test using ef2",
                Link = "youtube.com/watch?v=BL5Mdx1sUpQ",
                TribeId = 1,
                RockstarId = 1
            };
            _context.Remove(video);
            //Act
            _context.SaveChangesAsync();
            //Assert
            Assert.Empty(_context.Videos.Where(v => v.VideoId == video.VideoId));
        }
    }
}
