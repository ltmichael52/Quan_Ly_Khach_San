﻿using System.Threading.Tasks;
using Team_Project_4.Models;

namespace Team_Project_4.Repositories
{
    public interface IPhongRepository
    {
        Task<Phong> GetByIdAsync(int id);
        IQueryable<Phong> GetAllAsync();
        Task AddAsync(Phong phong);
        Task UpdateAsync(Phong phong);
        Task DeleteAsync(int Id);
        Task<List<Loaiphong>> GetAllLoaiphong();
        Task<Loaiphong> GetRoomTypeById(int id);
        Task<List<string>> GetDistinctRoomTypeAsync();
        Task<List<Phong>> GetRoomUsageReportAsync(int? thang);
        Task<List<Phong>> GetRoomsByTinhtrangAsync(int tinhtrang,int roomid=0);
        Task<Phong> GetRoomByNameAsync(string tenphong);
        Task<List<Phong>> GetRoomUsageReportAsync(int thang);
        Task<List<int>> GetSoluongkhachtoidaList();
    }
}
