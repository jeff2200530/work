using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class MainProcessorBase
    {

        public DataProcessor d = null;


        //public string _column = null;
        public string _execute = null;
        public string _startTime = null;
        public string _endTime = null;
        public string _connectString = null;
        public string _filePath = null;
        public SqlConnection _db = null;
        
        public  StringBuilder _logMessage =null;
        public string []_tableName = null;

        public MainProcessorBase() {
            SetBaseData();
        
        }
        public void SetBaseData() {
            //_column = ConfigurationManager.AppSettings["column"];


            _execute = ConfigurationManager.AppSettings["execute"].ToLower();
            _tableName = ConfigurationManager.AppSettings["tableName"].ToLower().Split(",");
            //_startTime = ConfigurationManager.AppSettings["startTime"];
            //_endTime = ConfigurationManager.AppSettings["endTime"];
            _connectString = ConfigurationManager.AppSettings["connectString"];
            _filePath =ConfigurationManager.AppSettings["filePath"];
            _db = new SqlConnection(_connectString);
            
           
        }
    }
}
