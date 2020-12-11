using System;

namespace app
{
    class Global
    {
        public static Guid userId = Guid.Empty;
        public static User user;

        public static bool IsLoggedIn()
        {
            if (userId != Guid.Empty)
            {
                return true;
            }
            return false;
        }
    }

}
