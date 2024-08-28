using IntegratedSystems.Domain.ETL_Models;
using Microsoft.AspNetCore.Mvc;

namespace IntegratedSystems.Web.Controllers
{
    public class ETLUserPlaylistsController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                HttpClient client = new HttpClient();
                string URL = "https://musicstoreweb20240808175547.azurewebsites.net/api/Admin/GetAllPlaylists";

                HttpResponseMessage response = client.GetAsync(URL).Result;
                var data = response.Content.ReadAsAsync<List<UserPlaylist>>().Result;
                return View("Index", data);
            }
            catch (Exception ex)
            {
                return View("MusicStoreAppNotRunning");
            }
        }
    }
}

