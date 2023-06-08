using DataTransfer.Factory;
using DataTransfer.Format;
using DataTransfer.Insert;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class InsertProcessor : MainProcessorBase
    {
        public InsertBase _process = null;
        public DataProcessor _d = null;
        public FormFormat _input = null;
        public InsertProcessor(FormFormat input)
        {
            _input = input;
            DateTime nowDate = DateTime.ParseExact(input.startDate.Replace("/", ""), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime endDate = DateTime.ParseExact(input.endDate.Replace("/", ""), "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

            //startDate開始執行到endDate結束
            while (DateTime.Compare(nowDate, endDate) != 1)
            {
                //挑選實作
                //_process = InsertFactory.GetProcess(input);

                //選取執行function
                switch (input.function)
                {

                    case ("insert"):
                        ExecuteInsert();
                        break;

                    default:
                        break;
                }
                nowDate = nowDate.AddDays(1);
                input.startDate = nowDate.ToString("yyyyMMdd");
            }
        }
        public void ExecuteInsert()
        {
            DirectoryInfo dirInfo = new DirectoryInfo($"{_filePath}\\{_input.processName}\\{_input.startDate.Replace("/", "")}");
            FileInfo[] files = dirInfo.GetFiles();
            _d = new DataProcessor();

            if (files.Length != 0)
            {
                foreach (var file in files)
                {
                    DataTable dt =_d.TxtToDt(file.FullName);
                }
            }
        }
    }
}
