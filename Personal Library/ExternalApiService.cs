using Personal_Library.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Text.Json;
using System.Threading.Tasks;

public class ExternalApiService
{
    private readonly HttpClient _httpClient;

    public ExternalApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // 搜尋書籍
    public async Task<IEnumerable<BookSearchResult>> SearchBooksAsync(
        string keyword,
        int page = 1,
        int total = 0,
        int stock = 1,
        string author = "",
        string publisher = "",
        string dateAfter = "",
        int priceMin = 0,
        int priceMax = 999999)
    {
        var url = $"http://127.0.0.1:5050/api/books/search?keyword={keyword}&page={page}&total={total}&stock={stock}&author={author}&publisher={publisher}&date_after={dateAfter}&price_min={priceMin}&price_max={priceMax}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        var result = JsonSerializer.Deserialize<SearchBooksResponse>(responseBody);
        return result.Books;
    }

    // 獲取書籍詳細資訊
    public async Task<BookDetail> GetBookDetailAsync(string productId)
    {
        var url = $"http://127.0.0.1:5050/api/book/{productId}";

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<BookDetail>(responseBody);
    }

    // 搜尋結果的模型
    public class SearchBooksResponse
    {
        public IEnumerable<BookSearchResult> Books { get; set; }
    }

    public class BookSearchResult
    {
        public string ProductId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public IEnumerable<string> Author { get; set; }
        public string Image { get; set; }
    }

    // 書籍詳細資訊的模型
    public class BookDetail
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public IEnumerable<string> OriginalAuthors { get; set; } // 新增此屬性
        public IEnumerable<string> Translators { get; set; }
        public string Publisher { get; set; }
        public string PublishDate { get; set; }
        public string Language { get; set; }
        public string Image { get; set; }
        public Dictionary<string, string> DetailInfo { get; set; }
    }
}