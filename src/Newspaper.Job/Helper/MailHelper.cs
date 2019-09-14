using Simplify.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Newspaper.Job.Helper
{
    public static class MailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="receivers">接收者</param>
        /// <param name="subject">主题</param>
        /// <param name="body">邮件正文</param>
        /// <returns></returns>
        public static async Task SendAsync(string sender, List<string> receivers, string subject, string body)
        {
            try
            {
                await MailSender.Default.SendAsync(sender, receivers, subject, body);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="sender">发送者</param>
        /// <param name="receivers">接收者</param>
        /// <param name="subject">主题</param>
        /// <param name="body">邮件正文</param>
        /// <returns></returns>
        public static void Send(string sender, List<string> receivers, string subject, string body, string attachFilePath)
        {
            try
            {
                Attachment attach = new Attachment(attachFilePath);
                MailSender.Default.Send(sender, receivers, subject, body, "", attach);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
