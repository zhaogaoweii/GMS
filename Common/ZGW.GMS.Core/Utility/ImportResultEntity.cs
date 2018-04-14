using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 导入Excel数据结果
    /// </summary>
    public class ImportResultEntity
    {
        /// <summary>
        /// 状态 1失败 2成功
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 导入的文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 导入的数据
        /// </summary>
        public object DataSource { get; set; }
    }
}
