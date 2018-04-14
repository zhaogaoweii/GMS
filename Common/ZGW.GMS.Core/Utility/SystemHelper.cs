using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public static class SystemHelper
    {
        /// <summary>
        /// 加载应用程序的程序集
        /// </summary>
        /// <returns>系统中自定义的程序集</returns>
        public static Assembly[] LoadAppAssemblies()
        {
            string binFolder = AppDomain.CurrentDomain.RelativeSearchPath;
          
            if (String.IsNullOrEmpty(binFolder))
                binFolder = Environment.CurrentDirectory;
            DirectoryInfo binInfo = new DirectoryInfo(binFolder);
            string[] files = binInfo.GetFiles("ZGW.GMS.*.dll").Select(m => m.Name).ToArray();

        
            return files.Select(m => Assembly.Load(Path.GetFileNameWithoutExtension(m)))
                .ToArray();
        }

        /// <summary>
        /// 将字符串转化为Int值
        /// </summary>
        /// <param name="strVal">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>整数</returns>
        public static int ToInt(this string strVal, int defaultValue = 0)
        {
            int result;
            return Int32.TryParse(strVal, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 将字符串转化为Int64
        /// </summary>
        /// <param name="strVal">目标字符串</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>Int64值</returns>
        public static Int64 ToInt64(this string strVal, Int64 defaultValue = 0)
        {
            Int64 result;
            return Int64.TryParse(strVal, out result) ? result : defaultValue;
        }

        /// <summary>
        /// 对枚举器的每个元素执行指定的操作
        /// </summary>
        /// <typeparam name="T">枚举器类型参数</typeparam>
        /// <param name="source">枚举器</param>
        /// <param name="action">要对枚举器的每个元素执行的委托</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source.IsNullOrEmpty() || action == null)
            {
                return;
            }
            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// 按指定条件删除数据项
        /// </summary>
        /// <typeparam name="T">数据项类型</typeparam>
        /// <param name="source">操作的数据源</param>
        /// <param name="predicate">移除的条件</param>
        /// <returns>移除后的数据项</returns>
        public static IList<T> Remove<T>(this IList<T> source, Func<T, bool> predicate = null)
        {
            if (source != null)
            {
                var removeItems = predicate != null
                                ? source.Where(predicate).ToArray()
                                : source.ToArray();

                foreach (var item in removeItems)
                {
                    source.Remove(item);
                }
            }
            return source;
        }

        /// <summary>
        /// 指示指定的枚举器是null还是没有任何元素
        /// </summary>
        /// <typeparam name="T">枚举器类型参数</typeparam>
        /// <param name="source">要测试的枚举器</param>
        /// <returns>true:枚举器是null或者没有任何元素 false:枚举器不为null并且包含至少一个元素</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// 获取类型的CustomAttribute
        /// </summary>
        /// <typeparam name="T">要获取的Attribute类型</typeparam>
        /// <param name="type">目标类型</param>
        /// <param name="inherit">是否采用继承方式查找</param>
        /// <returns>当前类型上的T类型的Attribute实例</returns>
        public static T GetCustomAttribute<T>(this Type type, bool inherit = false) where T : Attribute
        {
            if (type.IsDefined(typeof(T)))
            {
                return (T)type.GetCustomAttributes(typeof(T), inherit)[0];
            }
            return default(T);
        }

        /// <summary>
        /// 从文件中读取所有内容
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>二进制数据</returns>
        public static byte[] ReadFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(filePath);
            }

            return File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// 取得上传文件的内容
        /// </summary>
        /// <param name="rootPath">根路径</param>
        /// <param name="relativePath">文件的相对路径</param>
        /// <returns>二进制数据</returns>
        public static byte[] ReadFile(string rootPath, string relativePath)
        {
            string fullPath = Path.Combine(rootPath, relativePath);
            return ReadFile(fullPath);
        }

        #region 序列化与反序列化
        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns>返回二进制</returns>
        public static byte[] SerializeModel(Object obj)
        {
            if (obj != null)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                byte[] b;
                binaryFormatter.Serialize(ms, obj);
                ms.Position = 0;
                b = new Byte[ms.Length];
                ms.Read(b, 0, b.Length);
                ms.Close();
                return b;
            }
            else
                return new byte[0];
        }

        /// <summary>
        /// 反序列化对象
        /// </summary>
        /// <param name="b">要反序列化的二进制</param>
        /// <returns>返回对象</returns>
        public static object DeserializeModel(byte[] b, object SampleModel)
        {
            if (b == null || b.Length == 0)
                return SampleModel;
            else
            {
                object result = new object();
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                MemoryStream ms = new MemoryStream();
                try
                {
                    ms.Write(b, 0, b.Length);
                    ms.Position = 0;
                    result = binaryFormatter.Deserialize(ms);
                    ms.Close();
                }
                catch { }
                return result;
            }
        }

        /// <summary>
        /// 转化为半角字符串
        /// </summary>
        /// <param name="input">要转化的字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToSBC(this string input)//single byte charactor
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)//全角空格为12288，半角空格为32
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)//其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转化为全角字符串
        /// </summary>
        /// <param name="input">要转化的字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToDBC(this string input)//double byte charactor 
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        #endregion
    }
}
