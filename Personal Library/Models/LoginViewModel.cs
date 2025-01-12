using System.ComponentModel.DataAnnotations;

namespace Personal_Library.Models
{
	public class LoginViewModel
	{
		[Required(ErrorMessage = "帳號為必填欄位")]
		public string Account { get; set; }

		[Required(ErrorMessage = "密碼為必填欄位")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
