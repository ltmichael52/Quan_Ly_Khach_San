using System;
using System.Collections.Generic;

namespace Team_Project_4.Models
{
    public partial class Phieuthue
    {
        public Phieuthue()
        {
            Hoadons = new HashSet<Hoadon>();
        }

        public string Mapt { get; set; } = null!;
        public DateTime Ngaylappt { get; set; }
        public int Makh { get; set; }
        public int Map { get; set; }

        public virtual Khach MakhNavigation { get; set; } = null!;
        public virtual Phong MapNavigation { get; set; } = null!;
        public virtual ICollection<Hoadon> Hoadons { get; set; }
    }
}
