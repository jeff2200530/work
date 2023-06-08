using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Configuration;

namespace UserSecret
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        public Form1()
        {
            AllocConsole();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile(@"appsettings.json", false, true) //讀取設定檔
            .AddUserSecrets("123"); //指定項目ID
            var config = builder.Build();

            string cube_sys = config[$"database:0:connection:cube_sys"];
            string cube_sec = config[$"database:0:connection:cube_sec"];
            string cube_fut = config[$"database:0:connection:cube_fut"];
            string oracle_query = config[$"database:1:connection:oracle_query"];
            Console.WriteLine($"cube_sys = {cube_sys}");
            Console.WriteLine($"cube_sec = {cube_sec}");
            Console.WriteLine($"cube_fut = {cube_fut}");
            Console.WriteLine($"cube_fut1 = {oracle_query}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");
            var config = builder.Build();
            Console.WriteLine($"AppId = {config["service"]}");
         
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            int  amoung = 123;
            string s = $"總共{0}",1234;
            MessageBox.Show(s);
        }
    }
}
