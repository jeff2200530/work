
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using DataTransfer.Format;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace DataTransfer
{
    public partial class Form1 : Form
    {
        [DllImport("Kernel32")]
        public static extern void FreeConsole();
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();


        public string _processName = null;
        public string _column = null;
        public string _startDate = null;
        public string _endDate = null;

        public FormFormat _input = null;
        public FileWriter _fileWriter = new FileWriter();
        public MainProcessor _process = null;
        public InsertProcessor _insertProcess = null;
        public DataProcessor d = null;


        public Form1()
        {
            AllocConsole();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {




            //設定參數
            _processName = comboBox1.SelectedItem.ToString();
            _column = GetColumn();
            GetDate(out _startDate, out _endDate);
            string function = "query";



            _input = new FormFormat { processName = _processName, column = _column, function = function, startDate = _startDate, endDate = _endDate };

            _process = new MainProcessor(_input);



            //DirectoryInfo dirInfo = new DirectoryInfo($"{_process._filePath}\\{_input.processName}\\file\\{_input.startDate.Replace("/", "")}");

            //FileInfo[] files = dirInfo.GetFiles();
            //d = new DataProcessor();
            //if (files.Length != 0)
            //    dataGridView1.DataSource = d.TxtToDt(files[0].FullName);
            //else
            //    dataGridView1.DataSource = d.TxtToDt($"{_process._filePath}\\{_input.processName}");
            MessageBox.Show("執行完成!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //清除checkedlistbox
            checkedListBox1.Items.Clear();

            //設定參數
            string processName = comboBox1.SelectedItem.ToString();
            string column = "";
            string function = "getProperties";
            _input = new FormFormat { processName = processName, column = column, function = function, startDate = "20000101", endDate = "20000101" };
            _process = new MainProcessor(_input);


            //填入checkedlistbox

            foreach (var property in _process._properties.Split(','))
            {
                checkedListBox1.Items.Add(property);
            }


            if (comboBox1.SelectedItem.ToString() != "")
            {
                checkBox1.Enabled = true;
                checkedListBox1.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
                button1.Enabled = true;

            }
        }
        /// <summary>
        /// 取得勾選的清單
        /// </summary>
        /// <returns></returns>
        public string GetColumn()
        {
            string column = "";
            foreach (var item in checkedListBox1.CheckedItems)
            {

                column += item.ToString() + ",";
            }
            if (column.EndsWith(","))
                column = column.Remove(column.Length - 1);
            return column;
        }
        public void GetDate(out string startDate, out string endDate)
        {
            string startYear = dateTimePicker1.Value.Year.ToString();
            string startMonth = dateTimePicker1.Value.Month.ToString().PadLeft(2, '0');
            string startDay = dateTimePicker1.Value.Day.ToString().PadLeft(2, '0');
            string endYear = dateTimePicker2.Value.Year.ToString();
            string endMonth = dateTimePicker2.Value.Month.ToString().PadLeft(2, '0');
            string endDay = dateTimePicker2.Value.Day.ToString().PadLeft(2, '0');

            switch (comboBox1.SelectedItem)
            {
                case ("trafuhtrd"):
                    startDate = startYear + "/" + startMonth + "/" + startDay;
                    endDate = endYear + "/" + endMonth + "/" + endDay;
                    break;
                case ("trafuhord"):
                    startDate = startYear + "/" + startMonth + "/" + startDay;
                    endDate = endYear + "/" + endMonth + "/" + endDay;
                    break;
                default:
                    startDate = startYear + startMonth + startDay;
                    endDate = endYear + endMonth + endDay;
                    break;
            }

        }

        public void SetCheckedListBoxTrue()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, true);
        }
        public void SetCheckedListBoxFalse()
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, false);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                SetCheckedListBoxTrue();
            else
                SetCheckedListBoxFalse();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //d = new DataProcessor(_input);


        }

        private void button3_Click(object sender, EventArgs e)
        {
            //設定參數
            _processName = comboBox1.SelectedItem.ToString();
            _column = GetColumn();
            GetDate(out _startDate, out _endDate);
            string function = "insert";

            _input = new FormFormat { processName = _processName, column = _column, function = function, startDate = _startDate, endDate = _endDate };

            _insertProcess = new InsertProcessor(_input);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _process = new MainProcessor();
            if (_process._execute == "auto" & _process._tableName != null)
            {
                Console.WriteLine("finish!");
                this.Close();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (_process._execute == "auto" & _process._tableName != null)
            {

                this.Hide();

            }
            else
            {
                FreeConsole();
                    }



        }
    }
}
