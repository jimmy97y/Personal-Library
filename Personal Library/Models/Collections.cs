using System;

namespace Personal_Library.Models
{
    public class Collection
    {
        public int UserID { get; set; }
        public string ISBN { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // 導航屬性
        public Users User { get; set; }
        public Book Book { get; set; }
    }
}
