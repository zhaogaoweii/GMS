using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ZGW.GMS.Core.Data
{
    ///// <summary>
    ///// NHibernate的ISessionFactory的数据提供者
    ///// </summary>
    //public class NHSessionFactoryProvider
    //{
    //    private static NHSessionFactoryProvider sessionFactoryProvider = new NHSessionFactoryProvider();

    //    private Dictionary<DBCatetory, ISessionFactory> sessionFactories;

    //    private NHSessionFactoryProvider() { }

    //    /// <summary>
    //    /// 单例实体
    //    /// </summary>
    //    public static NHSessionFactoryProvider Instance
    //    {
    //        get { return sessionFactoryProvider; }
    //    }

    //    /// <summary>
    //    /// 获取当前的根据category获取ISessionFactory
    //    /// </summary>
    //    /// <param name="dbCategory">数据库分类</param>
    //    /// <param name="assemblyLoader">Assemly 加载回调</param>
    //    /// <returns>ISessionFactory实例</returns>
    //    public ISessionFactory GetSessionFactory(Func<Assembly[]> assemblyLoader,DBCatetory dbCategory=DBCatetory.Production)
    //    {
    //        if (sessionFactories == null)
    //        {
    //            sessionFactories = CreateSessionFactories(assemblyLoader);
    //        }
    //        if (!sessionFactories.ContainsKey(dbCategory))
    //        {
    //            throw new InvalidOperationException(String.Format("NHibernate:指定的数据库类别{0}不存在", dbCategory));
    //        }
    //        return sessionFactories[dbCategory];
    //    }

    //    /// <summary>
    //    /// 获取当前的根据category获取ISessionFactory
    //    /// </summary>
    //    /// <param name="dbCategory">数据库分类</param>
    //    /// <returns>ISessionFactory实例</returns>
    //    public ISessionFactory GetSessionFactory(DBCatetory dbCategory = DBCatetory.Production)
    //    {
    //        return GetSessionFactory(SystemHelper.LoadAppAssemblies, dbCategory);
    //    }

    //    /// <summary>
    //    /// 创建Session工厂
    //    /// </summary>
    //    /// <returns></returns>
    //    private Dictionary<DBCatetory, ISessionFactory> CreateSessionFactories(Func<Assembly[]> assemblyLoader)
    //    {
    //        Dictionary<DBCatetory, ISessionFactory> result = new Dictionary<DBCatetory, ISessionFactory>();
    //        DirectoryInfo dir = new DirectoryInfo(AppContext.ConfigsPhysicalPath);
    //        var files = dir.GetFiles("nhibernate*.config");
    //        foreach (FileInfo fi in files)
    //        {
    //            NHibernate.Cfg.Configuration config = new NHibernate.Cfg.Configuration().Configure(fi.FullName);
    //            RegisterMappingAssemblies(config, assemblyLoader);

    //            ISessionFactory sessionFactory = config.BuildSessionFactory();
    //            DBCatetory dbCategory = GetDBCategory(fi.Name);
    //            result.Add(dbCategory, sessionFactory);
    //        }
    //        return result;
    //    }

    //    /// <summary>
    //    /// 注册Hibernate的映射文件
    //    /// </summary>
    //    /// <param name="config"></param>
    //    private void RegisterMappingAssemblies(NHibernate.Cfg.Configuration config, Func<Assembly[]> assemblyLoader)
    //    {
    //        var mappingAssemblies = assemblyLoader();

    //        foreach (var assembly in mappingAssemblies)
    //        {
    //            config.AddAssembly(assembly);
    //        }
    //    }

    //    private DBCatetory GetDBCategory(string fileName)
    //    {
    //        return fileName.ToLower().Contains(".history.")
    //            ? DBCatetory.History
    //            : DBCatetory.Production;
    //    }
    //}
}
