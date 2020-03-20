using System;

namespace CoderaShopping.Core
{
    public static class Context
    {
        public static Guid UserId { get; set; }
        public static string UserName { get; set; }
        public static int UserRole { get; set; }
    }
}

