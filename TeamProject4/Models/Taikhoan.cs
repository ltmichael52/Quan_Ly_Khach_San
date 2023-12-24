using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Team_Project_4.Models
{
    public partial class Taikhoan 
    {
        public int Matknv { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên tài khoản")]
        public string Tentknv { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu tài khoản")]
        public string Mktk { get; set; } = null!;
        public int Manv { get; set; }

        public virtual Nhanvien ManvNavigation { get; set; } = null!;
    }
}
