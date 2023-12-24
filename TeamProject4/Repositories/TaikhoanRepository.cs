using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Team_Project_4.InterfacesRepositories;
using Team_Project_4.Models;

namespace Team_Project_4.Repositories
{
    public class TaikhoanRepository : ITaikhoanRepository
    {
        private readonly HotelDbContext _dbContext;

        public TaikhoanRepository(HotelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Taikhoan taikhoan)
        {
            _dbContext.Taikhoans.Add(taikhoan);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Taikhoan> GetByUsernameAndPasswordAsync(string tentknv, string mktk)
        {
            return await _dbContext.Taikhoans.FirstOrDefaultAsync(t => t.Tentknv == tentknv && t.Mktk == mktk);
        }
        public async Task CreateAccountForAllEmployee(IEnumerable<Nhanvien> employeesWithoutAccounts)
        {
            var newAccounts = employeesWithoutAccounts.Select(employee => new Taikhoan
            {
                Manv = employee.Manv,
                Tentknv = employee.Email,
                Mktk = "123456789",
            });

            _dbContext.Taikhoans.AddRange(newAccounts);
            await _dbContext.SaveChangesAsync();
        }

    }
}
