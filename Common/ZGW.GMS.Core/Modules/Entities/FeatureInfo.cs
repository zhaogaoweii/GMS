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
    /// 功能
    /// </summary>
    [Serializable]
    public class FeatureInfo : DomainEntity
    {
        private XElement _featureXml;
        private List<FeatureItem> items = new List<FeatureItem>();
        private List<FeatureInfo> children = new List<FeatureInfo>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="featureXml">Feature的Xml元素</param>
        public FeatureInfo(XElement featureXml)
        {
            if (featureXml == null) throw new ArgumentNullException("featureXml is null!");

            _featureXml = featureXml;
            Name = _featureXml.Attr("name");
            Label = _featureXml.Attr("label");
            Order = _featureXml.Attr("order").ToInt();
            Path = GetFullName(_featureXml);
            Children = _featureXml.Elements("feature").Select(m => new FeatureInfo(m)).ToList();
            Items = _featureXml.Elements("item").Select(m => new FeatureItem(m)).ToList();
        }

        /// <summary>
        /// 名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [XmlAttribute("label")]
        public string Label { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [XmlAttribute("order")]
        public int Order { get; set; }

        /// <summary>
        /// 组织结构路径
        /// </summary>
        [XmlIgnore]
        public string Path { get; set; }

        /// <summary>
        /// 子功能集合
        /// </summary>
        [XmlElement("feature")]
        public List<FeatureInfo> Children
        {
            get { return children; }
            set { children = value; }
        }

        /// <summary>
        /// 功能项
        /// </summary>
        [XmlElement("item")]
        public List<FeatureItem> Items
        {
            get { return items; }
            set { items = value; }
        }

        /// <summary>
        /// 取得Feature的完整名称
        /// </summary>
        /// <param name="feature">Feature的Xml元素节点</param>
        /// <returns>完整名称</returns>
        public static string GetFullName(XElement feature)
        {
            if (feature == null) throw new ArgumentNullException("feature is null!");
            string path = feature.Attr("name");
            if (feature.Parent.Name.LocalName == "feature")
            {
                path = GetFullName(feature.Parent) + "/" + path;
            }
            return path;
        }
    }
}
