using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tasker.Infrastructure
{
    /// <summary>
    /// 基础类型扩展
    /// </summary>
    public static class BaseExtension
    {
        #region 常量定义
        private const long _NULL_LONG = long.MinValue;
        private const int _NULL_INT = int.MinValue;
        private const decimal _NULL_DECIMAL = decimal.MinValue;
        private const double _NULL_DOUBLE = double.MinValue;
        #endregion

        #region 类型转换
        /// <summary>
        /// 说明：如果空值返回0
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T To<T>(this object value)
        {
            return To<T>(value, "ZERO");
        }

        /// <summary>
        /// 说明：如果空值返回空值自定义类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T To<T>(this object value, string NULLFlag)
        {
            long NULL_LONG = 0;
            Int32 NULL_INT = 0;
            decimal NULL_DECIMAL = 0;
            double NULL_DOUBLE = 0;
            float NULL_FLOAT = 0;

            if (NULLFlag == "MIN")
            {
                NULL_LONG = _NULL_LONG;
                NULL_INT = _NULL_INT;
                NULL_DECIMAL = _NULL_DECIMAL;
                NULL_DOUBLE = _NULL_DOUBLE;
            }
            else
            {
                NULL_LONG = 0;
                NULL_INT = 0;
                NULL_DECIMAL = 0;
                NULL_DOUBLE = 0;
            }

            if (value.IsNull())
            {
                //返回空值
                object obj = null;
                if (typeof(T) == typeof(string))
                {
                    obj = string.Empty;
                }
                else if (typeof(T) == typeof(long))
                {
                    obj = NULL_LONG;
                }
                else if (typeof(T) == typeof(Int32))
                {
                    obj = NULL_INT;
                }
                else if (typeof(T) == typeof(double))
                {
                    obj = NULL_DOUBLE;
                }
                else if (typeof(T) == typeof(decimal))
                {
                    obj = NULL_DECIMAL;
                }
                else if (typeof(T) == typeof(DateTime))
                {
                    obj = DateTime.MinValue;
                }
                else if (typeof(T) == typeof(float))
                {
                    obj = NULL_FLOAT;
                }
                else if (typeof(T) == typeof(int?) ||
                        typeof(T) == typeof(long?) ||
                        typeof(T) == typeof(double?) ||
                        typeof(T) == typeof(DateTime?) ||
                        typeof(T) == typeof(float?))
                {
                    obj = null;
                }
                else
                {
                    throw new ApplicationException("未知的数据类型[" + value.GetType().Name + "]");
                }
                return (T)obj;
            }
            else
            {
                return ConvertType<T>(value);
            }
        }

        /// <summary>
        /// 根据字符串转类型
        /// </summary>
        /// <param name="value">输入值</param>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public static dynamic To(this object value, string type = "string")
        {
            switch (type.ToUpper())
            {
                case "INT":
                    return value.To<int>();
                case "INT32":
                    return value.To<Int32>();
                case "INT64":
                    return value.To<Int64>();
                case "DOUBLE":
                    return value.To<double>();
                case "FLOAT":
                    return value.To<float>();
                case "STRING":
                    return value.To<string>();
                case "DATE":
                    return value.To<DateTime>();
                default:
                    return value.To<string>();
            }
        }

        /// <summary>
        /// 带空值判断的类型转换
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="value">输入值</param>
        /// <param name="value2">为空时的输出值</param>
        /// <returns></returns>
        public static T NVL<T>(this object value, T value2)
        {
            if (value.IsNull())
            {
                return value2;
            }
            else
            {
                return ConvertType<T>(value);
            }
        }

        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        private static T ConvertType<T>(object value)
        {
            object obj;

            try
            {
                if (typeof(T) == typeof(string))
                {
                    obj = Convert.ToString(value);
                }
                else if (typeof(T) == typeof(long) || typeof(T) == typeof(long?))
                {
                    obj = Convert.ToInt64(value);
                }
                else if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
                {
                    obj = Convert.ToInt32(value);
                }
                else if (typeof(T) == typeof(decimal) || typeof(T) == typeof(decimal?))
                {
                    obj = Convert.ToDecimal(value);
                }
                else if (typeof(T) == typeof(double) || typeof(T) == typeof(double?))
                {
                    obj = Convert.ToDouble(value);
                }
                else if (typeof(T) == typeof(float) || typeof(T) == typeof(float?))
                {
                    obj = Convert.ToSingle(value);
                }
                else if (typeof(T) == typeof(DateTime) || typeof(T) == typeof(DateTime?))
                {
                    obj = Convert.ToDateTime(value);
                }
                else
                {
                    throw new ApplicationException("未知数据类型[" + typeof(T).Name + "]");
                }
            }
            catch
            {
                throw new ApplicationException("数据类型[" + value.GetType().Name + "]转换到数据类型[" + typeof(T).Name + "]出错!");
            }
            return (T)obj;
        }
        #endregion

        #region 空值判断

        public static bool IsNotNull(this object value)
        {
            return !IsNull(value);
        }

        public static bool IsNull(this object value)
        {
            if (Convert.IsDBNull(value)) return true;
            if (value == null) return true;

            if (value is string)
            {
                if ((value as string).IsNull()) return true;
            }
            else if (value is int)
            {
                if ((int)value == int.MinValue) return true;
            }
            else if (value is Int32)
            {
                if ((Int32)value == Int32.MinValue) return true;
            }
            else if (value is Int64)
            {
                if ((Int64)value == Int64.MinValue) return true;
            }
            else if (value is long)
            {
                if (((long)value).IsNull()) return true;
            }
            else if (value is decimal)
            {
                if (((decimal)value).IsNull()) return true;
            }
            else if (value is double)
            {
                if (((double)value).IsNull()) return true;
            }
            else if (value is DateTime)
            {
                if (((DateTime)value).IsNull()) return true;
            }
            else
            {
                //未知类型  -->异常
                throw new ApplicationException("未知数据类型[" + value.GetType().Name + "]");
            }

            return false;
        }

        public static bool IsNull(this string value)
        {
            if (string.IsNullOrEmpty(value) == true) return true;
            return false;
        }

        public static int ToInt(this Nullable<int> value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                return (int)value;
            }
        }

        public static bool IsNull(this long value)
        {
            if (value == _NULL_LONG) return true;
            return false;
        }

        public static bool IsNull(this decimal value)
        {
            if (value == _NULL_DECIMAL) return true;
            return false;
        }

        public static bool IsNull(this double value)
        {
            if (value == _NULL_DOUBLE) return true;
            return false;
        }

        public static bool IsNull(this DateTime value)
        {
            if (value == DateTime.MinValue) return true;
            return false;
        }

        /// <summary>
        /// 值可能为NULL的转短时间
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateStringNullAble(this DateTime value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.ToShortDateString();
            }
        }

        #endregion

        #region 四舍五入
        /// <summary>
        /// Double值类型的四舍五入运算(利用ToString)
        /// 用法：a = MathMethod.DoubleRoundFormat(a, "##################.##");
        /// </summary>
        /// <param name="value">计算前的值</param>
        /// <param name="formatString">得到的值的格式：要保留的小数位等</param>
        /// <returns>计算后的值</returns>
        public static double DoubleRound(this double value, string formatString)
        {
            if (string.IsNullOrEmpty(formatString))
            {
                formatString = "##################.##";
            }
            string rtnValue;
            rtnValue = value.ToString(formatString);
            //if (string.IsNullOrEmpty(rtnValue))
            if (rtnValue.IsNull())
            {
                rtnValue = "0";
            };
            return Convert.ToDouble(rtnValue);
        }

        /// <summary>
        /// Double值类型的四舍五入运算(利用小数位)
        /// </summary>
        /// <param name="value">计算前的值</param>
        /// <param name="decimalPlace">几位小数</param>
        /// <returns>计算后的值</returns>
        public static double DoubleRound(this double value, int decimalPlace)
        {
            string formatString = "#.";
            for (int i = 0; i < decimalPlace; i++)
            {
                formatString += "#";
            }
            string rtnValue;
            rtnValue = value.ToString(formatString);
            if (rtnValue.IsNull())
            {
                rtnValue = "0";
            }
            return Convert.ToDouble(rtnValue);
        }

        /// <summary>
        /// Double值类型的四舍五入运算(2位小数)，一般用于统一金额运算(利用ToString)
        /// 用法：a = MathMethod.DoubleRoundFormat(a);
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns> 
        public static double DoubleRound(this double value)
        {
            return DoubleRound(value, "##################.##");
        }

        /// <summary>
        /// 计算从datatable中取得DOUBLE数据的四舍五入
        /// </summary>
        /// <param name="value">计算前的值</param>
        /// <param name="formatString">得到的值的格式：要保留的小数位等</param>
        /// <returns>计算后的值</returns>
        public static double DoubleRound(this object value, string formatString)
        {
            double rtn;
            rtn = value.To<double>();

            return DoubleRound(rtn, formatString);
        }

        /// <summary>
        /// 计算从datatable中取得DOUBLE数据的四舍五入
        /// </summary>
        /// <param name="value">计算前的值</param>
        /// <param name="decimalPlace">几位小数</param>
        /// <returns>计算后的值</returns>
        public static double DoubleRound(this object value, int decimalPlace)
        {
            double rtn;
            rtn = value.To<double>();
            return DoubleRound(rtn, decimalPlace);
        }

        /// <summary>
        /// 计算从datatable中取得DOUBLE数据的四舍五入
        /// </summary>
        /// <param name="value">计算前的值</param>
        /// <returns>计算后的值</returns>
        public static double DoubleRound(this object value)
        {
            double rtn;
            rtn = value.To<double>();

            return DoubleRound(rtn, "##################.##");
        }
        #endregion

        #region 正则判断
        /// <summary>
        /// 判断字符串是否与指定正则表达式匹配
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <param name="regularExp">正则表达式</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsMatch(string value, string regularExp)
        {
            return Regex.IsMatch(value, regularExp);
        }

        /// <summary>
        /// 验证非负整数（正整数 + 0）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUnMinusInt(this string value)
        {
            return Regex.IsMatch(value, RegularExp.UnMinusInteger);
        }

        /// <summary>
        /// 验证正整数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsPlusInt(this string value)
        {
            return Regex.IsMatch(value, RegularExp.PlusInteger);
        }

        /// <summary>
        /// 验证非正整数（负整数 + 0） 
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUnPlusInt(this string value)
        {
            return Regex.IsMatch(value, RegularExp.UnPlusInteger);
        }

        /// <summary>
        /// 验证负整数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsMinusInt(this string value)
        {
            return Regex.IsMatch(value, RegularExp.MinusInteger);
        }

        /// <summary>
        /// 验证整数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsInt(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Integer);
        }

        /// <summary>
        /// 验证非负浮点数（正浮点数 + 0）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUnMinusFloat(this string value)
        {
            return Regex.IsMatch(value, RegularExp.UnMinusFloat);
        }

        /// <summary>
        /// 验证正浮点数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsPlusFloat(this string value)
        {
            return Regex.IsMatch(value, RegularExp.PlusFloat);
        }

        /// <summary>
        /// 验证非正浮点数（负浮点数 + 0）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUnPlusFloat(this string value)
        {
            return Regex.IsMatch(value, RegularExp.UnPlusFloat);
        }

        /// <summary>
        /// 验证负浮点数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsMinusFloat(this string value)
        {
            return Regex.IsMatch(value, RegularExp.MinusFloat);
        }

        /// <summary>
        /// 验证浮点数
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsFloat(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Float);
        }

        /// <summary>
        /// 验证由26个英文字母组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsLetter(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Letter);
        }

        /// <summary>
        /// 验证由中文组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsChinese(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Chinese);
        }

        /// <summary>
        /// 验证由26个英文字母的大写组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUpperLetter(this string value)
        {
            return Regex.IsMatch(value, RegularExp.UpperLetter);
        }

        /// <summary>
        /// 验证由26个英文字母的小写组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsLowerLetter(this string value)
        {
            return Regex.IsMatch(value, RegularExp.LowerLetter);
        }

        /// <summary>
        /// 验证由数字和26个英文字母组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsNumericOrLetter(this string value)
        {
            return Regex.IsMatch(value, RegularExp.NumericOrLetter);
        }

        /// <summary>
        /// 验证由数字组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Numeric);
        }

        /// <summary>
        /// 验证由数字和26个英文字母或中文组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsNumericOrLetterOrChinese(this string value)
        {
            return Regex.IsMatch(value, RegularExp.NumbericOrLetterOrChinese);
        }

        /// <summary>
        /// 验证由数字、26个英文字母或者下划线组成的字符串
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsNumericOrLetterOrUnderline(this string value)
        {
            return Regex.IsMatch(value, RegularExp.NumericOrLetterOrUnderline);
        }

        /// <summary>
        /// 验证email地址
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsEmail(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Email);
        }

        /// <summary>
        /// 验证URL
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsUrl(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Url);
        }

        /// <summary>
        /// 验证电话号码
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsTelephone(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Telephone);
        }

        /// <summary>
        /// 验证手机号码
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsMobile(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Mobile);
        }

        /// <summary>
        /// 通过文件扩展名验证图像格式
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsImageFormat(this string value)
        {
            return Regex.IsMatch(value, RegularExp.ImageFormat);
        }

        /// <summary>
        /// 验证IP
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsIP(this string value)
        {
            return Regex.IsMatch(value, RegularExp.IP);
        }

        /// <summary>
        /// 验证日期（YYYY-MM-DD）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsDate(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Date);
        }

        /// <summary>
        /// 验证日期和时间（YYYY-MM-DD HH:MM:SS）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsDateTime(this string value)
        {
            return Regex.IsMatch(value, RegularExp.DateTime);
        }

        /// <summary>
        /// 验证颜色（#ff0000）
        /// </summary>
        /// <param name="input">要验证的字符串</param>
        /// <returns>验证通过返回true</returns>
        public static bool IsColor(this string value)
        {
            return Regex.IsMatch(value, RegularExp.Color);
        }

        public struct RegularExp
        {
            public const string Chinese = @"^[\u4E00-\u9FA5\uF900-\uFA2D]+$";
            public const string Color = "^#[a-fA-F0-9]{6}";
            public const string Date = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
            public const string DateTime = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d$";
            public const string Email = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            public const string Float = @"^(-?\d+)(\.\d+)?$";
            public const string ImageFormat = @"\.(?i:jpg|bmp|gif|ico|pcx|jpeg|tif|png|raw|tga)$";
            public const string Integer = @"^-?\d+$";
            public const string IP = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
            public const string Letter = "^[A-Za-z]+$";
            public const string LowerLetter = "^[a-z]+$";
            public const string MinusFloat = @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$";
            public const string MinusInteger = "^-[0-9]*[1-9][0-9]*$";
            public const string Mobile = "^0{0,1}13[0-9]{9}$";
            public const string NumbericOrLetterOrChinese = @"^[A-Za-z0-9\u4E00-\u9FA5\uF900-\uFA2D]+$";
            public const string Numeric = "^[0-9]+$";
            public const string NumericOrLetter = "^[A-Za-z0-9]+$";
            public const string NumericOrLetterOrUnderline = @"^\w+$";
            public const string PlusFloat = @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$";
            public const string PlusInteger = "^[0-9]*[1-9][0-9]*$";
            public const string Telephone = @"(\d+-)?(\d{4}-?\d{7}|\d{3}-?\d{8}|^\d{7,8})(-\d+)?";
            public const string UnMinusFloat = @"^\d+(\.\d+)?$";
            public const string UnMinusInteger = @"\d+$";
            public const string UnPlusFloat = @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$";
            public const string UnPlusInteger = @"^((-\d+)|(0+))$";
            public const string UpperLetter = "^[A-Z]+$";
            public const string Url = @"^http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$";
        }

        #endregion
    }
}
