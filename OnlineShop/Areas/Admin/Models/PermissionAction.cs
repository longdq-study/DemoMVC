using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Areas.Admin.Models
{
    public class PermissionAction
    {
        public long PermissionId { get; set; }
        public string PermissionName { get; set; }
        public string Description { get; set; }
        public bool isGrant { get; set; }
    }
}