using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyService
{
    public class FileService : Singleton<FileService>
    {
        public static void WriteTest(string path, string content)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {

                    sw.WriteLine(content);
                    LogService.Instance.Log(LogLevel.info, "写入文件成功");
                }
            }
            catch (Exception ep)
            {
                LogService.Instance.Log(LogLevel.err, "写入文件失败:" + ep.ToString());
            }
        }
        public static void ReadTest(string path, StringBuilder content)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    content.Append(sr.ReadLine());
                    LogService.Instance.Log(LogLevel.info, "读入文件成功");
                }
            }
            catch (Exception ep)
            {
                LogService.Instance.Log(LogLevel.err, "读入文件失败:" + ep.ToString());
            }
        }
    }
}
