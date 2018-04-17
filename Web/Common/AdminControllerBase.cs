using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZGW.GMS.Platform.BusinessEntity;

namespace ZGW.GMS.Web.Common
{
    public class AdminControllerBase
    {
        /// <summary>
        /// 登录后用户信息里的用户权限
        /// </summary>
        public static  List<EnumBusinessPermission> PermissionList
        {
            get
            {
                var permissionList = new List<EnumBusinessPermission>();

                if (HttpContext.Current.Session["user"] != null)
                {
                    User user = HttpContext.Current.Session["user"] as User;
                    permissionList = user.BusinessPermissionList;
                }

                return permissionList;
            }
        }
    }
}