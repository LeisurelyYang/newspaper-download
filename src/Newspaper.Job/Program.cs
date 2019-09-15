using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace Newspaper.Job
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var serviceName = "newspaper-报纸下载推送服务-v1.0.0.0";
            HostFactory.Run(x =>
            {
                x.Service<Startup>();
                x.SetDescription(serviceName);
                x.SetDisplayName(serviceName);
                x.SetServiceName(serviceName);
                x.RunAsLocalSystem();

                x.EnableServiceRecovery(r =>
                {
                    r.RestartService(1);
                    r.OnCrashOnly();
                });

                x.SetStartTimeout(TimeSpan.FromMinutes(1));
                x.SetStopTimeout(TimeSpan.FromMinutes(1));
            });

            Console.Read();
        }
    }
}
