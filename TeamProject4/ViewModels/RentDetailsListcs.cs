using Microsoft.EntityFrameworkCore;
using Team_Project_4.Models;

namespace Team_Project_4.ViewModels
{
    public class RentDetailsList
    {
        public Phieuthue phieuthue { get; set; }
        public List<Khachhang> khachhangs { get; set; }
    }

}
