using System;
using System.Collections.Generic;

#nullable disable

namespace WundermanThompson.Entities
{
    public partial class User
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int? RoleId { get; set; }
    }
}
