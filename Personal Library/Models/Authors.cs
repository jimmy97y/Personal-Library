using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Personal_Library.Models
{
    public class Author
    {
        [Key] // 設定主鍵
        public int AuthorID { get; set; } // 主鍵

        [Required] // 必填欄位
        [MaxLength(200)] // 限制作者名稱的長度
        public string Name { get; set; }

        [MaxLength(300)] // 限制 URL 的長度
        public string URL { get; set; }

        // 導航屬性：與書籍的多對多關係
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    }
}
