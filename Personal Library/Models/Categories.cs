using System.Collections.Generic;

namespace Personal_Library.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int? ParentID { get; set; }

        // 導航屬性
        public Category ParentCategory { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
