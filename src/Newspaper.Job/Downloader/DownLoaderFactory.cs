using Newspaper.Job.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job.Downloader
{
    public class DownLoaderFactory
    {
        /// <summary>
        /// 创建下载器
        /// </summary>
        /// <param name="type">下载器类型</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <returns></returns>
        public static IDownloader CreateDownloader(NewsPaperTypeEnum type, DateTime start, DateTime end)
        {
            IDownloader downloader = null;

            switch (type)
            {
                case NewsPaperTypeEnum.ChinaTeacher:
                    downloader = new TeacherDownLoader(start, end);
                    break;
                case NewsPaperTypeEnum.ChinaEducation:
                    downloader = new EducationDownLoader(start, end);
                    break;
            }

            return downloader;
        }
    }
}
