using Microsoft.EntityFrameworkCore;
using RockstarsIT.Models;

namespace RockstarsIT.Models
{
    public class DatabaseContext : DbContext
    {
        Spotify spotify = new Spotify();
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
        public DbSet<PodcastEpisode> PodcastEpisodes { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ArticleTextBlockTranslation> ArticleTextBlockTranslations { get; set; }
        public DbSet<ArticleContent> ArticleContents { get; set; }
        public DbSet<VideoContent> VideoContents { get; set; }
        public DbSet<PodcastContent> PodcastContents { get; set; }
        public DbSet<PodcastEpisodeContent> PodcastEpisodeContents { get; set; }

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

            Language language1 = new Language()
            {
                LanguageId = 1,
                Name = "English",
            };

            Language language2 = new Language()
            {
                LanguageId = 2,
                Name = "Nederlands",
            };

            builder.Entity<Language>().HasData(language1);
            builder.Entity<Language>().HasData(language2);

            Tribe java = new Tribe()
            {
                TribeId = 1,
                Name = "Java Tribe",
                Description = "Dit is de Java Tribe"
            };

            Tribe microsoft = new Tribe()
            {
                TribeId = 2,
                Name = "Microsoft Tribe",
                Description = "Dit is de Microsoft Tribe"
            };

            Tribe frond_end_and_Mobile = new Tribe()
            {
                TribeId = 3,
                Name = "Frond-end & Mobile Tribe",
                Description = "Dit is de Microsoft Tribe"
            };

            Tribe automated_Testing = new Tribe()
            {
                TribeId = 4,
                Name = "Automated Testing Tribe",
                Description = "Dit is de Automated Testing Tribe"
            };

            Tribe cloud_and_devops = new Tribe()
            {
                TribeId = 5,
                Name = "Cloud & Devops Tribe",
                Description = "Dit is de Cloud & Devops Tribe"
            };

            builder.Entity<Tribe>().HasData(java);
            builder.Entity<Tribe>().HasData(microsoft);
            builder.Entity<Tribe>().HasData(frond_end_and_Mobile);
            builder.Entity<Tribe>().HasData(automated_Testing);
            builder.Entity<Tribe>().HasData(cloud_and_devops);

            Rockstar rockstar1 = new Rockstar()
            {
                RockstarId = 1,
                Name = "Erik",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 1
            };

            Rockstar rockstar2 = new Rockstar()
            {
                RockstarId = 2,
                Name = "Jan",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 1
            };

            Rockstar rockstar3 = new Rockstar()
            {
                RockstarId = 3,
                Name = "Gerrit",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 2
            };

            Rockstar rockstar4 = new Rockstar()
            {
                RockstarId = 4,
                Name = "Piet",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 2
            };

            Rockstar rockstar5 = new Rockstar()
            {
                RockstarId = 5,
                Name = "Harrie",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 4
            };

            Rockstar rockstar6 = new Rockstar()
            {
                RockstarId = 6,
                Name = "Rick",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 5
            };

            Rockstar rockstar7 = new Rockstar()
            {
                RockstarId = 7,
                Name = "Richard",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 3
            };

            Rockstar rockstar8 = new Rockstar()
            {
                RockstarId = 8,
                Name = "Karin",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 3
            };

            Rockstar rockstar9 = new Rockstar()
            {
                RockstarId = 9,
                Name = "Pauline",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 5
            };

            Rockstar rockstar10 = new Rockstar()
            {
                RockstarId = 10,
                Name = "Ilse",
                Description = "test 123",
                LinkedIn = "linked.com/",
                TribeId = 4
            };

            builder.Entity<Rockstar>().HasData(rockstar1);
            builder.Entity<Rockstar>().HasData(rockstar2);
            builder.Entity<Rockstar>().HasData(rockstar3);
            builder.Entity<Rockstar>().HasData(rockstar4);
            builder.Entity<Rockstar>().HasData(rockstar5);
            builder.Entity<Rockstar>().HasData(rockstar6);
            builder.Entity<Rockstar>().HasData(rockstar7);
            builder.Entity<Rockstar>().HasData(rockstar8);
            builder.Entity<Rockstar>().HasData(rockstar9);
            builder.Entity<Rockstar>().HasData(rockstar10);

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
                RockstarId = 1,
                //Title = "Javascript Basic If Else Statement",
                //Description = "In dit artikel zal een basis if else statement worden getoond."
            };

            Article article2 = new Article()
            {
                ArticleId = 2,
                RockstarId = 1,
                //Title = "HTML div",
                //Description = "Uitleg over het HTML element div"
            };

            Article article3 = new Article()
            {
                ArticleId = 3,
                RockstarId = 1,
                //Title = "c# random",
                //Description = "Uitleg over c# random"
            };

            Article article4 = new Article()
            {
                ArticleId = 4,
                RockstarId = 1,
                //Title = "Python",
                //Description = "Uitleg over Python in zijn geheel"
            };

            Article article5 = new Article()
            {
                ArticleId = 5,
                RockstarId = 1,
                //Title = "Java",
                //Description = "Uitleg over Java in zijn geheel"
            };

            Article article6 = new Article()
            {
                ArticleId = 6,
                RockstarId = 1,
                //Title = "PHP",
                //Description = "Uitleg over PHP in zijn geheel"
            };

            builder.Entity<Article>().HasData(article1);
            builder.Entity<Article>().HasData(article2);
            builder.Entity<Article>().HasData(article3);
            builder.Entity<Article>().HasData(article4);
            builder.Entity<Article>().HasData(article5);
            builder.Entity<Article>().HasData(article6);

            //ArticleContent articleContent1 = new ArticleContent()
            //{
            //    ArticleTranslationId = 1,
            //    Title = "Tekst",
            //    LanguageId = 1,
            //    ArticleId = 1
            //};

            //builder.Entity<ArticleContent>().HasData(articleContent1);


            ArticleTextBlocks articleTextBlocks1 = new ArticleTextBlocks()
            {
                ArticleTextBlockId = 1,
                ArticleId = 1,
                Text = "<p>JavaScript is een veelgebruikte scripttaal om webpagina's interactief te maken en webapplicaties te ontwikkelen.&nbsp;</p><p>Naast HTML en CSS is JavaScript een van de kerntechnologieën van het wereldwijde web.</p><p>Javascript is <strong>NIET </strong>het zelfde als Java.</p><p><br data-cke-filler=" + "true" + "></p><p>Dit is een voorbeeld van een javascript <strong>if</strong> <strong>else statement:</strong></p><pre data-language=" + "JavaScript" + "spellcheck=" + "false" + "><code class=" + "language-javascript" + ">Let artikel = " + "Javascript" + ";<br><br>if(artikel == " + "Javascript" + "){<br>	alert(" + "Artikel is Javascritp!" + ");<br>}<br>else{<br>	alert(" + "Artikel is geen Javascritp!" + ");<br>}</code></pre><p>In dit voorbeeld wordt er gekeken of de variabele “artikel” het woord “Javascript” bevat.&nbsp;</p><p>Als dit het geval is komt er een alert in het scherm met " + "Artikel is Javascript!”. Wanneer dit niet het geval is zal er een alert komen met " + "Artikel is geen Javascript!”.</p>"
            };

            ArticleTextBlocks articleTextBlocks2 = new ArticleTextBlocks()
            {
                ArticleTextBlockId = 2,
                ArticleId = 1,
                Text = "<p>Javascript is a popular coding language to make webpages interactive and to develop webapplications.</p>" + "<p>Javascript besides HTML and CSS is one of the core technologies for the world wide web.</p>" + "<p>Javascript is <strong>NOT</strong> the same as Java.</p><p><br data-cke-filler=" + "true" +"></p><p>This is a example of a Javascript <strong>if</strong> <strong>else statement:</strong></p><pre data-language=" + "JavaScript" + "spellcheck=" + "false" + "><code class=" + "language-javascript" + ">Let article" + "Javascript" + ";<br><br>if(article == " + "Javascript" +"){<br>	alert(" + "article is Javascritp!" + ");<br>}<br>else{<br>	alert(" + "article is not Javascritp!" + ");<br>}</code></pre><p>In this example you check if the variable “article” contains the word “Javascript”.</p><p>If this is the case there will be a alert saying “article is Javascritp!”. When it is not the case the alert will say “articel is not Javascript!”.</p><p><br data-cke-filler=" + "true" + "></p>"
            };

            ArticleTextBlocks articleTextBlocks3 = new ArticleTextBlocks()
            {
                ArticleTextBlockId = 3,
                ArticleId = 1,
                Text = "<p>Deze is er om te verwijderen</p>"
            };

            builder.Entity<ArticleTextBlocks>().HasData(articleTextBlocks1);
            builder.Entity<ArticleTextBlocks>().HasData(articleTextBlocks2);
            builder.Entity<ArticleTextBlocks>().HasData(articleTextBlocks3);

            Podcast podcast1 = new Podcast()
            {
                PodcastId = 1,
                URL = "https://open.spotify.com/episode/7d9QeMg8T5XY5y85gyp0wb",
            };
            
            builder.Entity<Podcast>().HasData(podcast1);
            
            PodcastEpisode podcastEpisode1 = new PodcastEpisode()
            {
                PodcastEpisodeId = 1,
                URL = "https://open.spotify.com/episode/4BDg3QufDDZ0rpos71c6Wl",
                RockstarId = 1,
                TribeId = 1,
                PodcastId = 1
            };

            PodcastEpisode podcastEpisode2 = new PodcastEpisode()
            {
                PodcastEpisodeId = 2,
                URL = "https://open.spotify.com/episode/7pZyi78l6vRJVdNquFiaQG",
                RockstarId = 1,
                TribeId = 1,
                PodcastId = 1
            };

            PodcastEpisode podcastEpisode3 = new PodcastEpisode()
            {
                PodcastEpisodeId = 3,
                URL = "https://open.spotify.com/episode/7d9QeMg8T5XY5y85gyp0wb",
                RockstarId = 1,
                TribeId = 1,
                PodcastId = 1
            };

            PodcastEpisode podcastEpisode4 = new PodcastEpisode()
            {
                PodcastEpisodeId = 4,
                URL = "https://open.spotify.com/episode/2nH8y0ivbsjjuEX2OrObXt",
                RockstarId = 1,
                TribeId = 1,
                PodcastId = 1
            };

            PodcastEpisode podcastEpisode5 = new PodcastEpisode()
            {
                PodcastEpisodeId = 5,
                URL = "https://open.spotify.com/episode/4RrPIkmIMoUB0IcOfykmLF",
                RockstarId = 1,
                TribeId = 1,
                PodcastId = 1
            };

            builder.Entity<PodcastEpisode>().HasData(podcastEpisode1);
            builder.Entity<PodcastEpisode>().HasData(podcastEpisode2);
            builder.Entity<PodcastEpisode>().HasData(podcastEpisode3);
            builder.Entity<PodcastEpisode>().HasData(podcastEpisode4);
            builder.Entity<PodcastEpisode>().HasData(podcastEpisode5);

            PodcastEpisodeContent podcastEpisodeContent1 = new PodcastEpisodeContent()
            {
                PodcastEpisodeContentId = 1,
                Title = spotify.GetTitle(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/4BDg3QufDDZ0rpos71c6Wl")),
                Description = spotify.GetDescription(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/4BDg3QufDDZ0rpos71c6Wl")),
                LanguageId = 1,
                PodcastEpisodeId = 1
            };

            PodcastEpisodeContent podcastEpisodeContent2 = new PodcastEpisodeContent()
            {
                PodcastEpisodeContentId = 2,
                Title = spotify.GetTitle(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/7pZyi78l6vRJVdNquFiaQG")),
                Description = spotify.GetDescription(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/7pZyi78l6vRJVdNquFiaQG")),
                LanguageId = 1,
                PodcastEpisodeId = 1
            };

            PodcastEpisodeContent podcastEpisodeContent3 = new PodcastEpisodeContent()
            {
                PodcastEpisodeContentId = 3,
                Title = spotify.GetTitle(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/7d9QeMg8T5XY5y85gyp0wb")),
                Description = spotify.GetDescription(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/7d9QeMg8T5XY5y85gyp0wb")),
                LanguageId = 1,
                PodcastEpisodeId = 1
            };

            PodcastEpisodeContent podcastEpisodeContent4 = new PodcastEpisodeContent()
            {
                PodcastEpisodeContentId = 4,
                Title = spotify.GetTitle(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/2nH8y0ivbsjjuEX2OrObXt")),
                Description = spotify.GetDescription(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/2nH8y0ivbsjjuEX2OrObXt")),
                LanguageId = 1,
                PodcastEpisodeId = 1
            };

            PodcastEpisodeContent podcastEpisodeContent5 = new PodcastEpisodeContent()
            {
                PodcastEpisodeContentId = 5,
                Title = spotify.GetTitle(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/4RrPIkmIMoUB0IcOfykmLF")),
                Description = spotify.GetDescription(spotify.GetSpotifyLinkId("https://open.spotify.com/episode/4RrPIkmIMoUB0IcOfykmLF")),
                LanguageId = 1,
                PodcastEpisodeId = 1
            };

            builder.Entity<PodcastEpisodeContent>().HasData(podcastEpisodeContent1);
            builder.Entity<PodcastEpisodeContent>().HasData(podcastEpisodeContent2);
            builder.Entity<PodcastEpisodeContent>().HasData(podcastEpisodeContent3);
            builder.Entity<PodcastEpisodeContent>().HasData(podcastEpisodeContent4);
            builder.Entity<PodcastEpisodeContent>().HasData(podcastEpisodeContent5);


            Video video1 = new Video()
            {
                VideoId = 1,
                Link = "https://www.youtube.com/watch?v=eIrMbAQSU34",
                RockstarId = 1,
                TribeId = 1,
                LinkType = LinkType.Youtube,
                ViewCount = 15
            };

            Video video2 = new Video()
            {
                VideoId = 2,
                Link = "https://www.youtube.com/watch?v=2Wp8en1I9oQ",
                RockstarId = 5,
                TribeId = 4,
                LinkType = LinkType.Youtube,
                ViewCount = 15
            };

            Video video3 = new Video()
            {
                VideoId = 3,
                Link = "https://www.youtube.com/watch?v=Y2a16HAsHBE",
                RockstarId = 3,
                TribeId = 2,
                LinkType = LinkType.Youtube,
                ViewCount = 15
            };

            Video video4 = new Video()
            {
                VideoId = 4,
                Link = "https://www.youtube.com/watch?v=JTDK6r1GuUU",
                RockstarId = 4,
                TribeId = 2,
                LinkType = LinkType.Youtube,
                ViewCount = 15
            };

            Video video5 = new Video()
            {
                VideoId = 5,
                Link = "https://www.youtube.com/watch?v=3hLmDS179YE",
                RockstarId = 6,
                TribeId = 5,
                LinkType = LinkType.Youtube,
                ViewCount = 15
            };

            builder.Entity<Video>().HasData(video1);
            builder.Entity<Video>().HasData(video2);
            builder.Entity<Video>().HasData(video3);
            builder.Entity<Video>().HasData(video4);
            builder.Entity<Video>().HasData(video5);

            VideoContent videoContent1 = new VideoContent()
            {
                VideoContentId = 1,
                Title = "Java course",
                Description = "Dit is een video over Java",
                LanguageId = 1,
                VideoId = 1
            };

            VideoContent videoContent2 = new VideoContent()
            {
                VideoContentId = 2,
                Title = "Xunit",
                Description = "Dit is een video over Xunit",
                LanguageId = 1,
                VideoId = 2
            };

            VideoContent videoContent3 = new VideoContent()
            {
                VideoContentId = 3,
                Title = "dot.NET 6",
                Description = "Dit is een video over dot.NET 6",
                LanguageId = 1,
                VideoId = 3
            };

            VideoContent videoContent4 = new VideoContent()
            {
                VideoContentId = 4,
                Title = "MSSQL",
                Description = "Dit is een video over MSSQL",
                LanguageId = 1,
                VideoId = 4
            };

            VideoContent videoContent5 = new VideoContent()
            {
                VideoContentId = 5,
                Title = "AWS Cloud",
                Description = "Dit is een video over de cloud",
                LanguageId = 1,
                VideoId = 5
            };

            builder.Entity<VideoContent>().HasData(videoContent1);
            builder.Entity<VideoContent>().HasData(videoContent2);
            builder.Entity<VideoContent>().HasData(videoContent3);
            builder.Entity<VideoContent>().HasData(videoContent4);
            builder.Entity<VideoContent>().HasData(videoContent5);
        }

        public DbSet<RockstarsIT.Models.Podcast> Podcast { get; set; }
    }
}