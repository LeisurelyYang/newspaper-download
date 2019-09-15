using log4net;
using Newspaper.Job.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job.Downloader
{
    public abstract class AbstractDownLoader : IDownloader
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AbstractDownLoader));

        private DateTime startTime;
        private DateTime endTime;

        protected HttpClient client;

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadPath { get; set; }

        /// <summary>
        /// 合并保存地址
        /// </summary>
        public string MergePath { get; set; }


        /// <summary>
        /// 已经下载的日期
        /// </summary>
        protected List<DateTime> HasDownloadDates { get; set; }

        /// <summary>
        /// 是否自动保存成Pdf
        /// </summary>
        protected bool IsAutoMergePdf { get; set; } = true;

        /// <summary>
        /// 报纸名字
        /// </summary>
        protected string NewspaperName { get; set; }

        /// <summary>
        /// 初始化下载器
        /// </summary>
        /// <param name="startTime">下载开始时间</param>
        /// <param name="endTime">下载结束时间</param>
        public AbstractDownLoader(DateTime startTime, DateTime endTime)
        {
            this.startTime = startTime;
            this.endTime = endTime;

            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
        }
        public void Exec()
        {
            string foldPath = DownloadPath;

            //根据开始日期算
            DateTime start = startTime;
            List<DateTime> listTerm = GetTerms();

            for (int i = 0; i < listTerm.Count; i++)
            {
                //生成文件夹
                string dirPath = Path.Combine(foldPath, $"{listTerm[i].ToString("yyyyMMdd")}");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                List<string> fileNames = GetFileNames(listTerm[i]);

                for (int j = 0; j < fileNames.Count; j++)
                {
                    try
                    {
                        string path = GetDownLink(listTerm[i], j, fileNames[j]);
                        byte[] fileBytes = client.GetByteArrayAsync(path).GetAwaiter().GetResult();
                        string filePath = Path.Combine(dirPath, fileNames[j]);

                        File.WriteAllBytes(filePath, fileBytes);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                        break;
                    }
                }
                //合并
                SendEmail(listTerm[i], dirPath);
            }
        }

        /// <summary>
        /// 发送到邮箱
        /// </summary>
        /// <param name="term">报纸期数</param>
        /// <param name="dirPath">保存的文件路径</param>
        private void SendEmail(DateTime term, string dirPath)
        {
            if (IsAutoMergePdf)
            {
                string name = PdfHelp.MergePdf(dirPath, MergePath);
                string mergeSavePath = Path.Combine(MergePath, $"{name}.pdf");

                //发送邮件
                MailHelper.Send("zhahainie8480@163.com", Appsettings.Receivers, $"{term.ToString("yyyyMMdd")}期{NewspaperName}", $"{NewspaperName}，请查收", mergeSavePath);
            }
        }

        /// <summary>
        /// 获取要下载文件的名字集合
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private List<string> GetFileNames(DateTime dateTime)
        {
            List<string> fileNames = new List<string>();
            for (int i = 1; i <= 30; i++)
            {
                fileNames.Add(GetFileName(dateTime, i));
            }
            return fileNames;
        }

        /// <summary>
        /// 获取要下载的日期集合
        /// </summary>
        /// <returns></returns>
        private List<DateTime> GetTerms()
        {
            List<DateTime> listTerm = new List<DateTime>() { };
            for (DateTime i = startTime; i <= endTime; i = i.AddDays(1))
            {
                if (HasDownloadDates.Contains(i))
                {
                    continue;
                }
                string requestUrl = GetCheckUrl(i);
                try
                {
                    var response = client.GetAsync(requestUrl).GetAwaiter().GetResult();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        listTerm.Add(i);
                    }
                }
                catch (HttpRequestException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    continue;
                }
            }
            return listTerm;
        }

        /// <summary>
        /// 获取要下载的链接中文件名字部分
        /// </summary>
        /// <param name="downloadTime">当前下载日期</param>
        /// <param name="subIndex">当前分版索引</param>
        /// <returns></returns>
        protected abstract string GetFileName(DateTime downloadTime, int subIndex);


        /// <summary>
        /// 获取检验Url
        /// </summary>
        /// <param name="downloadTime">当前下载日期</param>
        /// <returns></returns>
        protected abstract string GetCheckUrl(DateTime downloadTime);


        /// <summary>
        /// 获取下载链接
        /// </summary>
        /// <param name="downloadTime">当前下载日期</param>
        /// <param name="subIndex">当前分版索引</param>
        /// <returns></returns>
        protected abstract string GetDownLink(DateTime downloadTime, int subIndex, string fileName);


        /// <summary>
        /// 释放HttpClient资源
        /// </summary>
        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
