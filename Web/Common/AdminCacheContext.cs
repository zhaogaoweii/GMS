using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZGW.GMS.Core.Caching;


namespace ZGW.GMS.Web.Common
{
    public class AdminCacheContext 
    {
        public static AdminCacheContext Current
        {
            get
            {
                return CacheHelper.Get<AdminCacheContext>("");
            }
        }

       
    }
}
