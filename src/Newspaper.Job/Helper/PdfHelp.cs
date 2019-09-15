using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job.Helper
{
    public class PdfHelp
    {
        /// <summary>
        /// 合并指定文件夹下所有PDF文件
        /// </summary>
        /// <param name="foldPath">原PDF文件夹</param>
        /// <param name="destPath">合并保存文件夹</param>
        /// <returns>合并后的PDF名称</returns>
        public static string MergePdf(string foldPath, string destPath)
        {
            string name = Path.GetFileName(foldPath);
            string[] files = Directory.GetFiles(foldPath);

            string mergePath = Path.Combine(destPath, $"{name}.pdf");
            using (PdfDocumentBase doc = PdfDocument.MergeFiles(files))
            {
                //保存文档
                doc.Save(mergePath, FileFormat.PDF);
                return name;
            }
        }
    }
}
