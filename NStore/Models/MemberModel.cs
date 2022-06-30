using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Models
{
    public class MemberModel
    {
        public class MemberBase
        {
            public int Id { get; set; }

            [Required(ErrorMessage = "Họ tên phải khác rỗng")]
            [Display(Name = "Họ tên")]
            public string Name { get; set; }

            [Display(Name = "Giới tính")]
            public bool Gender { get; set; }

            [Display(Name = "Ngày sinh")]
            [DataType(DataType.Date)]
            public DateTime Dob { get; set; }

            [Required(ErrorMessage = "Email phải khác rỗng")]
            [Display(Name = "Email")]
            [DataType(DataType.EmailAddress)]
            [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Điện thoại phải khác rỗng")]
            [Display(Name = "Điện thoại")]
            public string Phone { get; set; }

            [Required(ErrorMessage = "Mật khẩu phải khác rỗng")]
            [Display(Name = "Mật khẩu")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public bool Active { get; set; }
        }

        public class Input
        {
            public class ThongTinThanhVienMoi : MemberBase
            { }

            public class ThongTinThanhVien
            {
                public int Id { get; set; }
            }

            public class ThongTinThayDoiMatKhau
            {
                public int Id { get; set; }
                public string Username { get; set; }
                public string MatKhauCu { get; set; }
                public string MatKhauMoi { get; set; }
            }

            public class ThongTinDangNhap
            {
                public string UserName { get; set; }
                public string Password { get; set; }
            }

            public class KichHoatTaiKhoan
            {
                public string Email { get; set; }
            }
        }

        public class Output
        {
            public class ThongTinThanhVienMoi : MemberBase
            {
                [Required(ErrorMessage = "Xác nhận Mật khẩu phải khác rỗng")]
                [Display(Name = "Xác nhận mật khẩu")]
                [Compare("MatKhau", ErrorMessage = "Xác nhận lại mật khẩu không đúng.")]
                [DataType(DataType.Password)]
                public string XacNhanMatKhau { get; set; }
            }

            public class MemberInfo : MemberBase
            {
                public string AccessToken { get; set; }
                public string Noteti { get; set; }
            }
        }
    }
}