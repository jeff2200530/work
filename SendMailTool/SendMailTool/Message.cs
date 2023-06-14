using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMailTool
{
    public class Message
    {
        //寄件者
        public string sender;
        //收件者
        public string recipient;
        //主旨
        public string subject;
        //密件副本收件者
        public List<string> bcc;
        //副本收件者
        public List<string> cc;
        //附件
        public string attachment;
        //內文
        public string body;

        //寄件者信箱密碼
        public string password;



    }
}
