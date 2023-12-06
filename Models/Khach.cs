using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team_Project_4.Models
{
    public partial class Khach
    {
        public Khach()
        {
            Hoadons = new HashSet<Hoadon>();
            Phieuthues = new HashSet<Phieuthue>();
        }

        [Required(ErrorMessage = ("Vui lòng nhập mã khách hàng"))]
        public int? Makh { get; set; } 
        public string Tenkh { get; set; } = null!;
        public int Tuoi { get; set; }
        public string Tel { get; set; } = null!;
        public string? Diachikh { get; set; }
        public string Cmndkh { get; set; } = null!;
        public string Loaikhach { get; set; } = null!;

        public virtual ICollection<Hoadon> Hoadons { get; set; }
        public virtual ICollection<Phieuthue> Phieuthues { get; set; }
    }
}
