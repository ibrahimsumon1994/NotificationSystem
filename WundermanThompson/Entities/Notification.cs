using System;
using System.Collections.Generic;

#nullable disable

namespace WundermanThompson.Entities
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public int? UserId { get; set; }
        public int? EmployeeId { get; set; }
    }
}
