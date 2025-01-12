using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Personal_Library.Models
{
    public class Users
    {
        [Key] // 確保設置主鍵
        public int UserID { get; set; } // 與資料庫中主鍵欄位名稱一致

        [Required]
        [MaxLength(50)] // 設定資料長度限制（根據資料庫）
        public string Account { get; set; }

        [Required]
        [MaxLength(50)]
        public string Password { get; set; }

        [EmailAddress]
        [MaxLength(100)] // 設定 Email 長度限制
        public string Email { get; set; }

        [MaxLength(100)] // 設定使用者名稱長度限制
        public string UserName { get; set; }

        [MaxLength(200)] // 設定圖片連結長度限制
        public string Avatar { get; set; }

        [MaxLength(10)] // 設定性別字串長度限制（如：Male, Female）
        public string Gender { get; set; }

        // 導航屬性
        public virtual ICollection<Collection> Collections { get; set; } = new List<Collection>();
    }
}
