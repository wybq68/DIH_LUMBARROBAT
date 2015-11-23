
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Context;
using NHibernate.Linq;
using System.Web;
using NHibernate.Engine;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace LumbarRobot.DAL
{
    public class MySession
    {

        #region 变量

        private static Configuration MyConfiguration { get; set; }

        private static ISessionFactory MySessionFactory { get; set; }

        private static ISession session = null;

        public static ISession Session
        {
            get { return MySession.session; }
            set { MySession.session = value; }
        }
        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cfg"></param>
        public static void Init(NHibernate.Cfg.Configuration cfg)
        {
            MyConfiguration = cfg;
            cfg.AddAssembly("LumbarRobot.Maps");
            MySessionFactory = MySession.MyConfiguration.BuildSessionFactory();
            session = MySessionFactory.OpenSession();
        }
        #endregion

        #region 释放资源
        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Dispose()
        {
            session.Close();
            MySessionFactory.Close();
        }
        #endregion

        #region 释放上下文Session
        /// <summary>
        /// 释放上下文Session
        /// </summary>
        public static void DisposeContextSession()
        {

            if (session != null)
            {
                if (session.Transaction.IsActive)
                {
                    session.Transaction.Rollback();
                }

                if (session != null)
                {
                    if (session.IsOpen)
                    {
                        session.Close();
                    }
                }
            }
        }
        #endregion

        #region 查询
        
       
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public  static IQueryable<T> Query<T>()
        {
            return Session.Query<T>();
        }
        #endregion
    }
}
