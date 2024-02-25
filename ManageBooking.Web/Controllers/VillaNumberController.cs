using ManageBooking.Domain.Entities;
using ManageBooking.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManageBooking.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> list = _db.Villas.ToList().Select(x=> new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            ViewBag.VillaList = list;
            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaNumber obj)
        {
            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Add success";
                return RedirectToAction("Index");
            }
            return View();
        }

        //public IActionResult Update(int villaId)
        //{
        //    Villa? obj = _db.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaId);
        //    if (obj is null) return RedirectToAction("Error", "Home");
        //    return View(obj);
        //}

        //[HttpPost]
        //public IActionResult Update(Villa obj)
        //{
        //    if (ModelState.IsValid && obj.Id > 0)
        //    {
        //        _db.Update(obj);
        //        _db.SaveChanges();
        //        TempData["success"] = "Add success";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //public IActionResult Delete(int villaId)
        //{
        //    Villa? obj = _db.VillaNumbers.FirstOrDefault(x => x.Id == villaId);
        //    if (obj is null) return RedirectToAction("Error", "Home");
        //    return View(obj);
        //}

        //[HttpPost]
        //public IActionResult Delete(Villa obj)
        //{
        //    Villa? objFromDb = _db.VillaNumbers.FirstOrDefault(x => x.Id == obj.Id);
        //    if (objFromDb is not null)
        //    {
        //        _db.VillaNumbers.Remove(objFromDb);
        //        _db.SaveChanges();
        //        TempData["success"] = "Add success";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
    }
}
