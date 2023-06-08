
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
using DataInput.Format;
using NLog.Config;
using NLog;
using NLog.Targets;
using System.IO;
using System.Reflection;

namespace DataInput
{
    public partial class Form1 : Form
    {
        public Logger logger = LogManager.GetCurrentClassLogger();

        [DllImport("Kernel32")]
        public static extern void FreeConsole();

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        public Form1()
        {
            AllocConsole();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {



            Monitor monitor = new Monitor();
            Task t = new Task(() =>
            {
                monitor.fileWatcher();
            });
            t.Start();


            logger.Trace($"fileWatcher開始執行...");


           FreeConsole();

        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
