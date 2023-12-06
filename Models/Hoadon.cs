using System;
using System.Collections.Generic;

namespace Team_Project_4.Models
{
    public partial class Hoadon
    {
        public int Mahd { get; set; }
        public int? Songayo { get; set; }
        public string? Manv { get; set; }
        public int? Tongtien { get; set; }
        public string? Mapt { get; set; }
        public int? Makh { get; set; }

        public virtual Khach? MakhNavigation { get; set; }
        public virtual Nhanvien? ManvNavigation { get; set; }
        public virtual Phieuthue? MaptNavigation { get; set; }
    }
}
