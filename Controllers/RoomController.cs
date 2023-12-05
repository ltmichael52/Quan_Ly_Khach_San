using Microsoft.AspNetCore.Mvc;
using Team_Project_4.Models;

namespace Team_Project_4.Controllers
{
    public class RoomController : Controller
    {
        private readonly HotelDbContext context;
        public RoomController(HotelDbContext context_)
        {
            this.context = context_;
        }
        public IActionResult RoomList()
        {
            return View(context.Phongs.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Phong phong_)
        {
            phong_.Tinhtrang = true;
            if (ModelState.IsValid)
            {
                context.Add(phong_);
                context.SaveChanges();
                return RedirectToAction("RoomList");
            }
            return View();
        }

        public IActionResult Update(string roomid)
        {

            Phong roomNeedUpdate = context.Phongs.FirstOrDefault(r => r.Map == int.Parse(roomid));
            return View(roomNeedUpdate);
        }

        [HttpPost]
        public IActionResult Update(Phong room_, string roomid)
        {
            if (ModelState.IsValid)
            {
                Phong roomNeedUpdate = context.Phongs.FirstOrDefault(r => r.Map == int.Parse(roomid));
                roomNeedUpdate.Map = room_.Map;
                roomNeedUpdate.Tenphong = room_.Tenphong;
                roomNeedUpdate.Loai = room_.Loai;
                roomNeedUpdate.Dongia = room_.Dongia;
                roomNeedUpdate.Tinhtrang = room_.Tinhtrang;
                roomNeedUpdate.Ghichu = room_.Ghichu;
                context.SaveChanges();

                return RedirectToAction("RoomList");
            }
            return View();
        }


        [HttpPost]
        public IActionResult Delete(int roomid)
        {
            Phong needDelete = context.Phongs.FirstOrDefault(r => r.Map == roomid);
            context.Phongs.Remove(needDelete);
            context.SaveChanges();
            return RedirectToAction("RoomList");
        }
    }
}
