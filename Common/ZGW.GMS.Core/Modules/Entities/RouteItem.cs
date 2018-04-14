using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ZGW.GMS.Core.Modules
{
    /// <summary>
    /// 路由项
    /// </summary>
    [Serializable]
    public class RouteItem
    {
        private XElement _routeItemXml;
        private Dictionary<string, string> attributes = new Dictionary<string, string>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="routeItemXml">路由节点</param>
        /// <param name="module">模块名称</param>
        public RouteItem(XElement routeItemXml,string module)
        {
            if (routeItemXml == null)
            {
                throw new ArgumentNullException("路由配置项不能为空!");
            }

            _routeItemXml = routeItemXml;
            Module = module;
            Initialize();
        }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="attrName">Attribute名称</param>
        /// <returns>属性值</returns>
        public string this[string attrName]
        {
            get
            {
                return attributes.ContainsKey(attrName) 
                    ? attributes[attrName] 
                    : String.Empty;
            }
        }

        /// <summary>
        /// 配置中的扩展属性
        /// </summary>
        public IDictionary<string, string> Attributes
        {
            get { return attributes; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Controller
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Module
        /// </summary>
        public string Module { get; set; }

        /// <summary>
        /// Namespaces
        /// </summary>
        public string[] Namespaces { get; set; }

        /// <summary>
        /// 初始化信息
        /// </summary>
        private void Initialize()
        {
            Name = XmlHelper.Attr(_routeItemXml, "name");
            Url = XmlHelper.Attr(_routeItemXml, "url");
            Controller = XmlHelper.Attr(_routeItemXml, "controller");
            Action = XmlHelper.Attr(_routeItemXml, "action");
            if (!_routeItemXml.Attr("module").IsNullOrEmpty())
            {
                Module = XmlHelper.Attr(_routeItemXml, "module");
            }            

            var strNamespaces = XmlHelper.Attr(_routeItemXml, "namespaces");
            if (!strNamespaces.IsNullOrEmpty())
            {
                Namespaces = strNamespaces.Split(',');
            }

            LoadDefaultValues();
        }

        /// <summary>
        /// 加载扩展的默认值
        /// </summary>
        private void LoadDefaultValues()
        {
            string[] ignoreAttributes = new string[] { "name", "url", "controller", "action", "module", "namespaces" };
            var items = _routeItemXml.Attributes().Where(m => !ignoreAttributes.Any(n => n.IsEqual(m.Name.LocalName, true))).ToList();
            foreach (var item in items)
            {
                attributes.Add(item.Name.LocalName, item.Value);
            }
        }
    }
}
