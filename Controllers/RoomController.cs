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
        public IActionResult RoomList(string sortOrder, string sortColumn)
        {
            ViewData["MaSortParam"] = sortColumn == "Map" ? (sortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["TenSortParam"] = sortColumn == "Tenphong" ? (sortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["TinhtrangSortParam"] = sortColumn == "Tinhtrang" ? (sortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["DongiaSortParam"] = sortColumn == "Dongia" ? (sortOrder == "asc" ? "desc" : "asc") : "asc";
            var rooms = context.Phongs.AsQueryable();

            switch (sortColumn)
            {
                case "Map":
                    rooms = sortOrder == "desc" ? rooms.OrderByDescending(r => r.Map) : rooms.OrderBy(r => r.Map);
                    break;
                case "Tenphong":
                    rooms = sortOrder == "desc" ? rooms.OrderByDescending(r => r.Tenphong) : rooms.OrderBy(r => r.Tenphong);
                    break;
                case "Tinhtrang":
                    rooms = sortOrder == "desc" ? rooms.OrderByDescending(r => r.Tinhtrang) : rooms.OrderBy(r => r.Tinhtrang);
                    break;
                case "Dongia":
                    rooms = sortOrder == "desc" ? rooms.OrderByDescending(r => r.Dongia) : rooms.OrderBy(r => r.Dongia);
                    break;
                default:
                    rooms = rooms.OrderBy(r => r.Map);
                    break;
            }
            return View(rooms.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Phong phong_)
        {
            phong_.Tinhtrang = true;
            var existingRoom = context.Phongs.FirstOrDefault(r=>r.Map==phong_.Map);
            if (existingRoom != null)
            {
                ModelState.AddModelError("Map", "Mã phòng đã tồn tại.");
                return View(phong_);
            }
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("Map", "Vui lòng nhập mã phòng.");
                context.Add(phong_);
                context.SaveChanges();
                return RedirectToAction("RoomList");
            }
            return View(phong_);
        }


        public IActionResult Update(int roomid)
        {

            Phong roomNeedUpdate = context.Phongs.FirstOrDefault(r => r.Map == roomid);
            return View(roomNeedUpdate);
        }

        [HttpPost]
        public IActionResult Update(Phong room_, int roomid)
        {
            var existingRoom = context.Phongs.FirstOrDefault(r => r.Map== roomid);
            if (existingRoom != null)
            {
                ModelState.AddModelError("MaP", "Mã phòng đã tồn tại.");
                return View(room_);
            }
            if (ModelState.IsValid)
            {
                Phong roomNeedUpdate = context.Phongs.FirstOrDefault(r => r.Map == roomid);
                roomNeedUpdate.Map = room_.Map;
                roomNeedUpdate.Tenphong = room_.Tenphong;
                roomNeedUpdate.Loai = room_.Loai;
                roomNeedUpdate.Dongia = room_.Dongia;
                roomNeedUpdate.Tinhtrang = room_.Tinhtrang;
                roomNeedUpdate.Ghichu = room_.Ghichu;
                context.SaveChanges();

                return RedirectToAction("RoomList");
            }
            return View(room_);
        }


        [HttpPost]
        public IActionResult Delete(int roomid)
        {
            Phong needDelete = context.Phongs.FirstOrDefault(r => r.Map == roomid);
            context.Phongs.Remove(needDelete);
            context.SaveChanges();
            return RedirectToAction("RoomList");
        }

        
        public IActionResult Search(string searchString)
        {
            var rooms = context.Phongs.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                rooms = rooms.Where(r => r.Tenphong != null && r.Tenphong.ToLower().Contains(searchString.ToLower())).ToList();
            }
            TempData["searchWord"] = searchString;
            return View("RoomList", rooms);
        }

    }
}
