using Quartz;
using Quartz.Impl;
using System;
using Topshelf;

namespace Newspaper.Job
{
    public class Startup : ServiceControl
    {
        private IScheduler scheduler;
        public Startup()
        {
            //新建一个调度器工工厂
            ISchedulerFactory factory = new StdSchedulerFactory();
            //使用工厂生成一个调度器
            scheduler = factory.GetScheduler();
        }

        public bool Start(HostControl hostControl)
        {
            if (!scheduler.IsStarted)
            {
                try
                {
                    //启动调度器
                    scheduler.Start();

                    //Job
                    ChinaTeacherJob();//中国教师job
                    ChinaEducationJob();//中国教育Job
                }
                catch (Exception)
                {
                }
            }
            return true;
        }

        private void ChinaEducationJob()
        {
            IJobDetail behavior = JobBuilder.Create<EducationJob>()
                    .WithIdentity("EducationJob", "EducationJob")
                    .WithDescription("中国教育报job")
                    .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .ForJob(behavior)
#if DEBUG
            .StartNow()
#endif
                .WithCronSchedule(Appsettings.JobCron)
                .Build();
            scheduler.ScheduleJob(behavior, trigger);
        }

        private void ChinaTeacherJob()
        {
            IJobDetail behavior = JobBuilder.Create<TeacherJob>()
                   .WithIdentity("TeacherJob", "TeacherJob")
                   .WithDescription("中国教师报job")
                   .Build();
            ITrigger trigger = TriggerBuilder.Create()
                .ForJob(behavior)
#if DEBUG
            .StartNow()
#endif
                .WithCronSchedule(Appsettings.JobCron)
                .Build();
            scheduler.ScheduleJob(behavior, trigger);
        }

        public bool Stop(HostControl hostControl)
        {
            if (!scheduler.IsShutdown)
            {
                scheduler.Shutdown();
            }
            return true;
        }
    }
}