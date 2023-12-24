using Microsoft.EntityFrameworkCore;
using Team_Project_4.Models;

namespace Team_Project_4.InterfacesRepositories
{
    public interface ILoaiKhachRepository
    {
        Task<Loaikhach> GetByIdAsync(int id);
        IQueryable<Loaikhach> GetAllAsync();
        Task AddAsync(Loaikhach loaikhach);
        Task UpdateAsync(Loaikhach loaikhach);
        Task DeleteAsync(int Id);
        Task<List<string>> GetDistinctClientTypeAsync();

    }
}