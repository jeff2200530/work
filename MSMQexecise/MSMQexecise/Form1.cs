using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSMQexecise
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string queuePath = @".\Private$\queue";
            MessageQueue queue = new MessageQueue(queuePath);

            System.Messaging.Message message = new System.Messaging.Message();
            message.Body = "Hello, MSMQ!";

            queue.Send(message);
        }
       
    }
}
