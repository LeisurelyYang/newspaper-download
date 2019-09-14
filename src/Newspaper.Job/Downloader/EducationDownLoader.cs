using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job.Downloader
{
    public class EducationDownLoader : AbstractDownLoader
    {
        private const string CHECK_URL = "http://paper.jyb.cn/zgjyb/html/{0}/node_2.htm";
        private const string DOWNLOAD_URL = "http://paper.jyb.cn/zgjyb/images/{0}/{1}/ZGJYB{2}{3}.pdf";
        public EducationDownLoader(DateTime startTime, DateTime endTime)
            : base(startTime, endTime)
        {
            this.NewspaperName = "中国教育报";

            DownloadPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EducationDownload");
            MergePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EducationMerge");

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
            return string.Format(DOWNLOAD_URL, downloadTime.ToString("yyyy-MM/dd"), (subIndex + 1).ToString().PadLeft(2, '0'), downloadTime.ToString("yyyyMMdd"), (subIndex + 1).ToString().PadLeft(2, '0'));
        }

        protected override string GetFileName(DateTime downloadTime, int subIndex)
        {
            return $"ZGJYB{downloadTime.ToString("yyyyMMdd")}{subIndex.ToString().PadLeft(2, '0')}.pdf";
        }
    }
}
