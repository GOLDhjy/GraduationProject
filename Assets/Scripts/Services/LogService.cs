using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyService
{
    public enum LogLevel
    {
        info = 0,
        err = 1
    }
    public class LogService : Singleton<LogService>
    {
        private static string Directory = System.Environment.CurrentDirectory;
        private static string CurrentTime = Convert.ToString(System.DateTime.Now.Year + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "-" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second);
        private static string FilePath = @"Logs\" + CurrentTime + ".log";

        public void Log(LogLevel level, string str)
        {
            switch (level)
            {
                case LogLevel.info:
                    //FileOperater.WriteTest(FilePath, str);
                    break;
                case LogLevel.err:
                    Debug.Log("-><color=#ff0000ff>" + str + "</color>");
                    //FileOperater.WriteTest(FilePath, str);
                    break;
                default:
                    break;
            }
        }
    }
}
