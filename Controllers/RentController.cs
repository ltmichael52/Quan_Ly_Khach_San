using Microsoft.AspNetCore.Mvc;
using Team_Project_4.Models;

namespace Team_Project_4.Controllers
{
    public class RentController : Controller
    {
        private readonly HotelDbContext context;
        public RentController(HotelDbContext context_)
        {
            this.context = context_;
        }
        public IActionResult RentList()
        {
            return View(context.Phieuthues.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Phong phong_)
        {
            phong_.Tinhtrang = true;
            var existingRoom = context.Phongs.FirstOrDefault(r => r.Map == phong_.Map);
            if (existingRoom != null)
            {
                ModelState.AddModelError("Map", "Mã phòng đã tồn tại.");
                return View(phong_);
            }
            if (ModelState.IsValid)
            {
                context.Add(phong_);
                context.SaveChanges();
                return RedirectToAction("RoomList");
            }
            return View(phong_);
        }
    }
}
