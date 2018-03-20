using System.Collections.Generic;

namespace Core.News.Entities
{
    public class QuoteOfTheDay
    {
        public string Quote { get; set; }
        public string Length { get; set; }
        public string Author { get; set; }
        public List<string> Tags { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string Permalink { get; set; }
        public string Title { get; set; }
        public string Background { get; set; }
        public string id { get; set; }
        public static QuoteOfTheDay Current { get; set; }
    }
}
