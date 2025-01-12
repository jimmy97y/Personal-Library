using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Personal_Library.Models
{
    public class Book
    {
        [Key] // 使用 [Key] 標註主鍵
        [MaxLength(13)] // 限制 ISBN 長度
        public string ISBN { get; set; } // 假設 ISBN 是主鍵

        [Required]
        [MaxLength(200)] // 限制書名長度
        public string Title { get; set; }

        [MaxLength(50)] // 限制語言長度
        public string Language { get; set; }

        public DateTime? PublicationDate { get; set; } // 出版日期

        [MaxLength(300)] // 限制圖片 URL 長度
        public string ImageURL { get; set; }

        public string Description { get; set; } // 書籍描述

        public int Count { get; set; } = 0; // 收藏數量，默認為 0

        [Required]
        public int PublisherID { get; set; } // 外鍵：出版社 ID

        // 導航屬性
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        public virtual ICollection<BookTranslator> BookTranslators { get; set; } = new List<BookTranslator>();
        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    }
}
