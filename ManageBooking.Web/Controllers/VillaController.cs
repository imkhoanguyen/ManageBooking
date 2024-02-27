using ManageBooking.Domain.Entities;
using ManageBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace ManageBooking.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            if (ModelState.IsValid)
            {
                _db.Villas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Add success";
                return RedirectToAction(nameof(Index));
            }
            return View();

        }

        public IActionResult Update(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);
            if (obj is null) return RedirectToAction("Error", "Home");
            return View(obj);
        }

        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid && obj.Id > 0)
            {
                _db.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Edit success";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public IActionResult Delete(int villaId)
        {
            Villa? obj = _db.Villas.FirstOrDefault(x => x.Id == villaId);
            if (obj is null) return RedirectToAction("Error", "Home");
            return View(obj);
        }

        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? objFromDb = _db.Villas.FirstOrDefault(x => x.Id == obj.Id);
            if(objFromDb is not null)
            {
                _db.Villas.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = "Delete success";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
