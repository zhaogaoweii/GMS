using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 操作结果信息
    /// </summary>
    public class OperatingResultEntity
    {
        /// <summary>
        /// 状态 1 成功 2 失败
        /// </summary>
        public int Status { get; set; }
      
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回页面URL
        /// </summary>
        public string BackUrl { get; set; }

        /// <summary>
        /// 返回页面名称
        /// </summary>
        public string BackPageName { get; set; }

        /// <summary>
        /// 几秒之后返回相应的页面【默认10秒】
        /// </summary>
        public int SecondsBack { get; set; }

        /// <summary>
        /// 点击“确定”或“返回”执行的JS代码
        /// </summary>
        public string Script { get; set; }
    }   
}
