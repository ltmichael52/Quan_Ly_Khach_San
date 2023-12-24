using Microsoft.EntityFrameworkCore;
using Team_Project_4.Models;

namespace Team_Project_4.Repositories
{
    public interface ILoaiPhongRepository
    {
        Task<Loaiphong> GetByIdAsync(int id);
        IQueryable<Loaiphong> GetAllAsync();
        Task AddAsync(Loaiphong loaiphong);
        Task UpdateAsync(Loaiphong loaiphong, int id);
        Task DeleteAsync(int Id);
        Task<List<string>> GetDistinctRoomTypesAsync();

    }
}
