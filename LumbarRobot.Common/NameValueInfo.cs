using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Common
{

    public class NameValueInfo
    {
        private string _sName;
        /// <summary>
        /// 名称
        /// </summary>
        public string sName
        {
            get { return _sName; }
            set
            {
                _sName = value;
                //this.RaisePropertyChanged("sName");
            }
        }


        private double _nValue;
        /// <summary>
        /// 值
        /// </summary>
        public double nValue
        {
            get { return _nValue; }
            set
            {
                _nValue = value;
                //this.RaisePropertyChanged("nValue");
            }
        }

        private string _sValue;
        /// <summary>
        /// 字符串类型值
        /// </summary>
        public string sValue
        {
            get { return _sValue; }
            set { _sValue = value; }
        }


        public static List<NameValueInfo> strToNameValueList(string strvalue)
        {
            List<NameValueInfo> vnvList = new List<NameValueInfo>();
            if (!string.IsNullOrEmpty(strvalue))
            {
                List<string> slist = new List<string>(strvalue.Split(';'));
                foreach (var item in slist)
                {
                    NameValueInfo nv = new NameValueInfo();
                    nv.sName = item.Substring(0, item.IndexOf("|")).Trim();
                    string stemp = item.Substring(item.IndexOf("|") + 1);
                    if (stemp.IndexOf("|") >= 0)
                    {
                        nv.sValue = stemp.Substring(0, stemp.IndexOf("|"));
                    }
                    else
                    {
                        nv.sValue = item.Substring(item.IndexOf("|") + 1);
                    }
                    vnvList.Add(nv);
                }
            }
            return vnvList;
        }

        public static string NameValueListToStr(List<NameValueInfo> vlist)
        {
            string strRet = string.Empty;
            if (vlist == null) { return strRet; }
            foreach (var item in vlist)
            {
                strRet += item.sName + "|" + item.sValue + ";";
            }
            if (!string.IsNullOrEmpty(strRet))
            {
                strRet = strRet.Substring(0, strRet.Length - 1);
            }
            return strRet;
        }
    }
}
