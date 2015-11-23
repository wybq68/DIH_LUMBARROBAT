using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Windows;

namespace LumbarRobot.Common
{
    /// <summary>
    /// 转化类
    /// </summary>
    public class Objtoobj
    {
        /// <summary>
        /// 短时间类型转化为整型
        /// </summary>
        /// <param name="sTime"></param>
        /// <returns></returns>
        public static int shortTimeToInt(string sTime)
        {
            if (string.IsNullOrEmpty(sTime))
            {
                return 0;
            }
            else
            {
                string[] data = sTime.Split(':');
                string DD = data[0];
                string ss = data[1];

                int nRet = 0;
                nRet = Convert.ToInt32(ss);
                nRet = 60 * Convert.ToInt32(DD) + nRet;
                return nRet;
            }
        }

        /// <summary>
        /// 整型转化为短事件类型
        /// </summary>
        /// <param name="ntime"></param>
        /// <returns></returns>
        public static string IntToShortTime(int ntime)
        {
            int ns = ntime % 60;
            int nm = ntime / 60;

            return string.Format("{0:00}:{1:00}", nm, ns);
        }

        /// <summary>
        /// 转化为时间
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime objectToDateTime(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj.ToString());
            }
            catch (Exception)
            {
                return System.DateTime.Now;
            }
        }

        /// <summary>
        /// object转化为int32
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int32 objectToInt(object value)
        {
            Int32 nret = 0;
            if (value == null)
            {
                return nret;
            }
            try
            {
                nret = Convert.ToInt32(value);
            }
            catch
            {
                nret = 0;
            }
            return nret;
        }

        /// <summary>
        /// object 转化为浮点型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double objectToDouble(object value)
        {
            double nret = 0;
            if (value == null)
            {
                return nret;
            }
            try
            {
                nret = Convert.ToDouble(value);
            }
            catch
            {
                nret = 0;
            }
            return nret;
        }

        public static Brush StrToBrush(string sValue)
        {
            if (!string.IsNullOrEmpty(sValue))
                return (SolidColorBrush)new BrushConverter().ConvertFromString(sValue);
            else
                return Brushes.Black;
        }

        /// <summary>
        /// 字符数组转化成字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string bytesToStr(byte[] value)
        {
            return System.Text.Encoding.Default.GetString(value);
        }

        /// <summary>
        /// 字符串转化为字符数组
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] strToBytes(string value)
        {
            return System.Text.Encoding.Default.GetBytes(value);
        }


        /// <summary> 
        /// 转换为一个DataTable
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(IEnumerable<T> value) where T : class
        {
            if (value == null)
            {
                return null;
            }
            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in value)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// 实体类转化为集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DataTable InfoToDataTable<T>(T value) where T : class
        {
            List<T> vlist = new List<T>();
            vlist.Add(value);
            return ListToDataTable<T>(vlist);
        }


        public static object DataTableToInfo<T>(DataTable dt) where T : class,new()
        {
            List<T> objlist = DataTableToList<T>(dt);

            if (objlist != null && objlist.Count > 0)
            {
                return objlist[0];
            }
            else { return new T(); }
        }
        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : class,new()
        {
            if (dt == null)
            {
                return null;
            }
            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例 反射的入口
            Type t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            List<T> oblist = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                T ob = new T();
                //找到对应的数据 并赋值
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value && (p.CanWrite)) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }

        /// <summary>
        /// 用sSub分割字符串为List
        /// </summary>
        /// <param name="sValue">要分割的字符串</param>
        /// <param name="sSub">分隔符</param>
        /// <returns></returns>
        public static List<string> strToList(string sValue, string sSub)
        {
            List<string> vlist = new List<string>();
            while (sValue.IndexOf(sSub) >= 0)
            {
                vlist.Add(sValue.Substring(0, sValue.IndexOf(sSub)));
                sValue = sValue.Substring(sValue.IndexOf(sSub) + sSub.Length);
            }
            if (!string.IsNullOrEmpty(sValue))
            {
                vlist.Add(sValue);
            }
            return vlist;
        }

        public static string ListToString<T>(List<T> vlist)
        {
            string strRet = string.Empty;
            Type vtype = typeof(T);
            foreach (var vitem in vlist)
            {
                foreach (var item in vtype.GetFields())
                {
                    object o = item.GetValue(vitem);
                    if (o != null)
                    {
                        strRet += o.ToString();
                    }
                    strRet += "|";
                    //strRet += item[vitem].ToString() + "|";
                }
                if (strRet.Length > 0)
                {
                    strRet = strRet.Substring(0, strRet.Length - 1);
                }
                strRet += ",";
            }
            if (strRet.Length > 0)
            {
                strRet = strRet.Substring(0, strRet.Length - 1);
            }
            return strRet;
        }

        public static Thickness strToThicknewss(string sValue)
        {
            Thickness vness = new Thickness();
            List<string> vstrings = strToList(sValue, ",");
            if (vstrings.Count == 1)
            {
                vness = new Thickness(Objtoobj.objectToDouble(vstrings[0]));
            }
            else if (vstrings.Count == 2)
            {
                vness = new Thickness(Objtoobj.objectToDouble(vstrings[0]), Objtoobj.objectToDouble(vstrings[1]), 0, 0);
            }
            else if (vstrings.Count == 4)
            {
                vness = new Thickness(Objtoobj.objectToDouble(vstrings[0]), Objtoobj.objectToDouble(vstrings[1]),
                    Objtoobj.objectToDouble(vstrings[2]), Objtoobj.objectToDouble(vstrings[3]));
            }
            return vness;
        }
    }
}
