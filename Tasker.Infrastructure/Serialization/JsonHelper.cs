using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Tasker.Infrastructure.Serialization
{
    public class JsonHelper
    {
        /// <summary>
        /// JSON 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <returns></returns>
        public T Deserialize<T>(string s)
        {
            return (T)this.Deserialize(s, typeof(T));
        }

        /// <summary>
        /// JSON 反序列化
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public object Deserialize(string s, Type type)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(type);
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(s));
            object obj = ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// JSON 序列化
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public string Serializer<T>(T o)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, o);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
    }
}
