using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LumbarRobot.Enums
{
    public class EnumHelper
    {
        /// <summary>
        ///获取枚举中所有枚举项的名称和值
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, int>> GetAllEnumItem(Type enumType)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            var fields = enumType.GetFields();

            var instance = enumType.Assembly.CreateInstance(enumType.FullName);

            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    if (attribute is EnumItemName)
                    {
                        KeyValuePair<string, int> kvp = new KeyValuePair<string, int>((attribute as EnumItemName).Name, Convert.ToInt32(field.GetValue(instance)));
                        result.Add(kvp);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 获取枚举名称
        /// </summary>
        /// <param name="value"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string GetEnumItemName(int value, Type enumType)
        {
            var fields = enumType.GetFields();

            var instance = enumType.Assembly.CreateInstance(enumType.FullName);

            foreach (var field in fields)
            {
                if (Convert.ToInt32(field.GetValue(instance)) == value)
                {
                    var attributes = field.GetCustomAttributes(false);
                    foreach (var attribute in attributes)
                    {
                        if (attribute is EnumItemName)
                        {
                            return (attribute as EnumItemName).Name;
                        }
                    }
                }
            }

            return "未知";
        }

        public static int GetEnumItemValueByName(string name, Type enumType)
        {
            var fields = enumType.GetFields();

            var instance = enumType.Assembly.CreateInstance(enumType.FullName);

            foreach (var field in fields)
            {
                if (field.Name.Trim() == name.Trim())
                {
                    return Convert.ToInt32(field.GetValue(instance));
                }
            }

            return -100;
        }

        public static string GetEnumValueStr(object enumValue)
        {
            if (enumValue.GetType().IsEnum)
            {
                return ((int)enumValue).ToString();
            }
            else
            {
                throw new Exception("不是枚举类型！");
            }
        }

        public static string GetEnumValueName(object enumValue)
        {
            if (enumValue.GetType().IsEnum)
            {
                return enumValue.ToString();
            }
            else
            {
                throw new Exception("不是枚举类型！");
            }
        }
    }
}
