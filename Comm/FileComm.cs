#region copyright
// <copyright file="FileComm"  company="CIS"> 
// Copyright (c) CIS. All Right Reserved
// </copyright>
// <author>ddfm</author>
// <datecreated>2017/11/17 9:15:45</datecreated>
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comm
{

    public class FileComm
    {

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="text">文本内容</param>
        public static void WriteText(string path, string fileName, string text)
        {
            if (!System.IO.Directory.Exists(path))
                Directory.CreateDirectory(path);
            FileStream fs = new FileStream(path + fileName, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(text);
            sw.Close();
            fs.Close();
        }
        public static void WriteFile(string path, string fileName, Stream stream)
        {
            if (!System.IO.Directory.Exists(path))
                Directory.CreateDirectory(path);
            using (FileStream fs = System.IO.File.OpenWrite(path + fileName))
            {
                if (fs.CanWrite)
                {
                    fs.Seek(0, SeekOrigin.Current);
                    int bytecount = 1024 * 1024;
                    byte[] bytesize = new byte[bytecount];
                    int size;
                    while ((size = stream.Read(bytesize, 0, bytecount)) > 0)
                    {
                        fs.Write(bytesize, 0, size);
                    }
                }
            }
            stream.Close();
            stream.Dispose();

        }
        public static void WriteFile(string fileName, Stream stream)
        {
            using (FileStream fs = System.IO.File.OpenWrite(fileName))
            {
                if (fs.CanWrite)
                {
                    fs.Seek(0, SeekOrigin.Current);
                    int bytecount = 1024 * 1024;
                    byte[] bytesize = new byte[bytecount];
                    int size;
                    while ((size = stream.Read(bytesize, 0, bytecount)) > 0)
                    {
                        fs.Write(bytesize, 0, size);
                    }
                }
            }
            stream.Close();
            stream.Dispose();

        }
        public static void WriteFile(string fileName, byte[] btys)
        {
            using (FileStream fs = System.IO.File.OpenWrite(fileName))
            {
                if (fs.CanWrite)
                {
                    fs.Seek(0, SeekOrigin.Current);
                    fs.Write(btys, 0, btys.Length);
                }
            }

        }
    }
}
