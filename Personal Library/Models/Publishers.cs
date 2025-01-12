using System.Collections.Generic;

namespace Personal_Library.Models
{
    public class Publisher
    {
        public int PublisherID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        // 導航屬性
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
