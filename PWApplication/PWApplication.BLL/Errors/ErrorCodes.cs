using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWApplication.BLL.Errors
{
    public static class ErrorCodes
    {
        public const string USER_NOT_FOUND = "UserNotFound";

        public const string WRONG_PASSWORD = "WrongPassword";

        public const string BALANCE_LESS_THAN_AMOUNT = "BalanceLessThanAmount";
    }
}
