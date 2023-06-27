using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SendMailTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WriteMailProcessor process = new WriteMailProcessor();
            Message message = new Message()
            {
                sender = "s96015299@gmail.com",
                subject = "測試信件",
                recipient = "t108ab0452@ntut.org.tw",
                cc = new List<string>() { "s960152999@gmail.com" },
                bcc = new List<string>() { "s9601529999@gmail.com" },
                attachment = @"C:\Users\2200530M1CINAMIN\Desktop\backupFile\20230601\新增 文字文件.txt",
                body = "我準備要下班嚕!",
                password = "vnkiwrovenuwjsml"
            };
            process.SendMail(message);
            MessageBox.Show("發送成功!");
        }
    }
}
