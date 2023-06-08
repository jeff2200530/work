using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Splunk_Class01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // IP 與 Port 設定
            IPAddress iP = IPAddress.Parse("172.20.10.2");
            IPEndPoint iPEndPoint = new IPEndPoint(iP, 0001);
            // 測試文字訊息
            string message = "Test message";

            // Socket連接
            Socket scoketSet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            scoketSet.Connect(iPEndPoint);

            // 傳送
            byte[] messageByte = Encoding.UTF8.GetBytes(message);
            scoketSet.Send(messageByte, 0);
            MessageBox.Show("Send");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // IP 與 Port 設定
            IPAddress iP = IPAddress.Parse("172.20.10.2");
            IPEndPoint iPEndPoint = new IPEndPoint(iP, 0001);
            // Socket 連接
            Socket scoketSet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            scoketSet.Connect(iPEndPoint);

            // 測試文字訊息
            string dt = DateTime.Now.ToString("yyyy/MM/dd H:mm:ss");
            List<SplunkDTO> splunkJson = new List<SplunkDTO>()
   {
      new SplunkDTO(){functionName="function A", description="WorkA description", value=5, time=dt},
      new SplunkDTO(){functionName="function B", description="WorkB description", value=8, time=dt},
      new SplunkDTO(){functionName="function C", description="WorkC description", value=1, time=dt}
   };
            //Newtonsoft.Json序列化
            string jsonData = JsonConvert.SerializeObject(splunkJson);

            // 傳送
            byte[] messageByte = Encoding.UTF8.GetBytes(jsonData);
            scoketSet.Send(messageByte, 0);
            MessageBox.Show("Send");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

         
        }
    }
}
