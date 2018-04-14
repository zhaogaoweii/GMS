using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZGW.GMS.Core.Utility
{
    /// <summary>
    /// DataTableHelper
    /// 2017-3-21 WangYuanbo
    /// </summary>
    public static class DataTableHelper
    {
        //保存反射的类型的属性
        private static Dictionary<string, PropertyInfo[]> _cachedPropsList = new Dictionary<string, PropertyInfo[]>();
        private static object _root = new object();

        /// <summary>
        /// 将 DataTable 转换为 List
        /// </summary>
        /// <typeparam name="T">要转换为的类型</typeparam>
        /// <param name="dt">The dt.</param>
        /// <param name="getModelPropName">通过列名获取对应实体属性名的委托</param>
        /// <returns></returns>
        public static IEnumerable<T> ToList<T>(DataTable dt, Func<string, string> getModelPropName) where T : new()
        {
            var type = typeof(T);
            T model = new T();
            var props = GetProps<T>(model, null);

            foreach (DataRow row in dt.Select())
            {
                var item = new T();
                for (int i = 0; i < row.Table.Columns.Count; i++)
                {
                    var cellVal = row[i];
                    var cellName = row.Table.Columns[i].ColumnName;

                    //该委托是为了找到对应的实体属性, 如: 数据库某表有字段[USER_NAME] 
                    //实体对应的字段为[UserName], 则委托为[ x=>x.Replace("_","") ]
                    //以下已忽略大小写
                    var modelPropName = getModelPropName(cellName);
                    foreach (var p in props)
                    {
                        if (p.CanRead && p.CanWrite)
                        {
                            if (modelPropName.ToLower() == p.Name.ToLower() && cellVal != null && cellVal != DBNull.Value)
                            {
                                p.SetValue(item, ParseType(p, cellVal));
                                break;
                            }
                        }
                    }
                }

                yield return item;
            }
        }

        private static object ParseType(PropertyInfo p, object val)
        {
            //该委托用于校验: p 是值类型 并且 val 不为 null[如 val = {} ] 并且 val.ToString() 等于 ""
            //若以上条件成立, 则不能对该值进行转换, 否则下一步的 setValue 方法会异常, 
            //对于该种情况, 直接返回对应值类型的默认值
            Func<PropertyInfo, object, bool> check =
                (prop, obj) =>
                prop.PropertyType.IsValueType && obj != null && string.IsNullOrEmpty(obj.ToString());

            if (val != null)
            {
                var t = p.PropertyType;
                var typeName = t.FullName;
                if (t.FullName.IndexOf("System.Nullable") == 0)
                {
                    if (string.IsNullOrEmpty(val.ToString()))
                    {
                        //对于可空类型[原始类型肯定是值类型], 直接返回 null
                        return null;
                    }
                    //可空类型对应的是 Nullable<T>,  这里 GenericTypeArguments 属性返回的即是 Nullable<T> 的泛型类型数组 
                    var ts = t.GenericTypeArguments;
                    if (ts.Length > 0)
                    {
                        typeName = ts[0].FullName;
                    }
                }
                var valStr = (val ?? "").ToString();
                switch (typeName.ToLower())
                {
                    case "system.string":
                        return valStr;
                    case "system.int32":
                        if (check(p, val))
                        {
                            return default(int);
                        }
                        int i;
                        if (int.TryParse(valStr, out i))
                        {
                            return i;
                        }
                        return default(int);
                    case "system.int64":
                        if (check(p, val))
                        {
                            return default(Int64);
                        }
                        Int64 j;
                        if (long.TryParse(valStr, out j))
                        {
                            return j;
                        }
                        return default(Int64);
                    case "system.datetime":
                        if (check(p, val))
                        {
                            return default(DateTime);
                        }
                        DateTime dateTime;
                        if (DateTime.TryParse(valStr, out dateTime))
                        {
                            return dateTime;
                        }
                        return default(DateTime);
                    case "system.double":
                        if (check(p, val))
                        {
                            return default(double);
                        }
                        double d;
                        if (double.TryParse(valStr, out d))
                        {
                            return d;
                        }
                        return default(double);
                    case "system.boolean":
                        if (check(p, val))
                        {
                            return default(bool);
                        }
                        bool b;
                        if (bool.TryParse(valStr, out b))
                        {
                            return b;
                        }
                        else
                        {
                            int _i;
                            if (int.TryParse(valStr,out _i))
                            {
                                if (_i > 0)
                                    return true;
                                else
                                    return false;
                            }
                            return false;
                        }
                    default:
                        return val;
                }
            }
            return val;
        }

        private static PropertyInfo[] GetProps<T>(T model, BindingFlags? bindingAttr)
        {
            var key = model.GetType().FullName;
            if (_cachedPropsList.ContainsKey(key))
            {
                return _cachedPropsList[key];
            }
            else
            {
                var props = typeof(T).
                    GetProperties(bindingAttr ?? (BindingFlags.Public | BindingFlags.Instance));

                lock (_root)
                {
                    if (!_cachedPropsList.ContainsKey(key))
                    {
                        _cachedPropsList.Add(key, props);
                    }
                }
                return props;
            }
        }
    }
}
