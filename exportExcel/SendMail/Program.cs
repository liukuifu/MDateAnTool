using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Mime;
using System.Net;

namespace SendMail
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = @"E:\MD\UnZipFile\2015-11-30\数据分析-20151201.xlsx";
            try
            {
                MailMessage message = new MailMessage("5539383@qq.com", "liukuifu@oasgames.com", "sub test", "test test.");
                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                // Create  the file attachment for this e-mail message.
                // Add time stamp information for the file.
                //ContentDisposition disposition = data.ContentDisposition;
                //disposition.CreationDate = System.IO.File.GetCreationTime(file);
                //disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                //disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                //// Add the file attachment to this e-mail message.
                //message.Attachments.Add(data);
                //Send the message.
                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                client.Port = 25;
                client.Host = "smtp.qq.com";    //smtp服务
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("5539383@qq.com", "780307devil@");  //自己邮箱的帐密
                                                                                                              // Add credentials if the SMTP server requires them.
                client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.Send(message);
                string str = "";
            }
            catch (Exception ex)
            {

            }
        }
    }
}
