using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team_Project_4.Models
{
    public partial class Phong
    {
        public Phong()
        {
            Phieuthues = new HashSet<Phieuthue>();
        }

        [Required(ErrorMessage = "Vui lòng điền mã phòng")]
        public int? Map { get; set; }
        [Required(ErrorMessage = "Vui lòng điền tên phòng")]
        public string Tenphong { get; set; } = null!;
        public string Loai { get; set; } = null!;
        public int Dongia { get; set; }
        public bool Tinhtrang { get; set; }
        public string? Ghichu { get; set; }

        public virtual ICollection<Phieuthue> Phieuthues { get; set; }
    }
}
