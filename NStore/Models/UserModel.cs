using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Models
{
    public class UserModel
    {
        public class UserBase
        {
            public int Id { get; set; }

            [Display(Name = "Họ tên nhân viên")]
            [Required(ErrorMessage = "Họ tên phải khác rỗng")]
            public string Name { get; set; }

            [Display(Name = "Giới tính")]
            public bool Gender { get; set; }

            [Display(Name = "Ngày sinh")]
            public DateTime Dob { get; set; }

            [Display(Name = "Địa chỉ")]
            public string Address { get; set; }

            [Required(ErrorMessage = "Số CMND phải khác rỗng")]
            [Display(Name = "Số CMND")]
            public string Identification { get; set; }

            [Required(ErrorMessage = "Mật khẩu phải khác rỗng")]
            [Display(Name = "Mật khẩu")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Quyền hạn")]
            public string Role { get; set; }
        }

        public class Input
        {
            public class LoginInfo
            {
                public string UserName { get; set; }
                public string Password { get; set; }
            }

            public class ThongTinThayDoiMatKhau
            {
                public int Id { get; set; }
                public string Username { get; set; }
                public string MatKhauCu { get; set; }
                public string MatKhauMoi { get; set; }
            }

            public class DanhSachNhanVien
            {
                public bool QuanTri { get; set; }
            }
        }

        public class Output
        {
            public class UserInfo : UserBase
            {
                public string AccessToken { get; set; }
                public string Noteti { get; set; }
            }
        }
    }
}