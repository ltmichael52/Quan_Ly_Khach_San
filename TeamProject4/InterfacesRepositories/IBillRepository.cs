using Team_Project_4.Models;

public interface IBillRepository
{
    Task<List<Hoadon>> GetAllBills();
    Task<Hoadon> GetBillById(int id);
    Task CreateBill(Hoadon hoadon);
    Task UpdateBill(Hoadon hoadon);
    Task DeleteBill(int id);
    Task<bool> BillExists(int id);
  
}