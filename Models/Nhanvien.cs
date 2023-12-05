using System;
using System.Collections.Generic;

namespace Team_Project_4.Models
{
    public partial class Nhanvien
    {
        public Nhanvien()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        public string Manv { get; set; } = null!;
        public string? Hoten { get; set; }
        public string? Phai { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string? Sdt { get; set; }
        public string? Email { get; set; }
        public string? Diachi { get; set; }

        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
