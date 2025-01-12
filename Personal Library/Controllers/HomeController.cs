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
                return NotFound("���y ID ���i���šI");
            }

            var bookDetail = await _apiService.GetBookDetailAsync(productId);
            if (bookDetail == null)
            {
                return NotFound("�䤣��Ӯ��y�I");
            }

            // �M�g�� ViewModel
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
        //    // �D�����A�j�M���y�]�ȩI�s API�^
        //    public async Task<IActionResult> Index(string keyword, int page = 1, int total = 0, int stock = 1, string author = "", string publisher = "", string dateAfter = "", int priceMin = 0, int priceMax = 999999)
        //    {
        //        if (string.IsNullOrEmpty(keyword))
        //        {
        //            // �Ĥ@���i�J�ɡA����J����Ѽ�
        //            ViewBag.Message = "�п�J����r�H�j�M���y�I";
        //            return View(new List<BookSearchResult>()); // �^�ǪŪ����G�C��
        //        }

        //        // �I�s API �i����y�j�M
        //        var books = await _apiService.SearchBooksAsync(keyword, page, total, stock, author, publisher, dateAfter, priceMin, priceMax);
        //        return View(books); // �^�Ƿj�M���G
        //    }

    }
}
