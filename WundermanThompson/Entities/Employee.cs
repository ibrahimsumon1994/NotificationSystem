using System;
using System.Collections.Generic;

#nullable disable

namespace WundermanThompson.Entities
{
    public partial class Employee
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? ManagerId { get; set; }
    }
}
