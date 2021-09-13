using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WundermanThompson.Helper
{
    public class Constant
    {
        public static class ActionType
        {
            public const string
                UpdateProfile = "1",
                LeaveRequest = "2",
                ApproveRequest = "3";
        }

        public static class NotificationType
        {
            public const string
                SMS = "1",
                Email = "2",
                WebPushNotification = "3";
        }
    }
}
