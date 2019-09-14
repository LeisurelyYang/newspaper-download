using Newspaper.Model;
using Spire.Pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Newspaper
{
    public partial class frmNewspaper : Form
    {
        private HttpClient checkClient = null;

        public frmNewspaper()
        {
            InitializeComponent();
            checkClient = new HttpClient();
            checkClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");
        }

        private void btnTeacher_Click(object sender, EventArgs e)
        {
            string msg;
            if (!CheckParam(out msg))
            {
                MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<DateTime> hasDownLoadFile = new List<DateTime>();
            //List<DateTime> hasDownLoadFile = Directory.GetFiles(@"D:\temp\中国教师报合并结果").Select(file =>
            //  {
            //      string str = Path.GetFileNameWithoutExtension(file);
            //      int year = int.Parse(str.Substring(0, 4));
            //      int month = int.Parse(str.Substring(4, 2));
            //      int day = int.Parse(str.Substring(6, 2));
            //      return new DateTime(year, month, day);
            //  })?.ToList();

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件保存路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;


                FolderBrowserDialog dialog_pdf = new FolderBrowserDialog();
                dialog_pdf.Description = "请选择合并后PDF文件保存路径";
                if (dialog_pdf.ShowDialog() == DialogResult.OK)
                {
                    DownloadDto param = new DownloadDto()
                    {
                        SavePath = foldPath,
                        EndTime = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day),
                        StartTime = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day),
                        IsAutoMergePdf = cbAutoMergePdf.Checked,
                        HasDownLoadFiles = hasDownLoadFile,
                        PdfSavePath = dialog_pdf.SelectedPath
                    };
                    Thread thread = new Thread(new ParameterizedThreadStart(Teacher));
                    thread.Start(param);
                }
            }
        }

        private bool CheckParam(out string msg)
        {
            msg = "成功";
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;

            if (start > DateTime.Now)
            {
                msg = "开始日期不可大于当前时间";
                return false;
            }

            if (end > DateTime.Now)
            {
                msg = "结束日期不可大于当前时间";
                return false;
            }

            if (start > end)
            {
                msg = "开始日期不可大于结束日期";
                return false;
            }

            return true;
        }

        private void Teacher(object obj)
        {
            var param = obj as DownloadDto;
            string foldPath = param.SavePath;

            //根据开始日期算
            DateTime start = param.StartTime;

            List<DateTime> listTerm = GetTerms(start, param.EndTime, NewsPaperTypeEnum.ChinaTeacher, param.HasDownLoadFiles);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");

            int downloadNum = 0;
            for (int i = 0; i < listTerm.Count; i++)
            {
                labProgress.Invoke(new Action<String>(p =>
                {
                    labProgress.Text = p;
                }), $"共{listTerm.Count}期,已下载{downloadNum}期");

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
                        string path = $"http://www.chinateacher.com.cn/zgjsb/images/{listTerm[i].ToString("yyyy-MM/dd")}/{(j + 1).ToString().PadLeft(2, '0')}/{fileNames[j]}";

                        byte[] fileBytes = client.GetByteArrayAsync(path).GetAwaiter().GetResult();
                        string filePath = Path.Combine(dirPath, fileNames[j]);

                        File.WriteAllBytes(filePath, fileBytes);

                        labTips.Invoke(new Action<String>(p =>
                        {
                            labTips.Text = p;
                        }), $"{fileNames[j]}下载完成");
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                //合并
                if (param.IsAutoMergePdf)
                {
                    MergePdf(dirPath, param.PdfSavePath);
                    labTips.Invoke(new Action<String>(p =>
                    {
                        labTips.Text = p;
                    }), $"{Path.GetFileName(dirPath)}.pdf合并完成");
                }
                downloadNum++;
            }

            MessageBox.Show("下载完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private List<string> GetFileNames(DateTime dateTime, NewsPaperTypeEnum type = NewsPaperTypeEnum.ChinaTeacher)
        {
            List<string> fileNames = new List<string>();

            for (int i = 1; i <= 30; i++)
            {
                if (type == NewsPaperTypeEnum.ChinaTeacher)
                {
                    fileNames.Add($"ZGJSB{i.ToString().PadLeft(2, '0')}B{dateTime.ToString("yyyyMMdd")}C.pdf");
                }
                else
                {
                    fileNames.Add($"ZGJYB{dateTime.ToString("yyyyMMdd")}{i.ToString().PadLeft(2, '0')}.pdf");
                }

            }
            return fileNames;
        }


        private List<DateTime> GetTerms(DateTime start, DateTime end, NewsPaperTypeEnum type = NewsPaperTypeEnum.ChinaTeacher, List<DateTime> hasDownloadfiles = null)
        {
            List<DateTime> listTerm = new List<DateTime>() { };

            string url = string.Empty;
            if (type == NewsPaperTypeEnum.ChinaTeacher)
            {
                url = "http://www.chinateacher.com.cn/zgjsb/html/{0}/node_22.htm";
            }
            else if (type == NewsPaperTypeEnum.ChinaEducation)
            {
                url = "http://paper.jyb.cn/zgjyb/html/{0}/node_2.htm";
            }

            for (DateTime i = start; i <= end; i = i.AddDays(1))
            {
                if (hasDownloadfiles.Contains(i))
                {
                    continue;
                }
                string requestUrl = string.Format(url, i.ToString("yyyy-MM/dd"));
                try
                {
                    var response = checkClient.GetAsync(requestUrl).GetAwaiter().GetResult();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        listTerm.Add(i);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return listTerm;
        }

        private void btnDelDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件保存路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                List<string> emptyDirs = new List<string>();

                string[] alldirs = Directory.GetDirectories(foldPath);

                foreach (var dir in alldirs)
                {
                    string[] files = Directory.GetFiles(dir);
                    if (files == null || files.Length == 0)
                    {
                        emptyDirs.Add(dir);
                    }
                }

                //开始删除空文件夹
                foreach (var empDir in emptyDirs)
                {
                    Directory.Delete(empDir);
                }

                MessageBox.Show("删除完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            string msg;
            if (!CheckParam(out msg))
            {
                MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            //var hasDownLoadFile = Directory.GetFiles(@"D:\temp\中国教育报合并结果").Select(file =>
            //{
            //    string str = Path.GetFileNameWithoutExtension(file);
            //    int year = int.Parse(str.Substring(0, 4));
            //    int month = int.Parse(str.Substring(4, 2));
            //    int day = int.Parse(str.Substring(6, 2));
            //    return new DateTime(year, month, day);
            //})?.ToList();

            List<DateTime> hasDownLoadFile = new List<DateTime>();

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件保存路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                FolderBrowserDialog dialog_pdf = new FolderBrowserDialog();
                dialog_pdf.Description = "请选择合并后PDF文件保存路径";
                if (dialog_pdf.ShowDialog() == DialogResult.OK)
                {
                    DownloadDto param = new DownloadDto()
                    {
                        SavePath = foldPath,
                        EndTime = new DateTime(dtpEnd.Value.Year, dtpEnd.Value.Month, dtpEnd.Value.Day),
                        StartTime = new DateTime(dtpStart.Value.Year, dtpStart.Value.Month, dtpStart.Value.Day),
                        IsAutoMergePdf = cbAutoMergePdf.Checked,
                        HasDownLoadFiles = hasDownLoadFile,
                        PdfSavePath = dialog_pdf.SelectedPath
                    };
                    Thread thread = new Thread(new ParameterizedThreadStart(Education));
                    thread.Start(param);
                }

            }
        }

        private void Education(object obj)
        {
            var param = obj as DownloadDto;
            string foldPath = param.SavePath;

            //根据开始日期算
            DateTime start = param.StartTime;

            List<DateTime> listTerm = GetTerms(start, param.EndTime, NewsPaperTypeEnum.ChinaEducation, param.HasDownLoadFiles);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/76.0.3809.100 Safari/537.36");

            int downloadNum = 0;
            for (int i = 0; i < listTerm.Count; i++)
            {
                labProgress.Invoke(new Action<String>(p =>
                {
                    labProgress.Text = p;
                }), $"共{listTerm.Count}期,已下载{downloadNum}期");

                //生成文件夹
                string dirPath = Path.Combine(foldPath, $"{listTerm[i].ToString("yyyyMMdd")}");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                List<string> fileNames = GetFileNames(listTerm[i], NewsPaperTypeEnum.ChinaEducation);


                for (int j = 0; j < fileNames.Count; j++)
                {
                    try
                    {
                        string path = $"http://paper.jyb.cn/zgjyb/images/{listTerm[i].ToString("yyyy-MM/dd")}/{(j + 1).ToString().PadLeft(2, '0')}/ZGJYB{listTerm[i].ToString("yyyyMMdd")}{(j + 1).ToString().PadLeft(2, '0')}.pdf";

                        byte[] fileBytes = client.GetByteArrayAsync(path).GetAwaiter().GetResult();
                        string filePath = Path.Combine(dirPath, fileNames[j]);

                        File.WriteAllBytes(filePath, fileBytes);

                        labTips.Invoke(new Action<String>(p =>
                        {
                            labTips.Text = p;
                        }), $"{fileNames[j]}下载完成");
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                //合并
                if (param.IsAutoMergePdf)
                {
                    MergePdf(dirPath, param.PdfSavePath);
                    labTips.Invoke(new Action<String>(p =>
                    {
                        labTips.Text = p;
                    }), $"{Path.GetFileName(dirPath)}.pdf合并完成");
                }
                downloadNum++;
            }

            MessageBox.Show("下载完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //合并PDF
        private void btnMerge_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.SelectedPath = @"D:\temp\中国教师报";
            dialog.Description = "请选择文件保存路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //选择处理文件夹
                string foldPath = dialog.SelectedPath;

                //选择保存文件夹
                FolderBrowserDialog saveDialog = new FolderBrowserDialog();
                saveDialog.SelectedPath = foldPath;
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveDialog.SelectedPath;

                    MergePdfDto dto = new MergePdfDto()
                    {
                        DestPath = savePath,
                        SourcePath = foldPath
                    };

                    Thread thread = new Thread(new ParameterizedThreadStart(MergeDirPdf));
                    thread.Start(dto);
                }

            }
        }

        private void MergeDirPdf(Object dto)
        {
            MergePdfDto param = dto as MergePdfDto;

            string[] dirs = Directory.GetDirectories(param.SourcePath);
            Parallel.ForEach<string>(dirs, dir =>
            {
                MergePdf(dir, param.DestPath);
                string tips = $"{Path.GetFileName(dir)}.pdf合并完成";
                labTips.Invoke(new Action<String>(p =>
                {
                    labTips.Text = p;
                }), tips);
            });

            MessageBox.Show("合并完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MergePdf(string foldPath, string destPath)
        {
            string name = Path.GetFileName(foldPath);
            string[] files = Directory.GetFiles(foldPath);

            string mergePath = Path.Combine(destPath, $"{name}.pdf");
            PdfDocumentBase doc = PdfDocument.MergeFiles(files);

            //保存文档
            doc.Save(mergePath, FileFormat.PDF);
        }

        private void frmNewspaper_Load(object sender, EventArgs e)
        {
            dtpStart.MaxDate = DateTime.Now.AddDays(-1);
            dtpStart.MinDate = new DateTime(2019, 1, 1);

            dtpEnd.MaxDate = DateTime.Now;
            dtpEnd.MinDate = new DateTime(2019, 1, 1);

            dtpStart.Value = DateTime.Now.AddDays(-10);
            dtpEnd.Value = DateTime.Now;
        }
    }
}
