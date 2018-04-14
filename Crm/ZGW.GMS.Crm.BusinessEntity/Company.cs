using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Crm.BusinessEntity
{
    /// <summary>
    /// 公司
    /// </summary>
    public class Company:BaseElectricityUser
    {
        /*
         * 
         * 客户分为，用电公司，用电一般用户～如果是用电公司，就是包括，公司名，公司法人，联系电话，地址，注册号，用电量等；
         * 如果是用电一般用户，就包括户主姓名，联系电话，用电量等
         */
        /// <summary>
        /// 公司法人
        /// </summary>
        public string legalPerson { set;get;}
        /// <summary>
        /// 地址
        /// </summary>
        public string officeAddress { set; get; }
        /// <summary>
        /// 注册号
        /// </summary>
        public string registerCode { set; get; }
    }
}
