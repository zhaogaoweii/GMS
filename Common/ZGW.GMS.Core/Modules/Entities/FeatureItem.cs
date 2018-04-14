using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using ZGW.GMS.Core.Data;

namespace ZGW.GMS.Core.Modules
{
    /// <summary>
    /// 功能的组成项
    /// </summary>
    [Serializable]
    public class FeatureItem:DomainEntity
    {
        private XElement featureItemXml;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="featureItemXml">FeatureItem的Xml节点</param>
        public FeatureItem(XElement featureItemXml)
        {
            if (featureItemXml == null) throw new ArgumentNullException("featureItemXml is null!");

            this.featureItemXml = featureItemXml;
            Url =this.featureItemXml.Attr("url");
            Action = this.featureItemXml.Attr("action");
            Path = FeatureInfo.GetFullName(this.featureItemXml.Parent);
        }

        /// <summary>
        /// Url地址
        /// </summary>
        [XmlAttribute("url")]
        public string Url { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        [XmlAttribute("action")]
        public string Action { get; set; }

        /// <summary>
        /// 组织结构路径
        /// </summary>
        [XmlIgnore]
        public string Path { get; set; }
    }
}
