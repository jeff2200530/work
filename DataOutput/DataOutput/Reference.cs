using DataOutput.Format;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataOutput
{
    public class Reference
    {
        public string _secDB = null;
        public string _futDB = null;
        public string _secStock = null;
        public string _futCommodity = null;
        public string _secOrignMapping = null;
        public string _futOrignMapping = null;
        public Dictionary<string, string> _stockDic;
        public Dictionary<string, string> _commodityDic;
        public Dictionary<string, string> _secOrignDic;
        public Dictionary<string, string> _futOrignDic;
        private static readonly object syncRoot = new object();
        private static Reference instance;
        public FileWriter _writer = FileWriter.GetInstance();
        private Reference()
        {
            SetData();
            //ReSetStock();
            //ReSetCommodity();
            GetStock();
            GetFutureCommodity();
            GetSecOrign();
            GetFutOrign();

        }
        public static Reference GetInstance()
        {
            if (instance == null) //確保當前沒有這個物件存在
            {

                lock (syncRoot)//鎖住當前狀態
                {
                    if (instance == null)//再次確認該物件不存在
                    {
                        instance = new Reference();
                    }
                }
            }
            return instance;
        }
        public void SetData()
        {
            _secDB = ConfigurationManager.AppSettings["cube_sec_sinopac"];
            _futDB = ConfigurationManager.AppSettings["cube_fut_sinopac"];

            _secOrignMapping = ConfigurationManager.AppSettings["secOrignMapping"];
            _futOrignMapping = ConfigurationManager.AppSettings["futOrignMapping"];

            _secStock = ConfigurationManager.AppSettings["secStock"];
            _futCommodity = ConfigurationManager.AppSettings["futCommodity"];
        }
        public void ReSetStock()
        {
            SqlConnection secDB = new SqlConnection(_secDB);
            DataTable stockDataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("select stock,stockname from mstmb", secDB);
            try
            {
                secDB.Open();
                StringBuilder _logMessage = new StringBuilder();
                _logMessage.AppendLine($"{DateTime.Now} secSql連線成功");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "\\log.txt" });
            }
            catch
            {
                StringBuilder _logMessage = new StringBuilder();
                _logMessage.AppendLine($"{DateTime.Now} secSql連線失敗");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "\\log.txt" });

            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(stockDataTable);
            secDB.Close();
            using (StreamWriter sw = File.CreateText($"secStock.txt"))
            {
                StringBuilder _content = new StringBuilder();
                for (int j = 0; j < stockDataTable.Rows.Count; j++)
                {
                    _content.AppendLine($"{stockDataTable.Rows[j][0].ToString().Trim()},{stockDataTable.Rows[j][1].ToString().Trim()}");
                }
                sw.Write(_content);
            }

        }
        public void GetStock()
        {
            _stockDic = new Dictionary<string, string>();
            StreamReader sr = File.OpenText(_secStock);
            string[] array = new string[2];
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                array = nextLine.Split(',');
                _stockDic.Add(array[0], array[1]);
            }
        }
        public void ReSetCommodity()
        {
            SqlConnection futDB = new SqlConnection(_futDB);
            DataTable commodityDataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("select omcomno,comname from mcommodity", futDB);

            try
            {
                futDB.Open();
                StringBuilder _logMessage = new StringBuilder();
                _logMessage.AppendLine($"{DateTime.Now} futSql連線成功");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "\\log.txt" });
            }
            catch
            {
                StringBuilder _logMessage = new StringBuilder();
                _logMessage.AppendLine($"{DateTime.Now} futSql連線失敗");
                _writer._logQueue.Enqueue(new LogFormat() { content = _logMessage.ToString(), path = "\\log.txt" });

            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(commodityDataTable);
            futDB.Close();
            using (StreamWriter sw = File.CreateText($"futCommodity.txt"))
            {
                StringBuilder _content = new StringBuilder();
                for (int j = 0; j < commodityDataTable.Rows.Count; j++)
                {
                    _content.AppendLine($"{commodityDataTable.Rows[j][0].ToString().Trim()},{commodityDataTable.Rows[j][1].ToString().Trim()}");
                }
                sw.Write(_content);
            }
        }
        public void GetFutureCommodity()
        {
            _commodityDic = new Dictionary<string, string>();

            StreamReader sr = File.OpenText(_futCommodity);
            string[] array = new string[2];
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                array = nextLine.Split(',');
                _commodityDic.Add(array[0], array[1]); 
            }
        }
        public void GetSecOrign()
        {
            _secOrignDic = new Dictionary<string, string>();
            StreamReader sr = File.OpenText(_secOrignMapping);
            string[] array = new string[2];
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                array = nextLine.Split(',');
                _secOrignDic.Add(array[0], array[1]);

            }
        }
        public void GetFutOrign()
        {
            _futOrignDic = new Dictionary<string, string>();
            StreamReader sr = File.OpenText(_futOrignMapping);
            string[] array = new string[2];
            string nextLine;
            while ((nextLine = sr.ReadLine()) != null)
            {
                array = nextLine.Split(',');
                _futOrignDic.Add(array[0], array[1]);
            }
        }
    }
}
