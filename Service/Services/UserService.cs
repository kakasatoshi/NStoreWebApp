using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUser
    {
        private readonly NStoreContext _context;

        public UserService(NStoreContext context)
        {
            _context = context;
        }

        public User Login(string UserName, string Password)
        {
            var user = new User();
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                try
                {
                    user = _context.Users.FirstOrDefault(x => x.Identification.Equals(UserName)
                                        && x.Password.Equals(Password));
                }
                catch { }
            }
            return user;
        }
    }
}