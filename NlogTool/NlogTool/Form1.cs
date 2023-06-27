using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NlogTool
{
    public partial class Form1 : Form
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // log here
                logger.Trace("Trace");
                logger.Debug("Debug");
                logger.Info("Info");
                logger.Warn("Warn");
                logger.Error("Error");
                logger.Fatal("Fatal");

                int div = 0;
                div /= div;     //故意讓系統出現例外

            }
            catch (Exception ex)
            {
                // log with exception here
                logger.Trace(ex, "Trace");
                logger.Debug(ex, "Debug");
                logger.Info(ex, "Info");
                logger.Warn(ex, "Warn");
                logger.Error(ex, "Error");
                logger.Fatal(ex, "Fatal");

            }
            MessageBox.Show("執行完成");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
