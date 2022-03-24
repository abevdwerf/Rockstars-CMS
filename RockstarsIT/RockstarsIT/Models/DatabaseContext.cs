using Microsoft.EntityFrameworkCore;

namespace RockstarsIT.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        
   /*     public DbSet<Rockstar> Rockstars { get; set; }
        public DbSet<Tribe> Tribes { get; set; }*/
        public DbSet<Article> Article { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            Tribe tribe1 = new Tribe()
            {
                TribeId = 1,
                Name = "NaamTribe1",
                Description = "Dit is tribe 1"
            };
            
            builder.Entity<Tribe>().HasData(tribe1);

            Rockstar rockstar1 = new Rockstar()
            {
                RockstarId = 1,
                Description = "test 123",
                LinkedIn = "linnked.com/",
                Tribe = null
            };

            builder.Entity<Rockstar>().HasData(rockstar1);

            Article article1 = new Article()
            {
                ArticleId = 1,
                RockstarId = 1,
                Title = "Tekst",
                Description = "test 123",
                Author = "The Noso",
            };

            builder.Entity<Article>().HasData(article1);
        }
    }
}