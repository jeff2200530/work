﻿using DataInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackupTool
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
            Backup b = new Backup();
            b.backup();
        }
     

        private void button2_Click_1(object sender, EventArgs e)
        {
            Backup b = new Backup();
            b.backup();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
