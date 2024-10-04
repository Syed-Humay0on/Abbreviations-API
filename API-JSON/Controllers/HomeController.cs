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
        public async Task<IActionResult> Abbreviations(Abbr_Model Search_Abbr)
		{
			if (Search_Abbr == null || string.IsNullOrEmpty(Search_Abbr.Term))
			{
				// Return an error view if no search term is provided
				ViewBag.Error = "Invalid search term.";
				return View("Error");
			}

			string URL = "https://www.stands4.com/services/v2/abbr.php?uid=12802&tokenid=H78gmEjXBfNk8WmY&term=" + Search_Abbr.Term +"&format=json";
			using (HttpClient client = new HttpClient())
			{
				try
				{
					//WebClient web = new WebClient();
					string JsonData = await client.GetStringAsync(URL);
					JsonNode node = JsonNode.Parse(JsonData);
					//ViewBag.json = JsonData;

					List<Abbr_Model> listed = new List<Abbr_Model>();
					JsonArray jsonArray = (JsonArray)node["result"];
					foreach(var item in jsonArray)
					{
						//Search_Abbr.Term = item["term"].ToString();
						//Search_Abbr.Definition = item["definition"].ToString();
						//Search_Abbr.Category_Name = item["categoryname"].ToString();
						//Search_Abbr.Parent_Category = item["parentcategory"].ToString();
						//Search_Abbr.Id = item["id"].ToString();
						//Search_Abbr.Category = item["category"].ToString();

						//listed.Add(Search_Abbr);

						var abbrModel = new Abbr_Model
						{
							Term = item["term"].ToString(),
							Definition = item["definition"].ToString(),
							Category_Name = item["categoryname"].ToString(),
							Parent_Category = item["parentcategory"].ToString(),
							Id = item["id"].ToString(),
							Category = item["category"].ToString()
						};

						listed.Add(abbrModel);
					}
					
					return View("Abbreviations", listed);
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
