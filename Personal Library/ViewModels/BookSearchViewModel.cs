using System.Collections.Generic;

namespace Personal_Library.ViewModels
{
    public class BookSearchViewModel
    {
        public string Keyword { get; set; }
        public List<BookSearchResult> SearchResults { get; set; }

        public class BookSearchResult
        {
            public string ProductId { get; set; }
            public string Title { get; set; }
            public string Publisher { get; set; }
            public IEnumerable<string> Author { get; set; }
            public string Image { get; set; }
        }
    }
}
