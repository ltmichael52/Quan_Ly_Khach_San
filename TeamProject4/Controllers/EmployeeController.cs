using Microsoft.AspNetCore.Mvc;
using Team_Project_4.Models;

namespace Team_Project_4.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HotelDbContext context;
        public EmployeeController(HotelDbContext context_)
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
            phong_.Tinhtrang = 1;
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
