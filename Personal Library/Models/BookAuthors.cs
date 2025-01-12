namespace Personal_Library.Models
{
    public class BookAuthor
    {
        public string ISBN { get; set; }
        public int AuthorID { get; set; }

        // 導航屬性
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
