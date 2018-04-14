using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Crm.BusinessEntity
{
    /// <summary>
    /// 用户基本数据
    /// </summary>
    public class BaseElectricityUser
    {
        /// <summary>
        /// ID
        /// </summary>
        public int ID { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string name { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string linkPhone { set; get; }
        /// <summary>
        /// 用电量
        /// </summary>
        public string electricityConsumption { set; get; }
        /// <summary>
        /// 操作人id
        /// </summary>
        public int operatorID { set; get; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime operatorTime { set; get; }
        /// <summary>
        /// 最后操作人
        /// </summary>
        public int lastOperatorID { set; get; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime lastOperateTime { set; get; }
        public int isDel { set; get; }
    }
}
