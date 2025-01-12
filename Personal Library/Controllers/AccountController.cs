using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal_Library.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly PersonalBookCollectionContext _context;
    private readonly IWebHostEnvironment _env;

    public AccountController(PersonalBookCollectionContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    // 註冊頁面
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    // 註冊
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // 檢查帳號是否已存在
            var existingUser = _context.Users.FirstOrDefault(u => u.Account == model.Account);
            if (existingUser != null)
            {
                ModelState.AddModelError("Account", "此帳號已被註冊");
                return View(model);
            }

            // 處理照片上傳
            string avatarPath = null;
            if (model.Avatar != null && model.Avatar.Length > 0)
            {
                // 定義照片儲存目錄
                var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                // 生成唯一的檔案名稱
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(model.Avatar.FileName);
                avatarPath = Path.Combine("/uploads", uniqueFileName);

                // 儲存檔案到服務器
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Avatar.CopyToAsync(fileStream);
                }
            }

            // 新增使用者
            var newUser = new Users
            {
                Account = model.Account,
                Email = model.Email,
                Password = model.Password, // 注意：應加密密碼
                UserName = model.UserName,
                Avatar = avatarPath, // 儲存檔案路徑
                Gender = model.Gender
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home"); // 註冊完成後返回首頁
        }
        return View(model);
    }

    // 登入頁面
    [HttpGet]
    public IActionResult Login() => View();

    // 登入
    [HttpPost]
    public IActionResult Login(string account, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Account == account && u.Password == password);
        if (user == null)
        {
            ViewBag.ErrorMessage = "帳號或密碼錯誤";
            return View();
        }

        HttpContext.Session.SetInt32("UserID", user.UserID);
        HttpContext.Session.SetString("Account", user.Account);
        return RedirectToAction("Index", "Library");
    }

    // 登出
    [HttpGet]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Profile()
    {
        int? userId = HttpContext.Session.GetInt32("UserID");
        if (userId == null)
        {
            return RedirectToAction("Login");
        }

        var user = _context.Users.FirstOrDefault(u => u.UserID == userId);
        if (user == null)
        {
            return RedirectToAction("Login");
        }

        return View(user);
    }

}
