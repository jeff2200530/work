using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMonitor
{
    public class MainProcessorBase
    {
        public string []_source = null;
        public string _filePath = null;
        public string _connectString = null;
        public SqlConnection _db = null;
        public MainProcessorBase()
        {
            SetBaseData();
        }
        public void SetBaseData()
        {
            _source = ConfigurationManager.AppSettings["source"].ToLower().Split(",");
            _filePath = ConfigurationManager.AppSettings["filePath"];
            _connectString = ConfigurationManager.AppSettings["SGTP_TRADE"];
            _db = new SqlConnection(_connectString);

        }
    }
}
