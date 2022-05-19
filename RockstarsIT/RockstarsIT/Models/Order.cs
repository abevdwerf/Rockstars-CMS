namespace RockstarsIT.Models
{
    public class Order
    {
        private string orderBy = string.Empty;
        private string orderOn = string.Empty;
        private string searchOn = string.Empty;
        private string filterOn = string.Empty;

        public string OrderBy { get { return orderBy; } set { orderBy = value;} }
        public string OrderOn { get { return orderOn; } set { orderOn = value; } }
        public string SearchOn { get { return searchOn; } set { searchOn = value; } }
        public string FilterOn { get { return filterOn; } set { filterOn = value; } }

/*        public string GetDataByFilter(string orderBy, string orderOn, string searchString, string filterString)
        {
            if (!string.IsNullOrEmpty(orderBy) && !string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {
                    case "title":

                        
                }
            }
        }*/
    }
}
