using log4net;
using Newspaper.Job.Downloader;
using Newspaper.Job.Helper;
using Newspaper.Job.Model;
using Quartz;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job
{
    /// <summary>
    /// 中国教师报
    /// </summary>
    [DisallowConcurrentExecution]
    public class TeacherJob : IJob
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(TeacherJob));
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

#if DEBUG
                end = new DateTime(2019, 09, 11);
#endif
                DateTime start = end.AddDays(-1);

                using (IDownloader loader = DownLoaderFactory.CreateDownloader(NewsPaperTypeEnum.ChinaTeacher, start, end))
                {
                    loader.Exec();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }
    }
}
