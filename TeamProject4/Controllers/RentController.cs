using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Team_Project_4.InterfacesRepositories;
using Team_Project_4.Repositories;
using Team_Project_4.Models;
using Team_Project_4.ViewModels;
using System.Diagnostics;

public class RentController : Controller
{
    private readonly IRentRepository _rentRepository;
    private readonly IKhachhangRepository _clientRepo;
    private readonly IPhongRepository _roomRepo;
    public RentController(IRentRepository rentRepository, IKhachhangRepository clientRepository, IPhongRepository roomRepo)
    {
        _rentRepository = rentRepository;
        _clientRepo = clientRepository;
        _roomRepo = roomRepo;
    }

    [HttpGet]
    public async Task<IActionResult> Index(int manager)
    {
        TempData["Manager"] = manager;
        var rents = await _rentRepository.GetAllAsync();
        return View(rents);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, int manager)
    {
        TempData["Manager"] = manager;
        if (id == null || !(await _rentRepository.RentExists(id)))
        {
            return NotFound();
        }

        var rent = await _rentRepository.GetByIdAsync(id);

        // Lấy danh sách khách hàng có mã phòng giống với mã phòng trên phiếu thuê
        var customers = await _clientRepo.GetCustomersByRoomIdAsync(rent.Map);

        // Tạo một đối tượng ViewModel để chứa cả phiếu thuê và danh sách khách hàng
        var viewModel = new RentDetailsList
        {
            phieuthue = rent,
            khachhangs = customers
        };

        return View(viewModel);
    }


    [HttpGet]
    public async Task<IActionResult> Create(int manager)
    {
        TempData["Manager"] = manager;
        var roomList = await _roomRepo.GetRoomsByTinhtrangAsync(1);
        ViewData["Map"] = new SelectList(roomList, "Map", "Tenphong");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Phieuthue rent,int manager)
    {
        // Kiểm tra CCCD và lấy thông tin khách hàng
        var client = await _clientRepo.GetClientByCCCDAsync(rent.Cccd);

        if (client == null || client.Makh == 0)
        {
            TempData["Manager"] = manager;
            // Display an error message to the user
            ModelState.AddModelError("CCCD", "CCCD không hợp lệ. Hãy nhập lại CCCD của bạn");

            // Return the view with the model to display the error message
            var roomList = await _roomRepo.GetRoomsByTinhtrangAsync(1);
            ViewData["Map"] = new SelectList(roomList, "Map", "Tenphong");
            return View(rent);
        }

        // Assign Makh to the rent object
        rent.Makh = client.Makh;

        // Add the rental entry to the repository
        await _rentRepository.AddAsync(rent);
        var room = await _roomRepo.GetByIdAsync(rent.Map);
        if (room != null)
        {
            room.Tinhtrang = 2; // Update Tinhtrang to the desired value
            await _roomRepo.UpdateAsync(room);
        }
        return RedirectToAction(nameof(Index), new {manager=manager});
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id,int manager,int roomid)
    {
        TempData["Manager"] = manager;
        if (id == null)
        {
            return NotFound();
        }

        var rentExists = await _rentRepository.RentExists(id.Value);
        if (!rentExists)
        {
            return NotFound();
        }

        var clientTypes = _clientRepo.GetAllAsync(); // Fetch client types again

        var roomList = await _roomRepo.GetRoomsByTinhtrangAsync(1, roomid);
 
        ViewBag.Map = new SelectList(roomList, "Map", "Tenphong");
        // Fetch the existing rent data
        var rent = await _rentRepository.GetByIdAsync(id.Value);
        return View(rent);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,Phieuthue rent,int manager,int roomOldId)
    {
        TempData["Manager"] = manager;
        if (id ==null)
        {
            return NotFound();
        }

        var client = await _clientRepo.GetClientByCCCDAsync(rent.Cccd);

        if (client == null || client.Makh == 0)
        {
            TempData["Manager"] = manager;
            // Display an error message to the user
            ModelState.AddModelError("CCCD", "Vui lòng nhập căn cước công dân");

            // Return the view with the model to display the error message
            var roomList = await _roomRepo.GetRoomsByTinhtrangAsync(1);
            ViewData["Map"] = new SelectList(roomList, "Map", "Tenphong");
            return View(rent);
        }

        // Assign Makh to the rent object
        rent.Makh = client.Makh;
        rent.Mapt = id;
        var roomOld = await _roomRepo.GetByIdAsync(roomOldId);
        if (roomOld != null)
        {
            roomOld.Tinhtrang = 1; // Update Tinhtrang to the desired value
            await _roomRepo.UpdateAsync(roomOld);
        }
        var room = await _roomRepo.GetByIdAsync(rent.Map);
        if (room != null)
        {
            room.Tinhtrang = 2; // Update Tinhtrang to the desired value
            await _roomRepo.UpdateAsync(room);
        }
        await _rentRepository.UpdateAsync(rent);

        return RedirectToAction(nameof(Index), new {manager=manager});
        /*
        var clientTypes = _clientRepo.GetAllAsync();
        var rooms = _roomRepo.GetAllAsync();
        ViewBag.Makh = new SelectList(clientTypes, "Makh", "Tenkh");
        ViewBag.Map = new SelectList(rooms, "Map", "Tenphong");

        return View(rent);
        */
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || !(await _rentRepository.RentExists(id.Value)))
        {
            return NotFound();
        }

        var rent = await _rentRepository.GetByIdAsync(id.Value);
        return View(rent);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Lấy thông tin về phiếu thuê cần xóa
        var rent = await _rentRepository.GetByIdAsync(id);

        // Kiểm tra nếu phiếu thuê không tồn tại
        if (rent == null)
        {
            return NotFound();
        }

        // Lấy danh sách khách hàng có map giống với map của phiếu thuê
        var customers = await _clientRepo.GetCustomersByRoomIdAsync(rent.Map);
        customers = customers.Where(c => c.Makh != rent.Makh).ToList();
        // Xóa từng khách hàng trong danh sách
        foreach (var customer in customers)
        {
            await _clientRepo.DeleteAsync(customer.Makh);
        }

        // Xóa phiếu thuê

        var room = await _roomRepo.GetByIdAsync(rent.Map);
        if (room != null)
        {
            room.Tinhtrang = 1; // Update Tinhtrang to the desired value
            await _roomRepo.UpdateAsync(room);
        }
        await _rentRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }


}
