using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job
{
    public class Appsettings
    {
        /// <summary>
        ///Corn表达式
        /// </summary>
        public static string JobCron
        {
            get
            {
                return ConfigurationManager.AppSettings["JobCron"];
            }
        }
        /// <summary>
        /// 接收者的邮箱地址
        /// </summary>
        public static List<string> Receivers
        {
            get
            {
                string receiverStr = ConfigurationManager.AppSettings["Receivers"];
                return receiverStr.Split(';')?.ToList();
            }
        }
    }
}
