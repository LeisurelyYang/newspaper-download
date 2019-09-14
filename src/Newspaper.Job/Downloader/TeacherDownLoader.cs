using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job.Downloader
{
    public class TeacherDownLoader : AbstractDownLoader
    {
        private const string CHECK_URL = "http://www.chinateacher.com.cn/zgjsb/html/{0}/node_22.htm";
        private const string DOWNLOAD_URL = "http://www.chinateacher.com.cn/zgjsb/images/{0}/{1}/{2}";
        public TeacherDownLoader(DateTime startTime, DateTime endTime)
            : base(startTime, endTime)
        {
            this.NewspaperName = "中国教师报";

            DownloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TeacherDownload");
            MergePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TeacherMerge");

            

            if (!Directory.Exists(DownloadPath))
            {
                Directory.CreateDirectory(DownloadPath);
            }
            if (!Directory.Exists(MergePath))
            {
                Directory.CreateDirectory(MergePath);
            }

            HasDownloadDates = Directory.GetFiles(MergePath).Select(file =>
            {
                string str = Path.GetFileNameWithoutExtension(file);
                int year = int.Parse(str.Substring(0, 4));
                int month = int.Parse(str.Substring(4, 2));
                int day = int.Parse(str.Substring(6, 2));
                return new DateTime(year, month, day);
            })?.ToList() ?? new List<DateTime>();

        }
        protected override string GetCheckUrl(DateTime downloadTime)
        {
            return string.Format(CHECK_URL, downloadTime.ToString("yyyy-MM/dd"));
        }

        protected override string GetDownLink(DateTime downloadTime, int subIndex, string fileName)
        {
            return string.Format(DOWNLOAD_URL, downloadTime.ToString("yyyy-MM/dd"), (subIndex + 1).ToString().PadLeft(2, '0'), fileName);
        }

        protected override string GetFileName(DateTime downloadTime, int subIndex)
        {
            return $"ZGJSB{subIndex.ToString().PadLeft(2, '0')}B{downloadTime.ToString("yyyyMMdd")}C.pdf";
        }
    }
}
