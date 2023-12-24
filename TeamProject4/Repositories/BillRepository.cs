using Microsoft.EntityFrameworkCore;
using Team_Project_4.Models;

public class BillRepository : IBillRepository
{
    private readonly HotelDbContext _context;

    public BillRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<List<Hoadon>> GetAllBills()
    {
        return await _context.Hoadons
           
            .Include(h => h.ManvNavigation)
          
            .ToListAsync();
    }

    public async Task<Hoadon> GetBillById(int id)
    {
        return await _context.Hoadons

            .Include(h => h.ManvNavigation)
          
            .FirstOrDefaultAsync(m => m.Mahd == id);
    }

    public async Task CreateBill(Hoadon hoadon)
    {
        _context.Add(hoadon);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateBill(Hoadon hoadon)
    {
        _context.Update(hoadon);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBill(int id)
    {
        var hoadon = await _context.Hoadons.FindAsync(id);
        if (hoadon != null)
        {
            _context.Hoadons.Remove(hoadon);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> BillExists(int id)
    {
        return await _context.Hoadons.AnyAsync(e => e.Mahd == id);
    }
}
