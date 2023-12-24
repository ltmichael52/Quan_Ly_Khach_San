using Microsoft.EntityFrameworkCore;
using Team_Project_4.InterfacesRepositories;
using Team_Project_4.Models;

public class PhuthuRepository : IPhuthuRepository
{
    private readonly HotelDbContext _context;

    public PhuthuRepository(HotelDbContext context)
    {
        _context = context;
    }

    public async Task<List<Phuthu>> GetAllPhuthusAsync()
    {
        return await _context.Phuthus.ToListAsync();
    }

    public async Task<Phuthu> GetPhuthuByIdAsync(double id)
    {
        return await _context.Phuthus.FirstOrDefaultAsync(m => m.Idphuthu == id);
    }

    public async Task AddPhuthuAsync(Phuthu phuthu)
    {
        _context.Add(phuthu);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePhuthuAsync(Phuthu phuthu)
    {
        _context.Update(phuthu);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePhuthuAsync(double id)
    {
        var phuthu = await _context.Phuthus.FindAsync(id);
        if (phuthu != null)
        {
            _context.Phuthus.Remove(phuthu);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> PhuthuExistsAsync(double id)
    {
        return (_context.Phuthus?.Any(e => e.Idphuthu == id)).GetValueOrDefault();
    }
    public async Task<Phuthu> GetFirstPhuthuAsync()
    {
        return await _context.Phuthus.FirstOrDefaultAsync();
    }
}
