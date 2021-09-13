using System;
using WundermanThompson.Model;
using WundermanThompson.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WundermanThompson.Interface;
using WundermanThompson.Service;

namespace WundermanThompson
{
    class Program
    {
        private static readonly INotification _notification1;
        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            Startup startup = new Startup();
            startup.ConfigureServices(services);

            Console.WriteLine("1. Update employee profile");
            Console.WriteLine("2. Leave request as an employee");
            Console.WriteLine("3. Approved/declined leave request");
            Console.WriteLine("Please select your choice: (ex: Type 1 for update profile)");
            string input = Console.ReadLine();
            Console.WriteLine("1. SMS");
            Console.WriteLine("2. Email");
            Console.WriteLine("3. Web push notification");
            Console.WriteLine("Please select your notification preference: (ex: Type 1 for SMS notification)");
            string preference = Console.ReadLine();
            EmployeeDto employee = new EmployeeDto()
            {
                ID = 2,
                UserID = 000315,
                FirstName = "Ibrahim",
                LastName = "Sumon",
                PhoneNumber = "01622677031",
                ManagerID = 1
            };
            EmployeeInformationContext _db = new EmployeeInformationContext();
            Notify notification = new Notify(_db, _notification1);
            if (input == "1")
            {
                bool isNotified = notification.UpdateProfile(employee, input, preference);
                MessageAfterNotify(isNotified);
            }
            else if (input == "2")
            {
                bool isNotified = notification.LeaveRequest(employee, input, preference);
                MessageAfterNotify(isNotified);
            }
            else if (input == "3")
            {
                bool isNotified = notification.ApproveOrDeclineRequest(employee.ID, input, preference);
                MessageAfterNotify(isNotified);
            }
        }

        private static void MessageAfterNotify(bool isNotified)
        {
            if (isNotified)
            {
                Console.WriteLine("Hurrah! Notification sent successfully.");
            }
            else
            {
                Console.WriteLine("Oops! Some error occurred.");
            }
        }

        public class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                var connectionString = "Server=DESKTOP-79Q2RII;Database=EmployeeInformation;User Id=sa;Password=123456;Trusted_Connection=True;";
                services.AddDbContext<EmployeeInformationContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Transient);
                services.AddTransient<INotification, NotificationService>();
                services.BuildServiceProvider();
            }
        }
    }
}
