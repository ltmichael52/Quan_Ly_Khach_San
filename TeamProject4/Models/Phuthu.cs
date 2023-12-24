using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team_Project_4.Models
{
    public partial class Phuthu
    {
        public int Idphuthu { get; set; }
        [Required(ErrorMessage="Vui lòng nhập giá trị phụ thu")]
        public double? Giatriphuthu { get; set; }
    }
}
