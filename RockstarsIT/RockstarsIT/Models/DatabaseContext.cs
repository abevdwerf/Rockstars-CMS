using Microsoft.EntityFrameworkCore;
using RockstarsIT.Models;

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
        
        public DbSet<PodcastEpisode> PodcastEpisodes { get; set; }
        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Video> Videos { get; set; }

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
                Title = "Javascript Basic If Else Statement",
                Description = "In dit artikel zal een basis if else statement worden getoond."
            };

            Article article2 = new Article()
            {
                ArticleId = 2,
                RockstarId = 1,
                Title = "HTML div",
                Description = "Uitleg over het HTML element div"
            };

            Article article3 = new Article()
            {
                ArticleId = 3,
                RockstarId = 1,
                Title = "c# random",
                Description = "Uitleg over c# random"
            };

            Article article4 = new Article()
            {
                ArticleId = 4,
                RockstarId = 1,
                Title = "Python",
                Description = "Uitleg over Python in zijn geheel"
            };

            Article article5 = new Article()
            {
                ArticleId = 5,
                RockstarId = 1,
                Title = "Java",
                Description = "Uitleg over Java in zijn geheel"
            };

            Article article6 = new Article()
            {
                ArticleId = 6,
                RockstarId = 1,
                Title = "PHP",
                Description = "Uitleg over PHP in zijn geheel"
            };

            builder.Entity<Article>().HasData(article1);
            builder.Entity<Article>().HasData(article2);
            builder.Entity<Article>().HasData(article3);
            builder.Entity<Article>().HasData(article4);
            builder.Entity<Article>().HasData(article5);
            builder.Entity<Article>().HasData(article6);

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

            PodcastEpisode podcastEpisode3 = new PodcastEpisode()
            {
                PodcastEpisodeId = 3,
                Title = "#3 Vincent Hendriks; over de kracht van de waarom vraag aan de klant, zijn voorspellingen voor het IT landschap & tips voor junior engineers.",
                Description = "Niet direct met gestrekt been erin, eerst luisteren, vragen stellen en proberen te begrijpen waar de gewenste IT-oplossing van de klant vandaan komt. Dit is wat er nodig is om te zorgen voor een goede samenwerking en de beste oplossing volgens Vincent. Dat dit een echte senior developer is op vele vlakken werd al snel duidelijk. Vincent deelt met ons zijn visie op het vakgebied, vertelt wat er volgens hem anders kan en strooit met tips voor groeien op kennis en impact maken bij de klant.",
                URL = "https://open.spotify.com/episode/7d9QeMg8T5XY5y85gyp0wb",
                RockstarId = 1,
                TribeId = 1
            };

            PodcastEpisode podcastEpisode5 = new PodcastEpisode()
            {
                PodcastEpisodeId = 5,
                Title = "#5 Ivar Lugtenburg, Full Stack developer en Tech Lead deelt op scherpe wijze zijn visie en vertelt ons waarom kritisch zijn op jezelf juist goed is.",
                Description = "Met de Nintendo als trigger en zijn oudere broer als voorbeeld dook Ivar de wereld van IT in. Hij koos de weg van Java, vervult nu een lead rol en wil hier elke dag nog meer over leren. Hoe en van wie leert hij dan en hoe zorgt hij voor de beste oplossing bij de klant als hij van mening verschilt over wat de juiste aanpak is? In deze slot-aflevering geeft Ivar antwoord en deelt hij waarom hij fan is van Clean Code en Venkat Subramaniam. Ivar vertelt over zijn ervaring in het IT landschap en deelt zijn visie met ons. Niet ergens binnenkomen en meteen je kennis uitstrooien maar eerst in gesprek gaan, goed luisteren en vanuit daar een ander technisch perspectief kunnen bieden. Luister de hele aflevering voor nog meer inspiratie en leuk als je een reactie post!",                
                URL = "https://open.spotify.com/episode/4RrPIkmIMoUB0IcOfykmLF",
                RockstarId = 1,
                TribeId = 1
            };

            PodcastEpisode podcastEpisode4 = new PodcastEpisode()
            {
                PodcastEpisodeId = 4,
                Title = "#4 Duco Fronik; een senior front-end developer met oa. Xamarin in zijn technische rugzak over de snelheid van de front-end wereld en hoe hij bijblijft en zich ontwikkelt op deze sneltrein.",
                Description = "Duco is stiekem een echte lefgozer als het gaat om het inzetten van de allernieuwste technieken. Hij creëert zijn eigen speelveld en hij leert door het gewoon te gaan doen! Zijn gouden tip? Zoek iets wat in relatie staat tot de echte wereld en kijk hoe kan je het daar toepassen. Dat maakt het zoveel toffer! In deze talk doet Duco zijn boekje open over front-end gedrochten, dode frameworks, hedendaagse technologie, hobby projecten, wat er de prullenbak in kan, het maken van maatschappelijke impact trends, voorspellingen en nog veel meer. Dit is weer een technische talk op hoog niveau die je volledig meeneemt in de snelle en dynamische wereld van front-end!",                
                URL = "https://open.spotify.com/episode/2nH8y0ivbsjjuEX2OrObXt",
                RockstarId = 1,
                TribeId = 1
            };

            PodcastEpisode podcastEpisode2 = new PodcastEpisode()
            {
                PodcastEpisodeId = 2,
                Title = "#2 Nadine Wolff; Deze embedded .NET developer met liefde voor hardware én japan neemt je mee in haar slimme hacks tot het seniorschap!",
                Description = "Deze vrouw is dol op de directe feedback die hardware haar geeft en gaat het liefst door alle lagen heen. Ze heeft in haar loopbaan heel goed gekeken naar experts om haar heen, heeft haar eigen saus er over heen gegoten en pakt nu zelf de lead en begeleidt juniors en studenten. In deze podcast deelt ze haar visie, haar geheim voor succes in detachering en tips hoe je bij blijft in je vak!",                
                URL = "https://open.spotify.com/episode/7pZyi78l6vRJVdNquFiaQG",
                RockstarId = 1,
                TribeId = 1
            };

            PodcastEpisode podcastEpisode1 = new PodcastEpisode()
            {
                PodcastEpisodeId = 1,
                Title = "#1 Guido Schippers",
                Description = "Deze .NET en Xamarin fan vertelt over zijn passie voor technische filosofie, VR en de droom om ooit die gast te zijn die de Matrix maakt.",                
                URL = "https://open.spotify.com/episode/4BDg3QufDDZ0rpos71c6Wl",
                RockstarId = 1,
                TribeId = 1
            };

            builder.Entity<PodcastEpisode>().HasData(podcastEpisode1);
            builder.Entity<PodcastEpisode>().HasData(podcastEpisode2);
            builder.Entity<PodcastEpisode>().HasData(podcastEpisode3);
            builder.Entity<PodcastEpisode>().HasData(podcastEpisode4);
            builder.Entity<PodcastEpisode>().HasData(podcastEpisode5);

            Video video1 = new Video()
            {
                VideoId = 1,
                Title = "Java course",
                Description = "Dit is een video over Java",
                Link = "https://www.youtube.com/watch?v=eIrMbAQSU34",
                RockstarId = 1,
                TribeId = 1
            };

            Video video2 = new Video()
            {
                VideoId = 2,
                Title = "Xunit",
                Description = "Dit is een video over Xunit",
                Link = "https://www.youtube.com/watch?v=2Wp8en1I9oQ",
                RockstarId = 5,
                TribeId = 4
            };

            Video video3 = new Video()
            {
                VideoId = 3,
                Title = "dot.NET 6",
                Description = "Dit is een video over dot.NET 6",
                Link = "https://www.youtube.com/watch?v=Y2a16HAsHBE",
                RockstarId = 3,
                TribeId = 2
            };

            Video video4 = new Video()
            {
                VideoId = 4,
                Title = "MSSQL",
                Description = "Dit is een video over MSSQL",
                Link = "https://www.youtube.com/watch?v=JTDK6r1GuUU",
                RockstarId = 4,
                TribeId = 2
            };

            Video video5 = new Video()
            {
                VideoId = 5,
                Title = "AWS Cloud",
                Description = "Dit is een video over de cloud",
                Link = "https://www.youtube.com/watch?v=3hLmDS179YE",
                RockstarId = 6,
                TribeId = 5
            };

            builder.Entity<Video>().HasData(video1);
            builder.Entity<Video>().HasData(video2);
            builder.Entity<Video>().HasData(video3);
            builder.Entity<Video>().HasData(video4);
            builder.Entity<Video>().HasData(video5);

        }

        public DbSet<RockstarsIT.Models.Podcast> Podcast { get; set; }
    }
}