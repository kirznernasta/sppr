using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using WEB_153502_KIRZNER.Domain.Entities;
using WEB_153502_KIRZNER.Domain.Models;
using WEB_153502_KIRZNER.Services.CategoryService;

namespace WEB_153502_KIRZNER.Services.ProductService
{
	public class ApiProductService : IProductService
	{
        private HttpClient _httpClient;
        private int _pageSize;
        private JsonSerializerOptions _serializerOptions;
        private ILogger _logger;

		public ApiProductService(HttpClient httpClient, IConfiguration configuration, ILogger<ApiProductService> logger)
		{
            _httpClient = httpClient;
            _pageSize = configuration.GetValue<int>("ItemsPerPage");
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            _logger = logger;
        }

        public Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseData<ProductListModel<Product>>> GetProductListAsync(string? category, int pageNo = 1)
        {
            
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}products/");
            
            if (category != null)
            {
                urlString.Append($"{category}/");
            };
            if (pageNo > 1)
            {
                urlString.Append($"page{pageNo}/");
            };
            if (!_pageSize.Equals(3))
            {
                urlString.Append($"size{_pageSize}");
            }
            var response = await _httpClient.GetAsync(new Uri(urlString.ToString()));

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return await response.Content.ReadFromJsonAsync<ResponseData<ProductListModel<Product>>>(_serializerOptions);

                }
                catch (JsonException ex)
                {
                    _logger.LogError($"-----> Ошибка: {ex.Message}");
                    return new ResponseData<ProductListModel<Product>>
                    {
                        Success = false,
                        ErrorMessage = $"Ошибка: {ex.Message}"
                    };
                }
            }
            _logger.LogError($"-----> Данные не получены от сервера. Error:{response.StatusCode}");
            return new ResponseData<ProductListModel<Product>>
            {
                Success = false,
                ErrorMessage = $"Данные не получены от сервера. Error: {response.StatusCode}"
            };
        }


        public Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}

