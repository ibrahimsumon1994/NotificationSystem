using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WundermanThompson.Entities;
using WundermanThompson.Interface;
using WundermanThompson.Model;
using static WundermanThompson.Helper.Constant;

namespace WundermanThompson
{
    public class Notify
    {
        public readonly EmployeeInformationContext _db = new EmployeeInformationContext();
        public readonly INotification _notification;

        public Notify(EmployeeInformationContext db, INotification notification)
        {
            _db = db;
            _notification = notification;
        }

        public bool UpdateProfile(EmployeeDto employee, string input, string preference)
        {
            try
            {
                bool isNotified = false;
                isNotified = _notification.UpdateProfile(employee, input, preference, _db);
                return isNotified;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool LeaveRequest(EmployeeDto employee, string input, string preference)
        {
            try
            {
                bool isNotified = false;
                isNotified = _notification.LeaveRequest(employee, input, preference, _db);
                return isNotified;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ApproveOrDeclineRequest(int employeeId, string input, string preference)
        {
            try
            {
                bool isNotified = false;
                isNotified = _notification.ApproveOrDeclineRequest(employeeId, input, preference, _db);
                return isNotified;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
