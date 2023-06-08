using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class FileWriter
    {
        public string _filePath = null;
        public FileWriter()
        {
            SetBaseData();

        }

        public void SetBaseData()
        {
            _filePath = ConfigurationManager.AppSettings["filePath"];
        }

        public void WriteTxt(string content,string fileName)
        {
            //if (!File.Exists(_filePath+fileName))
            //{
            //    File.Create(_filePath + fileName);

            //}

          
                    using (StreamWriter sw = File.AppendText(_filePath + fileName))
                    {
                        sw.Write(content);
                        sw.Close();
                    }
          
          
           
               
            
        }


    }
}
