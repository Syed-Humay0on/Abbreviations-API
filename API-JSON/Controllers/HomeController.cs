using System.Diagnostics;
using System.Net;
using System.Text.Json.Nodes;
using API_JSON.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace API_JSON.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

		public IActionResult Abbreviations()
		{
			return View();
			;
		}
		[HttpPost]
        public async Task<IActionResult> Abbreviations(string Search_Abbr)
		{
            string URL = "https://www.stands4.com/services/v2/abbr.php?uid=12802&tokenid=H78gmEjXBfNk8WmY&term=" + Search_Abbr +"&format=json";
			using (HttpClient client = new HttpClient())
			{
				try
				{
					//WebClient web = new WebClient();
					string JsonData = await client.GetStringAsync(URL);
					JsonNode node = JsonNode.Parse(JsonData);
					ViewBag.json = JsonData;

					

					if (node != null)
					{

					}
					return View();
				}
				catch (HttpRequestException ex)
				{
					// Handle the exception and log it if necessary
					ViewBag.Error = "Error fetching data from the API: " + ex.Message;
					return View("Error"); // Display an error view
				}
			}
		}
        
    }
}
