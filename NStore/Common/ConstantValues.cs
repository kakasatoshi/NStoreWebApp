using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NStore.Common
{
    public class ConstantValues
    {
        public class Product
        {
            public const string Paging = "api/Products/paging";
            public const string Detail = "api/Products/Detail";
        }

        public class Category
        {
            public const string GetAll = "api/Categories/GetAll";
        }

        public class User
        {
            public const string DanhSachNhanVien = "api/NhanVien/DanhSachNhanVien";
            public const string Login = "api/Users/Login";
            public const string Logout = "api/Users/Logout";
            public const string DoiMatKhau = "api/NhanVien/ThayDoiMatKhau";
        }
    }
}