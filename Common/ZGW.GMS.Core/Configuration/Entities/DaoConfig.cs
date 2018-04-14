using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGW.GMS.Core.Configuration.Entities
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    [Serializable]
    public class DaoConfig 
    {
        public DaoConfig()
        {
        }
        #region 序列化属性
        public String Account { get; set; }
        public String Log { get; set; }
        public String Cms { get; set; }
        public String Crm { get; set; }
        public String OA { get; set; }
        #endregion
    }
}
