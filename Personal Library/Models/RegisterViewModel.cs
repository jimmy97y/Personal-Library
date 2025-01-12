using System.ComponentModel.DataAnnotations;

namespace Personal_Library.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "帳號")]
        public string Account { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "密碼和確認密碼不一致")]
        [DataType(DataType.Password)]
        [Display(Name = "確認密碼")]
        public string ConfirmPassword { get; set; }

        [EmailAddress]
        [Display(Name = "電子郵件")]
        public string Email { get; set; }

        [MaxLength(100)]
        [Display(Name = "使用者名稱")]
        public string UserName { get; set; }

        [Display(Name = "頭像")]
        public IFormFile Avatar { get; set; }

        [Display(Name = "性別")]
        public string Gender { get; set; }
    }
}
