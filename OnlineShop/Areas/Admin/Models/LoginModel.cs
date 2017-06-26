﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string Password { get; set; }
        public bool RememeberMe { get; set; }
    }
}