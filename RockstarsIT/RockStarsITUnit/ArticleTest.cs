using System.Linq;
using RockstarsIT.Models;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace RockStarsITUnit
{
    public class ArticleTest
    {
        private TestDatabaseContext _context;
        public ArticleTest()
        {
            var options = new DbContextOptionsBuilder<TestDatabaseContext>()
           .UseInMemoryDatabase("ConnectionStrings.TestDatabaseConnection")
           .Options;
            this._context = new TestDatabaseContext(options);
        }
        [Fact]
        public void AddArticleTest()
        {
            //Arrange
            var article = new Article()
            {
                ArticleId   = 7,
                RockstarId  = 1,
                Title       = "UnitTest",
                Description = "How to test your app",
            };
            this._context.Add(article);
            //Act
            this._context.SaveChangesAsync();
            //Assert
            var result = _context.Article.Where(a => a.ArticleId == article.ArticleId).First();
            Assert.NotNull(result);
            Assert.NotNull(result.Title);
            Assert.NotNull(result.RockstarId);
            Assert.NotNull(result.Description);
            Assert.Equal(result.ArticleId, article.ArticleId);
            Assert.Equal(result.Title, article.Title);
            Assert.Equal(result.Description, article.Description);
            Assert.Equal(result.RockstarId, article.RockstarId);
        }
        [Fact]
        private void EditArticleTest()
        {
            //Arrange
            var newArticleData = new Article()
            {
                ArticleId = 7,
                RockstarId = 1,
                Title = "UnitTest2",
                Description = "How to test your app2",
            };
            _context.Update(newArticleData);
            //Act
            _context.SaveChangesAsync();
            //Assert
            var newData = _context.Article.Where(a => a.ArticleId == newArticleData.ArticleId).First();
            Assert.NotNull(newData);
            Assert.Equal(newData.Title, newArticleData.Title);
            Assert.Equal(newData.Description, newArticleData.Description);
            Assert.Equal(newData.RockstarId, newArticleData.RockstarId);
        }
        [Fact]
        private void DeleteArticleTest()
        {
            //Arrange
            var article = new Article()
            {
                ArticleId = 7,
                RockstarId = 1,
                Title = "UnitTest2",
                Description = "How to test your app2",
            };
            _context.Remove(article);
            //Act
            _context.SaveChangesAsync();
            //Assert
            Assert.Empty(_context.Article.Where(a => a.ArticleId == article.ArticleId));
        }
    }
}
