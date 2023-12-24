using Microsoft.EntityFrameworkCore;
using System;
using Team_Project_4.InterfacesRepositories;
using Team_Project_4.Models;

namespace Team_Project_4.Repositories
{
    public class LoaiKhachRepository : ILoaiKhachRepository
    {
        private readonly HotelDbContext _dbContext;

        public LoaiKhachRepository(HotelDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task AddAsync(Loaikhach lkhach)
        {
            await _dbContext.Loaikhaches.AddAsync(lkhach);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Loaikhach khach = await _dbContext.Loaikhaches
                .Include(l => l.Khachhangs) // Include associated Khachhangs
                .FirstOrDefaultAsync(l => l.Maloaikhach == Id);

            if (khach != null)
            {
                _dbContext.Khachhangs.RemoveRange(khach.Khachhangs); // Remove associated Khachhangs
                _dbContext.Loaikhaches.Remove(khach); // Remove Loaikhach
                await _dbContext.SaveChangesAsync(); // Save changes
            }
        }


        public IQueryable<Loaikhach> GetAllAsync()
        {
            var khachs = _dbContext.Loaikhaches
            .Select(khach => new Loaikhach
            {
                Maloaikhach = khach.Maloaikhach,
                Tenloaikhach = khach.Tenloaikhach,

            });

            return khachs;
        }

        public async Task<Loaikhach> GetByIdAsync(int id)
        {
            var lkhach = await _dbContext.Loaikhaches.FindAsync(id);
            return lkhach;
        }

        public async Task UpdateAsync(Loaikhach lkhachUpdate)
        {

            _dbContext.Loaikhaches.Update(lkhachUpdate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Loaikhach>> GetAllLoaikhach()
        {
            return await _dbContext.Loaikhaches.ToListAsync();
        }
        public async Task<Loaikhach> GetClientTypeById(int id)
        {
            return await _dbContext.Loaikhaches.FindAsync(id); // Assuming _context is your DbContext
        }
        public async Task<List<string>> GetDistinctClientTypeAsync()
        {
            return await _dbContext.Loaikhaches.Select(r => r.Tenloaikhach).Distinct().ToListAsync();
        }

    }
}
