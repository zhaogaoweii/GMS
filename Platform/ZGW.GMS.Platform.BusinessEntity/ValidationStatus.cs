using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Platform.BusinessEntity
{
    public enum ValidationStatus
    {
        Authenticated,
        OrgNotExist,
        UserNotExist,
        WrongPassword,
        AccountLocked
    }
}
