using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Team_Project_4.InterfacesRepositories;
using Team_Project_4.Models;
using Team_Project_4.Repositories;
using Team_Project_4.ViewModels;

namespace Team_Project_4.Controllers
{
    public class LoaiPhongController : Controller
    {
        private readonly ILoaiPhongRepository _lprepo;
        public LoaiPhongController(ILoaiPhongRepository repo)
        {
            _lprepo = repo;
        }
        public async Task<IActionResult> RoomType(string searchString, string SortOrder, string sortColumn, int pageNumber, string currentFilter)
        {
            ViewData["sortColumn"] = sortColumn;
            ViewData["sortOrder"] = SortOrder;
            ViewData["MaSortParam"] = sortColumn == "Maloaiphong" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["TenSortParam"] = sortColumn == "Tenloai" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            ViewData["DongiaSortParam"] = sortColumn == "Dongia" ? (SortOrder == "asc" ? "desc" : "asc") : "asc";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;
            var loaiphongs = _lprepo.GetAllAsync();
            if (!string.IsNullOrEmpty(searchString))
            {
                loaiphongs = loaiphongs.Where(r => r.Tenloai != null && r.Tenloai.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortColumn)
            {
                case "Maloaiphong":
                    loaiphongs = SortOrder == "desc" ? loaiphongs.OrderByDescending(r => r.Maloaiphong) : loaiphongs.OrderBy(r => r.Maloaiphong);
                    break;
                case "Tenloai":
                    loaiphongs = SortOrder == "desc" ? loaiphongs.OrderByDescending(r => r.Tenloai) : loaiphongs.OrderBy(r => r.Tenloai);
                    break;
                case "Dongia":
                    loaiphongs = SortOrder == "desc" ? loaiphongs.OrderByDescending(r => r.Dongia) : loaiphongs.OrderBy(r => r.Dongia);
                    break;
                default:
                    loaiphongs = loaiphongs.OrderBy(r => r.Maloaiphong);
                    break;
            }

            if (pageNumber < 1)
            {
                pageNumber = 1;
            }
            int pageSize = 7;

            return View(await PaginatedList<Loaiphong>.CreateAsync(loaiphongs, pageNumber, pageSize));

        }

        public async Task<IActionResult> CreateRoomType()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRoomType(Loaiphong loaiphong)
        {

            if (!ModelState.IsValid)
            {
                return View(loaiphong);
            }

            await _lprepo.AddAsync(loaiphong);
            return RedirectToAction("RoomType");
        }


        public async Task<IActionResult> UpdateRoomType(string roomtypeid)
        {
            var roomType = await _lprepo.GetByIdAsync(int.Parse(roomtypeid));
            return View(roomType);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRoomType(Loaiphong loaiphong, string roomtypeid)
        {
            int id = int.Parse(roomtypeid);
            if (!ModelState.IsValid)
            {
                return View(loaiphong);
            }

            await _lprepo.UpdateAsync(loaiphong, id);
            return RedirectToAction("RoomType");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRoomType(string roomtypeid)
        {
            await _lprepo.DeleteAsync(int.Parse(roomtypeid));
            return RedirectToAction("RoomType");
        }
    }
}
