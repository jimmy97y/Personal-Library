using System.Collections.Generic;

namespace Personal_Library.Models
{
    public class Translator
    {
        public int TranslatorID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        // 導航屬性
        public ICollection<BookTranslator> BookTranslators { get; set; } = new List<BookTranslator>();
    }
}
