
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using DataMonitor.Format;

namespace DataMonitor
{
  


    public partial class Form1 : Form
    {
        public FileWriter _writer = FileWriter.GetInstance();
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        public Form1()
        {
            AllocConsole();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StringBuilder _logMessage = new StringBuilder();


            DataProcessor d = new DataProcessor();
            Task t = new Task(() =>
            {
                
                d.fileWatcher();
            });
            t.Start();


            _logMessage.AppendLine($"{DateTime.Now} fileWatchcer開始執行....");
            _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "log.txt" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
