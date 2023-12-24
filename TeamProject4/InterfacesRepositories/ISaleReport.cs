using Team_Project_4.Models;
using Team_Project_4.ViewModels;

namespace Team_Project_4.InterfacesRepositories
{
    public interface ISaleReport
    {
        Task<List<SaleReportViewModel>> GetSaleReportForMonthYear(int month);
    }
}
