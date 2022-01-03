using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreModel
{
    public class RegisterModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public long Phone { get; set; }
    }
}
