using System;
using System.Collections.Generic;

namespace Slot_api.Models
{
    public partial class Userinfo
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Createdate { get; set; }
        public int Score { get; set; }
    }
}
