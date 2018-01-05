#region copyright
// <copyright file="FormatterComm"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>ddfm</author>
// <datecreated>2017/11/17 9:17:16</datecreated>
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Comm
{
    public static class FormatterComm
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return Convert.ToBase64String(buffer);
            }

        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string base64String)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                byte[] bytes = Convert.FromBase64String(base64String);
                stream.Write(bytes, 0, bytes.Length);
                stream.Position = 0;
                IFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }

        }
    }
}
