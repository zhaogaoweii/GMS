using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZGW.GMS.Core
{
    /// <summary>
    /// 组件注册
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class ComponentRegistryAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ComponentRegistryAttribute() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lifetime">组件生命周期</param>
        public ComponentRegistryAttribute(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="lifetime">组件生命周期</param>
        /// <param name="configKey">配置文件中的Key</param>
        /// <param name="configValue">配置文件中的Value</param>
        public ComponentRegistryAttribute(Lifetime lifetime, string configKey, string configValue)
        {
            Lifetime = lifetime;
            ConfigKey = configKey;
            ConfigValue = configValue;
        }

        /// <summary>
        /// 组件生命周期
        /// </summary>
        public Lifetime Lifetime { get; set; }

        /// <summary>
        /// Config配置中的Key
        /// </summary>
        public string ConfigKey { get; set; }

        /// <summary>
        /// Config配置中的Value
        /// </summary>
        public string ConfigValue { get; set; }

        /// <summary>
        /// 是否默认的注册类型
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否是注册的类型
        /// </summary>
        public bool IsRegistedType
        {
            get
            {
                return String.IsNullOrWhiteSpace(ConfigKey)
                    || ConfigureHelper.GetComponentKey(ConfigKey).IsEqual(ConfigValue, true)
                    || ConfigureHelper.GetComponentKey(ConfigKey).IsNullOrEmpty() && IsDefault;
            }
        }

        /// <summary>
        /// 检测是否可以注册
        /// </summary>
        /// <param name="type">实例类型</param>
        /// <param name="lifeTime">生命周期</param>
        /// <returns>是否可以注册</returns>
        public static bool ValidateType(Type type, Lifetime lifeTime)
        {
            bool result = false;
            if (!InvalidTypes.Instance.Contains(type)
                && type.IsDefined(typeof(ComponentRegistryAttribute), false))
            {
                var attr = type.GetCustomAttribute<ComponentRegistryAttribute>();
                result = attr.Lifetime == lifeTime && attr.IsRegistedType;
            }
            return result;
        }
    }
}
