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

namespace WundermanThompson.Service
{
    public class NotificationService : INotification
    {
        public bool UpdateProfile(EmployeeDto employee, string input, string preference, EmployeeInformationContext _db)
        {
            try
            {
                bool isNotified = false;
                using (var dbTransaction = _db.Database.BeginTransaction())
                {
                    var updatedData = _db.Employees.FirstOrDefault(x => x.Id == employee.ID);
                    if (updatedData != null)
                    {
                        updatedData.UserId = employee.UserID;
                        updatedData.FirstName = employee.FirstName;
                        updatedData.LastName = employee.LastName;
                        updatedData.PhoneNumber = employee.PhoneNumber;
                        updatedData.ManagerId = employee.ManagerID;
                        _db.Entry(updatedData).State = EntityState.Modified;
                        _db.SaveChanges();
                        isNotified = true;
                    }
                    if (isNotified)
                    {
                        isNotified = SendNotification(input, employee.ID, _db, preference);
                    }
                    dbTransaction.Commit();
                }
                return isNotified;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SendNotification(string type, int employeeId, EmployeeInformationContext _db, string preference)
        {
            try
            {
                bool isReturn = false;
                var employee = _db.Employees.FirstOrDefault(x => x.Id == employeeId);
                if (employee != null)
                {
                    if (type == ActionType.UpdateProfile)
                    {
                        //Notification for the employee
                        var user = _db.Users.FirstOrDefault(x => x.Id == employee.UserId);
                        if (user != null)
                        {
                            NotificationMessage(employee.UserId, user.UserName, preference);
                        }
                        //Notification for Manager
                        var employeeManager = _db.Employees.FirstOrDefault(x => x.Id == employee.ManagerId);
                        if (employeeManager != null)
                        {
                            user = _db.Users.FirstOrDefault(x => x.Id == employeeManager.UserId);
                            if (user != null)
                            {
                                NotificationMessage(employeeManager.UserId, user.UserName, preference);
                            }
                        }

                        //Notification for HR Managers
                        var hrManagers = (from userTbl in _db.Users
                                          join roleTbl in _db.Roles.Where(x => x.Name == "HR Manager")
                                          on userTbl.RoleId equals roleTbl.Id
                                          select userTbl).ToList();
                        foreach (var hr in hrManagers)
                        {
                            NotificationMessage(hr.Id, hr.UserName, preference);
                        }
                        isReturn = true;
                    }
                    else if (type == ActionType.LeaveRequest)
                    {
                        //Notification for Manager
                        var employeeManager = _db.Employees.FirstOrDefault(x => x.Id == employee.ManagerId);
                        if (employeeManager != null)
                        {
                            var user = _db.Users.FirstOrDefault(x => x.Id == employeeManager.UserId);
                            if (user != null)
                            {
                                NotificationMessage(employeeManager.UserId, user.UserName, preference);
                            }
                            isReturn = true;
                        }
                    }
                    else if (type == ActionType.ApproveRequest)
                    {
                        //Notification for Employee
                        var user = _db.Users.FirstOrDefault(x => x.Id == employee.UserId);
                        if (user != null)
                        {
                            NotificationMessage(employee.UserId, user.UserName, preference);
                        }
                        isReturn = true;
                    }
                }
                return isReturn;
            }
            catch
            {
                return false;
            }
        }

        private static void NotificationMessage(int? userId, string userName, string preference)
        {
            if (preference == NotificationType.SMS)
            {
                Console.WriteLine("SMS is sent to" + userId + ", " + userName);
            }
            else if (preference == NotificationType.Email)
            {
                Console.WriteLine("Email is sent to" + userId + ", " + userName);
            }
            else if (preference == NotificationType.WebPushNotification)
            {
                Console.WriteLine("Web push notification is sent to" + userId + ", " + userName);
            }
        }

        public bool LeaveRequest(EmployeeDto employee, string input, string preference, EmployeeInformationContext _db)
        {
            return SendNotification(input, employee.ID, _db, preference);
        }

        public bool ApproveOrDeclineRequest(int employeeId, string input, string preference, EmployeeInformationContext _db)
        {
            return SendNotification(input, employeeId, _db, preference);
        }
    }
}
