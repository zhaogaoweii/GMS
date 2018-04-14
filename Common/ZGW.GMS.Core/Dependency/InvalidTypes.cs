using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// DI容器中的无效类型
    /// </summary>
    internal class InvalidTypes
    {
        private static readonly InvalidTypes invalidTypes = new InvalidTypes();
        private readonly List<Type> typeList = new List<Type>();

        private InvalidTypes() { }

        /// <summary>
        /// 单例接口
        /// </summary>
        public static InvalidTypes Instance
        {
            get { return invalidTypes; }
        }

        /// <summary>
        /// 注册无效类型
        /// </summary>
        /// <param name="type"></param>
        public void Register(Type type)
        {
            if (!Contains(type))
            {
                typeList.Add(type);
            }
        }

        /// <summary>
        /// 注册无效类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Register<T>()
        {
            Register(typeof(T));
        }

        /// <summary>
        /// 判断指定类型是否是无效类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool Contains(Type type)
        {
            return typeList.Contains(type);
        }

        /// <summary>
        /// 判断指定类型是否是无效类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool Contains<T>()
        {
            return Contains(typeof(T));
        }

    }
}
