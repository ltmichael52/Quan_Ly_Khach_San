using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team_Project_4.Models
{
    public partial class Khachhang
    {
        public Khachhang()
        {
            Phieuthues = new HashSet<Phieuthue>();
        }

        public int Makh { get; set; }
        [Required(ErrorMessage="Vui lòng nhập tên")]
        public string Tenkh { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tuổi")]
        public int? Tuoi { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập sdt")]
        public string Tel { get; set; }
        public string? Diachikh { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập cccd")]
        public string Cmndkh { get; set; }
        public int Maloaikhach { get; set; }
        public int Map { get; set; }

        public virtual Loaikhach MaloaikhachNavigation { get; set; } = null!;
        public virtual Phong MapNavigation { get; set; } = null!;
        public virtual ICollection<Phieuthue> Phieuthues { get; set; }
    }
}
