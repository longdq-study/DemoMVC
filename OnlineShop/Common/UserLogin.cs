﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop
{
    [Serializable]
    public class UserLogin
    {
        public long UserId { set; get;  }
        public string UserName { set; get; }
    }
}