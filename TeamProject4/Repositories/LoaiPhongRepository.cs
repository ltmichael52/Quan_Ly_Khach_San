using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using Team_Project_4.InterfacesRepositories;
using Team_Project_4.Models;

namespace Team_Project_4.Repositories
{
    public class LoaiPhongRepository : ILoaiPhongRepository
    {
        private readonly HotelDbContext _dbContext;
        //Dependency Injection
        public LoaiPhongRepository(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Loaiphong> GetByIdAsync(int id)
        {
            var loaiphong = await _dbContext.Loaiphongs.FindAsync(id);
            return loaiphong;
        }

        public IQueryable<Loaiphong> GetAllAsync()
        {
            var loaiphongs = _dbContext.Loaiphongs
            .Select(loaiphong => new Loaiphong
            {
                Maloaiphong = loaiphong.Maloaiphong,
                Tenloai = loaiphong.Tenloai,
                Dongia = loaiphong.Dongia
            });

            return loaiphongs;

        }

        public async Task AddAsync(Loaiphong loaiphong)
        {

            await _dbContext.Loaiphongs.AddAsync(loaiphong);
            await _dbContext.SaveChangesAsync();
        }



        public async Task UpdateAsync(Loaiphong loaiphongmoi, int id)
        {
            var loaiphong = await _dbContext.Loaiphongs.FindAsync(id);

            loaiphong.Tenloai = loaiphongmoi.Tenloai;
            loaiphong.Dongia = loaiphongmoi.Dongia;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            Loaiphong loaiphong = await _dbContext.Loaiphongs
                .Include(lp => lp.Phongs)
                .FirstOrDefaultAsync(lp => lp.Maloaiphong == Id);

            foreach (var phong in loaiphong.Phongs.ToList())
            {
                _dbContext.Phongs.Remove(phong);
            }

            _dbContext.Loaiphongs.Remove(loaiphong);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<string>> GetDistinctRoomTypesAsync()
        {
            return await _dbContext.Loaiphongs.Select(r => r.Tenloai).Distinct().ToListAsync();
        }

    }
}
