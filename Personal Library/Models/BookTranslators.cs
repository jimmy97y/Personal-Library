namespace Personal_Library.Models
{
    public class BookTranslator
    {
        public string ISBN { get; set; }
        public int TranslatorID { get; set; }

        // 導航屬性
        public Book Book { get; set; }
        public Translator Translator { get; set; }
    }
}
