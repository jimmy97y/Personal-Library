namespace Personal_Library.Models
{
    public class BookCategory
    {
        public string ISBN { get; set; }
        public int CategoryID { get; set; }

        // 導航屬性
        public Book Book { get; set; }
        public Category Category { get; set; }
    }
}
