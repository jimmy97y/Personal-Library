using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Personal_Library.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Personal_Library.ViewModels;

namespace Personal_Library.Controllers
{
    public class HomeController : Controller
    {
        private readonly PersonalBookCollectionContext _context;
        private readonly ExternalApiService _apiService;

        public HomeController(PersonalBookCollectionContext context, ExternalApiService apiService)
        {
            _context = context;
            _apiService = apiService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BookDetails(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return NotFound("書籍 ID 不可為空！");
            }

            var bookDetail = await _apiService.GetBookDetailAsync(productId);
            if (bookDetail == null)
            {
                return NotFound("找不到該書籍！");
            }

            // 映射到 ViewModel
            var viewModel = new BookDetailViewModel
            {
                ISBN = bookDetail.ISBN,
                Title = bookDetail.Title,
                Price = bookDetail.Price,
                Authors = bookDetail.Authors,
                OriginalAuthors = bookDetail.OriginalAuthors,
                Translators = bookDetail.Translators,
                Publisher = bookDetail.Publisher,
                PublishDate = bookDetail.PublishDate,
                Language = bookDetail.Language,
                Image = bookDetail.Image,
                DetailInfo = bookDetail.DetailInfo
            };

            return View(viewModel);
        }
        //    // 主頁面，搜尋書籍（僅呼叫 API）
        //    public async Task<IActionResult> Index(string keyword, int page = 1, int total = 0, int stock = 1, string author = "", string publisher = "", string dateAfter = "", int priceMin = 0, int priceMax = 999999)
        //    {
        //        if (string.IsNullOrEmpty(keyword))
        //        {
        //            // 第一次進入時，未輸入任何參數
        //            ViewBag.Message = "請輸入關鍵字以搜尋書籍！";
        //            return View(new List<BookSearchResult>()); // 回傳空的結果列表
        //        }

        //        // 呼叫 API 進行書籍搜尋
        //        var books = await _apiService.SearchBooksAsync(keyword, page, total, stock, author, publisher, dateAfter, priceMin, priceMax);
        //        return View(books); // 回傳搜尋結果
        //    }

    }
}
