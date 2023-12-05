using System;
using System.Collections.Generic;

namespace Team_Project_4.Models
{
    public partial class Khach
    {
        public Khach()
        {
            Hoadons = new HashSet<Hoadon>();
            Phieuthues = new HashSet<Phieuthue>();
        }

        public string Makh { get; set; } = null!;
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
