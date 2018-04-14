using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using ZGW.GMS.Core.Data;

namespace ZGW.GMS.Core.Modules
{
    /// <summary>
    /// 模块信息
    /// </summary>
    [Serializable]
    [XmlRoot("module", Namespace = "http://www.ciic.com.cn/")]
    public class ModuleInfo : DomainEntity
    {
        private IList<FeatureInfo> features = new List<FeatureInfo>();
        private IList<RouteItem> routes = new List<RouteItem>();
        private XElement moduleXml;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">module.config文件地址</param>
        public ModuleInfo(string path)
        {
            if (!File.Exists(path)) throw new Exception("配置文件不存在!");
            XDocument doc = XDocument.Load(path);
            Initialize(doc.Root);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="module">模块的元素节点</param>
        public ModuleInfo(XElement module)
        {
            if (module == null) throw new ArgumentNullException("module is null!");
            Initialize(module);
        }

        /// <summary>
        /// 模块名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// 模块标签
        /// </summary>
        [XmlElement("label")]
        public string Label { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [XmlElement("order")]
        public int Order { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [XmlElement("description")]
        public string Description { get; set; }

        /// <summary>
        /// 功能
        /// </summary>
        [XmlArray("features")]
        [XmlArrayItem("feature")]
        public IList<FeatureInfo> Features
        {
            get { return features; }
            set { features = value; }
        }

        /// <summary>
        /// 路由信息
        /// </summary>
        [XmlArray("routes")]
        [XmlArrayItem("item")]
        public IList<RouteItem> Routes
        {
            get { return routes; }
            set { routes = value; }
        }

        private void Initialize(XElement module)
        {
            moduleXml = module;
            Name = moduleXml.ElementVal("name");
            Label = moduleXml.ElementVal("label");
            Order = moduleXml.ElementVal("order").ToInt();
            Description = moduleXml.ElementVal("description");
            Features = LoadFeatures();
            Routes = LoadRouteItems(Name);
        }

        private IList<FeatureInfo> LoadFeatures()
        {
            IList<FeatureInfo> result;

            var xeFeatures = moduleXml.Element("features");
            if (xeFeatures != null)
            {
                return xeFeatures.Elements("feature").Select(m => new FeatureInfo(m)).ToList();
            }
            else
            {
                result = new List<FeatureInfo>();
            }
            return result;
        }

        private IList<RouteItem> LoadRouteItems(string moduleName)
        {
            IList<RouteItem> result;

            var xeRoutes = moduleXml.Element("routes");
            if (xeRoutes != null)
            {
                return xeRoutes.Elements("item").Select(m => new RouteItem(m, moduleName)).ToList();
            }
            else
            {
                result = new List<RouteItem>();
            }
            return result;
        }
    }
}
