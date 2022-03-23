namespace RockstarsIT.Models
{
    public class Article
    {

        private int id;
        private string title;
        private string description;
        private string author;
        private string image;
        private string text;

        public int Id { 
            get { return id; } 
            set { id = value; } 
        }
        public string Title {
            get { return title; }
            set { title = value; }
        }
        public string Description {
            get { return description; }
            set { description = value;}
        }
        public string Author {
            get { return author; }
            set { author = value; }
        }
        public string Image {
            get { return image; }
            set { image = value; }
            }
        public int Text {
            get { return Text; }
            set { Text = value; }
        }  
        
    }
}
