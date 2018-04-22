using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Platform.BusinessEntity
{
    public class UserRole
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { set; get; }
        public int UserID { set; get; }
        public int RoleID { set; get; }
        public DateTime CreateTime { set; get; }
    }
}
