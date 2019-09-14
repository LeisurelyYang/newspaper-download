using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Model
{
    public class DownloadDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }


        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }


        /// <summary>
        /// 是否自动下载
        /// </summary>
        public bool IsAutoMergePdf { get; set; }


        /// <summary>
        /// 保存文件夹路径
        /// </summary>
        public string SavePath { get; set; }

        /// <summary>
        /// PDF保存文件夹路径
        /// </summary>
        public string PdfSavePath { get; set; }


        /// <summary>
        /// 已下载文件集合
        /// </summary>
        public List<DateTime> HasDownLoadFiles { get; set; }
    }
}
