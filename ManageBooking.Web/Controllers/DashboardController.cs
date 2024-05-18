using Microsoft.AspNetCore.Mvc;

namespace ManageBooking.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
