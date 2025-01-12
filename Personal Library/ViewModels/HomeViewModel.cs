using Personal_Library.Models;

namespace Personal_Library.ViewModels
{
    public class HomeViewModel
    {
        public bool IsLoggedIn { get; set; }
        public string UserName { get; set; }
        public List<Book> UserBooks { get; set; } = new List<Book>();
        public string Message { get; set; }
    }
    public class BookDetailViewModel
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> OriginalAuthors { get; set; }
        public IEnumerable<string> Translators { get; set; }
        public string Publisher { get; set; }
        public string PublishDate { get; set; }
        public string Language { get; set; }
        public string Image { get; set; }
        public Dictionary<string, string> DetailInfo { get; set; }
    }
}
