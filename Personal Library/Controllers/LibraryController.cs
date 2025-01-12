using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal_Library.Models;
using Personal_Library.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_Library.Controllers
{
    public class LibraryController : Controller
    {
        private readonly PersonalBookCollectionContext _context;
        private readonly ExternalApiService _apiService;

        public LibraryController(PersonalBookCollectionContext context, ExternalApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }

        // 顯示個人書庫
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var collections = await _context.Collections
                .Where(c => c.UserID == userId)
                .Include(c => c.Book)
                .ToListAsync();

            ViewData["Message"] = collections == null || !collections.Any()
                ? "您的書庫目前是空的，新增書籍豐富書庫！"
                : null;

            ViewData["Collections"] = collections ?? new List<Collection>();
            ViewData["UserName"] = HttpContext.Session.GetString("Account");

            return View();
        }

        // 搜尋並新增書籍頁面
        [HttpGet]
        public async Task<IActionResult> AddBook(string keyword = "")
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return View(new BookSearchViewModel());
            }

            // 使用 API 搜尋書籍
            try
            {
                var results = await _apiService.SearchBooksAsync(keyword);
                var model = new BookSearchViewModel
                {
                    Keyword = keyword,
                    SearchResults = results.Select(r => new BookSearchViewModel.BookSearchResult
                    {
                        ProductId = r.ProductId,
                        Title = r.Title,
                        Publisher = r.Publisher,
                        Author = r.Author,
                        Image = r.Image
                    }).ToList()
                };
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"無法搜尋書籍：{ex.Message}";
                return View(new BookSearchViewModel());
            }
        }

        // 新增書籍到收藏
        [HttpPost]
        public async Task<IActionResult> AddBook(string isbn, string note)
        {
            int? userId = HttpContext.Session.GetInt32("UserID");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            try
            {
                // 確保書籍存在於資料庫
                var book = await CheckAndAddBookFromApi(isbn);

                // 檢查是否已收藏
                var existingCollection = await _context.Collections
                    .FirstOrDefaultAsync(c => c.UserID == userId && c.ISBN == isbn);
                if (existingCollection != null)
                {
                    ViewBag.ErrorMessage = "您已收藏過該書籍！";
                    return View();
                }

                // 新增到收藏
                var collection = new Collection
                {
                    UserID = userId.Value,
                    ISBN = isbn,
                    Note = note
                };

                _context.Collections.Add(collection);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"新增書籍時發生錯誤：{ex.Message}";
                return View();
            }
        }

        // 檢查書籍是否存在，若不存在則從 API 中新增
        private async Task<Book> CheckAndAddBookFromApi(string isbn)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
            if (book != null)
            {
                return book;
            }

            // 從 API 中新增書籍
            var bookDetail = await _apiService.GetBookDetailAsync(isbn);
            if (bookDetail == null)
            {
                throw new Exception("找不到該書籍的詳細資訊");
            }

            book = new Book
            {
                ISBN = bookDetail.ISBN,
                Title = bookDetail.Title,
                PublisherID = GetOrCreatePublisherId(bookDetail.Publisher),
                Language = bookDetail.Language,
                PublicationDate = DateTime.Parse(bookDetail.PublishDate),
                ImageURL = bookDetail.Image,
                Description = string.Join(", ", bookDetail.DetailInfo?.Values ?? Enumerable.Empty<string>())
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        // 獲取或新增 Publisher
        private int GetOrCreatePublisherId(string publisherName)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Name == publisherName);
            if (publisher == null)
            {
                publisher = new Publisher { Name = publisherName };
                _context.Publishers.Add(publisher);
                _context.SaveChanges();
            }
            return publisher.PublisherID;
        }
        [HttpGet]
        public async Task<IActionResult> TestSearchBooks(string keyword)
        {
            try
            {
                // 呼叫 ExternalApiService 的 SearchBooksAsync 方法
                var results = await _apiService.SearchBooksAsync(keyword);

                // 返回測試結果到檢視或 JSON
                return Json(results);
            }
            catch (Exception ex)
            {
                return Json(new { Error = ex.Message });
            }
        }

    }
}
