using System;
using System.Collections.Generic;

namespace Team_Project_4.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Phieuthues = new HashSet<Phieuthue>();
        }

        public int Makh { get; set; }
        public string Tenkh { get; set; } = null!;
        public int Tuoi { get; set; }
        public string Tel { get; set; } = null!;
        public string? Diachikh { get; set; }
        public string Cmndkh { get; set; } = null!;
        public int Maloaikhach { get; set; }
        public int Map { get; set; }

        public virtual Loaikhach MaloaikhachNavigation { get; set; } = null!;
        public virtual Phong MapNavigation { get; set; } = null!;
        public virtual ICollection<Phieuthue> Phieuthues { get; set; }
    }
}
