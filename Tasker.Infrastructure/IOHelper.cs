using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tasker.Infrastructure
{
    /// <summary>
    /// 文件操作
    /// </summary>
    public class IOHelper
    {
        /// <summary>
        /// 获取目录大小 递归
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static long DirSize(DirectoryInfo d)
        {
            long Size = 0;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                Size += fi.Length;
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                Size += DirSize(di);
            }
            return (Size);
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path"></param>
        public static void DirMake(string path)
        {
            bool isExists = Directory.Exists(path);
            if (!isExists)
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="fromPath"></param>
        /// <param name="toPath"></param>
        public static bool DirCopy(string fromPath, string toPath)
        {
            try
            {
                // 创建目的文件夹
                DirMake(toPath);

                // 拷贝文件
                DirectoryInfo fromDir = new DirectoryInfo(fromPath);
                FileInfo[] fileArray = fromDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    file.CopyTo(toPath + "\\" + file.Name, true);
                }

                // 循环子文件夹
                DirectoryInfo toDir = new DirectoryInfo(toPath);
                DirectoryInfo[] subDirArray = toDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    DirCopy(subDir.FullName, toPath + "//" + subDir.Name);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 文件夹删除
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void DirDelete(string path)
        {
            if (Directory.Exists(path))
            {
                foreach (string deleteFile in Directory.GetFileSystemEntries(path))
                {
                    if (File.Exists(deleteFile))
                        File.Delete(deleteFile);
                    else
                        DirDelete(deleteFile);
                }
                Directory.Delete(path);
            }
        }
    }
}
