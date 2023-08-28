using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Azure.Core;
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

        public async Task<ResponseData<Product>> CreateProductAsync(Product product, IFormFile? formFile)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}products/");
            var response = await _httpClient.PostAsJsonAsync(urlString.ToString(), product, _serializerOptions);

            if (response.IsSuccessStatusCode)
            {
                var productResponse = await response.Content.ReadFromJsonAsync<ResponseData<Product>>(_serializerOptions);
                if (formFile != null)
                {

                    Debug.WriteLine($"FTYFJGF {productResponse.Data.Id}");
                    await SaveImageAsync(productResponse.Data.Id, formFile);
                }
                
                return new ResponseData<Product>
                {
                    Data = productResponse.Data,
                    Success = true
                };
            }
            else
            {
                return new ResponseData<Product>
                {
                    Success = false,
                    ErrorMessage = "Ошибка при создании продукта"
                };
            }

        }

        public async Task DeleteProductAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}products/{id}");
            await _httpClient.DeleteAsync(urlString.ToString());
        }

        public async Task<ResponseData<Product>> GetProductByIdAsync(int id)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}products/{id}");
            var response = await _httpClient.GetFromJsonAsync<ResponseData<Product>>(urlString.ToString(), _serializerOptions);
            return response;
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


        public async Task UpdateProductAsync(int id, Product product, IFormFile? formFile)
        {
            var urlString = new StringBuilder($"{_httpClient.BaseAddress!.AbsoluteUri}products/{id}");
            await _httpClient.PutAsJsonAsync(urlString.ToString(), product, _serializerOptions);

            if (formFile != null)
            {
                await SaveImageAsync(id, formFile);
            }
        }

        private async Task SaveImageAsync(int id, IFormFile image)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress.AbsoluteUri}Products/{id}")
            };

            var content = new MultipartFormDataContent();
            var streamContent = new StreamContent(image.OpenReadStream());
            content.Add(streamContent, "formFile", image.FileName);
            request.Content = content;
            await _httpClient.SendAsync(request);
        }
    }
}

