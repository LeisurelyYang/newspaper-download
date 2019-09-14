using Newspaper.Job.Downloader;
using Newspaper.Job.Helper;
using Newspaper.Job.Model;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job
{
    [DisallowConcurrentExecution]
    public class EducationJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                DateTime end = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
#if DEBUG
                end = new DateTime(2019, 09, 11);
#endif
                DateTime start = end.AddDays(-1);
                IDownloader loader = DownLoaderFactory.CreateDownloader(NewsPaperTypeEnum.ChinaEducation, start, end);
                loader.Exec();
            }
            catch (Exception)
            {
            }
        }
    }
}
