using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// Xml操作的辅助类
    /// </summary>
    public static class XmlHelper
    {
        /// <summary>
        /// 将对象序化化为xml
        /// </summary>
        /// <param name="data">需要序列化的对象</param>
        /// <returns>生成的Xml字符串</returns>
        public static string SerializeToXml(object data)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());
            StringBuilder sbXml=new StringBuilder();

            using (StringWriter writer = new StringWriter(sbXml))
            {
                serializer.Serialize(writer, data);
            }

            return sbXml.ToString();
        }

        /// <summary>
        /// 将对象序列化成xml后保存到指定文件中
        /// </summary>
        /// <param name="data">需要序列化的对象</param>
        /// <param name="filePath">xml文件地址</param>
        public static void SerializeToXml(object data,string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(data.GetType());

            using (FileStream stream=File.Open(filePath,FileMode.Create))
            {
                serializer.Serialize(stream, data);
            }
        }

        /// <summary>
        /// 将DataTable转化为Xml
        /// </summary>
        /// <param name="dt">DataTable数据</param>
        /// <returns>Xml字符串</returns>
        public static string ConvertDataTableToXml(DataTable dt)
        {
            StringBuilder sbXml = new StringBuilder();
            using (StringWriter writer = new StringWriter(sbXml))
            {
                dt.WriteXml(writer);
            }

            return sbXml.ToString();
        }

        /// <summary>
        /// 将Dictionary转化为Xml
        /// </summary>
        /// <param name="data">字符值</param>
        /// <returns>生成的Xml字符串</returns>
        public static string ConverDictionaryToXml(Dictionary<string, object> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "utf-8", "yes"));
            XElement root = new XElement("root");
            doc.Add(root);

            foreach (var kvp in data)
            {
                XElement xe = new XElement(kvp.Key, kvp.Value);
                root.Add(xe);
            }

            StringBuilder sbXml = new StringBuilder();
            using (StringWriter writer = new StringWriter(sbXml))
            {
                doc.Save(writer);
            }

            return sbXml.ToString();
        }

        /// <summary>
        /// 从XElement中获取指定的Attribute的值
        /// </summary>
        /// <param name="xe">元素节点</param>
        /// <param name="attrName">属性名称</param>
        /// <returns>属性值</returns>
        public static string Attr(this XElement xe, string attrName)
        {
            var attr=xe.Attribute(attrName);
            return attr != null ? attr.Value : null;
        }

        /// <summary>
        /// 取得子级Element的值
        /// </summary>
        /// <param name="parentElement">父节点</param>
        /// <param name="name">取值节名</param>
        /// <returns>节点下面的值</returns>
        public static string ElementVal(this XElement parentElement, string name)
        {
            var element=parentElement.Element(name);
            return element != null ? element.Value : null;
        }
    }
}
