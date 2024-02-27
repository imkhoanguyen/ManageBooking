using ManageBooking.Domain.Entities;
using ManageBooking.Infrastructure.Data;
using ManageBooking.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
            var villaNumbers = _db.VillaNumbers.Include(u => u.Villa).ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            bool roomNumberExists = _db.VillaNumbers.Any(x => x.Villa_Number == obj.VillaNumber.Villa_Number);
            if (ModelState.IsValid && !roomNumberExists)
            {
                _db.VillaNumbers.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "Add success";
                return RedirectToAction(nameof(Index));
            }

            if(roomNumberExists)
            {
                TempData["error"] = "The villa number already exists.";
            }

            obj.VillaList = _db.Villas.ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                VillaNumber = _db.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaNumberId),
            };
            if (villaNumberVM.VillaNumber is null) return RedirectToAction("Error", "Home");
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                _db.Update(villaNumberVM.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "Update success";
                return RedirectToAction(nameof(Index));
            }

            villaNumberVM.VillaList = _db.Villas.ToList().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });

            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _db.Villas.ToList().Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                VillaNumber = _db.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaNumberId),
            };
            if (villaNumberVM is null) return RedirectToAction("Error", "Home");
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? objFromDb = _db.VillaNumbers.FirstOrDefault(x => x.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
            if (objFromDb is not null)
            {
                _db.VillaNumbers.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = "Delete success";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Delete fail";
            return View();
        }
    }
}
