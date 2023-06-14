using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendMailTool
{
    public class WriteMailProcessor
    {

        public void SendMail(Message message) {


            try
            {
                MailMessage msg = new MailMessage();
                //寄件者
                msg.From = new MailAddress(message.sender);
                //收件者
                msg.To.Add(message.recipient);
                //主旨
                msg.Subject = message.subject;

                //密件副本收件者
                foreach (var recipient in message.bcc)
                { 
                    msg.Bcc.Add(recipient); 
                }

                //副本收件者
                foreach (var recipient in message.cc)
                { 
                    msg.CC.Add(recipient); 
                }

                //加入附件
                Attachment attch = new Attachment(message.attachment);
                msg.Attachments.Add(attch);

                //內文
                msg.Body =message.body;

                //msg.IsBodyHtml = true;
                //msg.ReplyToList.Add(new MailAddress("s96015299@gmail.com", "s96015299"));


                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 25))
                {

                    smtp.Credentials = new NetworkCredential(message.sender, message.password);
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                }

                Console.WriteLine("傳送成功!");
        
            }
            catch (Exception ex)
            {
                Console.WriteLine($"傳送mail失敗，錯誤訊息:{ex}");
            }


        }
    }
}
