using Microsoft.EntityFrameworkCore;
using Team_Project_4.InterfacesRepositories;
using Team_Project_4.Models;
using Team_Project_4.ViewModels;

namespace Team_Project_4.Repositories
{
    public class SaleReportRepository : ISaleReport
    {
        private readonly HotelDbContext _htDbContext;
        public SaleReportRepository(HotelDbContext htDbContext)
        {
            _htDbContext = htDbContext;
        }
        /*
        public async Task<List<SaleReportViewModel>> GetSaleReportForMonthYear(int month)
        {
            var saleReport = _htDbContext.Hoadons
                .Where(hd => hd.Ngaylaphd.Month == month && hd.Ngaylaphd.Year == DateTime.Now.Year)
                .Select(hd => new { hd.Tenphong, hd.Tongtien })
                .ToList();

            var saleReportGroupedByRoomType = saleReport
                .GroupBy(sr => sr.Tenphong)
                .Select(group => new
                {
                    RoomType = group.Key,
                    TotalRevenue = group.Sum(item => item.Tongtien)
                })
                .ToList();

            var totalRevenue = saleReportGroupedByRoomType.Sum(sr => sr.TotalRevenue);

            var saleReportViewModel = saleReportGroupedByRoomType
                .Select(sr => new SaleReportViewModel
                {
                    loaiphongNavigation = _htDbContext.Phongs
                        .Where(p => p.Tenphong == sr.RoomType)
                        .Select(p => p.MaloaiphongNavigation)
                        .FirstOrDefault(),
                    doanhThu = sr.TotalRevenue,
                    tyle = totalRevenue != 0 ? (float)sr.TotalRevenue / totalRevenue : 0
                })
                .ToList();

            return saleReportViewModel;
        }
        */
        public async Task<List<SaleReportViewModel>> GetSaleReportForMonthYear(int month)
        {
            var saleReports = _htDbContext.Hoadons
                .Where(hd => hd.Ngaylaphd.Month == month && hd.Ngaylaphd.Year == DateTime.Now.Year)
                .Select(hd => new { hd.Tenphong, hd.Tongtien })
                .ToList();

            var aggregatedSaleReports = saleReports
                .GroupBy(sr => sr.Tenphong)
                .Select(group => new SaleReportViewModel
                {
                    loaiphongNavigation = _htDbContext.Phongs
                        .Where(p => p.Tenphong == group.Key)
                        .Select(p => p.MaloaiphongNavigation)
                        .FirstOrDefault(),
                    doanhThu = group.Sum(item => item.Tongtien)
                })
                .GroupBy(sr => sr.loaiphongNavigation)
                .Select(group => new SaleReportViewModel
                {
                    loaiphongNavigation = group.Key,
                    doanhThu = group.Sum(item => item.doanhThu)
                })
                .ToList();

            var totalRevenue = aggregatedSaleReports.Sum(sr => sr.doanhThu);

            // Calculate percentages
            foreach (var saleReport in aggregatedSaleReports)
            {
                saleReport.tyle = totalRevenue != 0 ? (float)saleReport.doanhThu / totalRevenue : 0;
            }

            return aggregatedSaleReports;
        }


    }
}
