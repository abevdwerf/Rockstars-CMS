using Microsoft.EntityFrameworkCore;

namespace RockstarsIT.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }
        
        public DbSet<Rockstar> Rockstars { get; set; }
        public DbSet<Tribe> Tribes { get; set; }
        public DbSet<TribeImages> TribeImages { get; set; }
        public DbSet<TribeTextBlock> TribeTextBlocks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<ArticleImages> ArticleImages { get; set; }
        public DbSet<ArticleTextBlocks> ArticleTextBlocks { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ArticleTextBlockTranslation> ArticleTextBlockTranslations { get; set; }
        public DbSet<ArticleContent> ArticleContents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ArticleTextBlocks>()
            .HasOne(a => a.Article)
            .WithMany(b => b.ArticleTextBlocks)
            .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ArticleImages>()
            .HasOne(a => a.Article)
            .WithMany(b => b.ArticleImages)
            .OnDelete(DeleteBehavior.Cascade);

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
                Name = "Erik",
                Description = "test 123",
                LinkedIn = "linked.com/",
                Tribe = null
            };

            builder.Entity<Rockstar>().HasData(rockstar1);

            Role role1 = new Role()
            {
                RoleId = 1,
                Name = "Tribe member"
            };
            Role role2 = new Role()
            {
                RoleId = 2,
                Name = "Tribe Lead"
            };
            Role role3 = new Role()
            {
                RoleId = 3,
                Name = "Special agent"
            };
            Role role4 = new Role()
            {
                RoleId = 4,
                Name = "Auteur"
            };

            builder.Entity<Role>().HasData(role1);
            builder.Entity<Role>().HasData(role2);
            builder.Entity<Role>().HasData(role3);
            builder.Entity<Role>().HasData(role4);

            Article article1 = new Article()
            {
                ArticleId = 1,
                RockstarId = 1
            };

            builder.Entity<Article>().HasData(article1);

            ArticleContent articleContent1 = new ArticleContent()
            {
                ArticleTranslationId = 1,
                Title = "Tekst",
                LanguageId = 1,
                ArticleId = 1
            };

            builder.Entity<ArticleContent>().HasData(articleContent1);

            ArticleTextBlocks articleTextBlocks1 = new ArticleTextBlocks()
            {
                ArticleTextBlockId = 1,
                ArticleId = 1,
                Text = "lorem impsum"
            };

            builder.Entity<ArticleTextBlocks>().HasData(articleTextBlocks1);

            Language language1 = new Language()
            {
                LanguageId = 1,
                Name = "English"
            };
            Language language2 = new Language()
            {
                LanguageId = 2,
                Name = "Dutch"
            };

            builder.Entity<Language>().HasData(language1);
            builder.Entity<Language>().HasData(language2);
        }
    }
}