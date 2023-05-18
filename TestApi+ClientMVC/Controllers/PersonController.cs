using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestApi_Client.Model;

namespace TestApi_ClientMVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public PersonController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;

            var baseUrl = _configuration.GetConnectionString("ApiBaseUrl");
            _httpClient.BaseAddress = new Uri(baseUrl);
        }
        public async Task<IActionResult> List()
        {
            var response = await _httpClient.GetAsync("api/person");
            response.EnsureSuccessStatusCode();

            var apartments = await response.Content.ReadFromJsonAsync<Person[]>();

            return View(apartments);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            var response = await _httpClient.PostAsJsonAsync("api/person", person);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");

                return RedirectToAction(nameof(List));
            }

            return RedirectToAction(nameof(List));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/person/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(List));
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error: {errorContent}");

                return RedirectToAction(nameof(List));
            }
        }
    }
}
