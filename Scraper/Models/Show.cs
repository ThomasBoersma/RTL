using System.Collections.Generic;

namespace Scraper.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Person> Cast { get; set; }
    }
}
