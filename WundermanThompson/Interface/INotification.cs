using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WundermanThompson.Entities;
using WundermanThompson.Model;

namespace WundermanThompson.Interface
{
    public interface INotification
    {
        bool UpdateProfile(EmployeeDto employee, string input, string preference, EmployeeInformationContext _db);
        bool LeaveRequest(EmployeeDto employee, string input, string preference, EmployeeInformationContext _db);
        bool ApproveOrDeclineRequest(int employeeId, string input, string preference, EmployeeInformationContext _db);
    }
}
