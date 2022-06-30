using System;
using System.Collections.Generic;

#nullable disable

namespace Service.Models
{
    public partial class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Gender { get; set; }
        public bool Active { get; set; }
        public string Password { get; set; }
    }
}
