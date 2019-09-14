using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job.Downloader
{
    /// <summary>
    /// 下载器接口
    /// </summary>
    public interface IDownloader
    {
        /// <summary>
        /// 执行下载
        /// </summary>
        void Exec();
    }
}
