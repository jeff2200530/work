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

namespace DataMonitor
{
    public class Reference
    {
        public string _secDB = null;
        public string _futDB = null;
        public string _secOrignMapping = null;
        public string _furOrignMapping = null;
        public   Dictionary<string, string> _stockDic;
        public  Dictionary<string, string> _commodityDic;
        public  Dictionary<string, string> _secOrignDic;
        public  Dictionary<string, string> _futOrignDic;
        private static readonly object syncRoot = new object();
        private static Reference instance;
        private Reference() {
            SetData();
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
        public void SetData() {

            _secDB = ConfigurationManager.AppSettings["cube_sec_sinopac"];
            _futDB = ConfigurationManager.AppSettings["cube_fut_sinopac"];

            _secOrignMapping = ConfigurationManager.AppSettings["secOrignMapping"];
            _furOrignMapping = ConfigurationManager.AppSettings["futOrignMapping"];

        }
        public void GetStock()
        {
            SqlConnection secDB = new SqlConnection(_secDB);
            DataTable stockDataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("select stock,stockname from mstmb", secDB);
            _stockDic = new Dictionary<string, string>();
            secDB.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(stockDataTable);
            for (int i = 0; i < stockDataTable.Rows.Count; i++)
            {
                if (!_stockDic.ContainsKey(stockDataTable.Rows[i][0].ToString().Trim()))
                {
                    _stockDic.Add(stockDataTable.Rows[i][0].ToString().Trim(), stockDataTable.Rows[i][1].ToString().Trim());
                }   
            }
            secDB.Close();
        }
        public void GetFutureCommodity()
        {
            SqlConnection futDB = new SqlConnection(_futDB);
            DataTable commodityDataTable = new DataTable();
            SqlCommand sqlCommand = new SqlCommand("select omcomno,comname from mcommodity", futDB);
            _commodityDic = new Dictionary<string, string>();
            futDB.Open();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(commodityDataTable);
            for (int i = 0; i < commodityDataTable.Rows.Count; i++)
            {
                if (!_commodityDic.ContainsKey(commodityDataTable.Rows[i][0].ToString().Trim()))
                {
                    _commodityDic.Add(commodityDataTable.Rows[i][0].ToString().Trim(), commodityDataTable.Rows[i][1].ToString().Trim());
                }
            }
            futDB.Close();

        }
        public void GetSecOrign() {
            _secOrignDic = new Dictionary<string, string>();
            StreamReader sr = File.OpenText(_secOrignMapping);
            string []array= new string [2];
            string nextLine;
            while ((nextLine =  sr.ReadLine()) != null)
            {
                array = nextLine.Split(',');
                _secOrignDic.Add(array[0], array[1]);

            }
        }
        public void GetFutOrign() {

            _futOrignDic = new Dictionary<string, string>();
            StreamReader sr = File.OpenText(_furOrignMapping);
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
