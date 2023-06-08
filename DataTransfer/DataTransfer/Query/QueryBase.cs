using DataTransfer.Format;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Query
{
    public abstract class QueryBase : MainProcessorBase
    {






        public FileWriter _Writer = new FileWriter();
        public FormFormat _input = null;
        

        public StringBuilder _content = new StringBuilder();
        public string _queryBhnoCommand = null;
        public string []_bhnoArray =new string[0];
        public string _queryCommand = null;
        public string _date = null;
        public string _company =null;
        public string _order = null;
        public string _table = null;
        
        public QueryBase()
        {
             Directory.CreateDirectory($"{_filePath}");

            
            foreach (var table in new[] {"hmtht","hodrt","trafuhord","trafuhtrd"}) {
                Directory.CreateDirectory($"{_filePath}\\{table}");
                Directory.CreateDirectory($"{_filePath}\\{table}\\file");
                Directory.CreateDirectory($"{_filePath}\\{table}\\log");
            }
           
        }
        public abstract void Query();

        public abstract void SetBaseData(FormFormat input);

        public abstract void toString();
        public abstract string  GetProperties();

        public void Insert(DataTable dt, string tableName)
        {

            _db.Open();

            //using (var transaction = _db.BeginTransaction())
            //{
            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(_db/*, SqlBulkCopyOptions.Default, transaction*/))
            {
                try
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            if (i == 2)
                                Console.WriteLine(dt.Rows[j][i]);


                        }
                    }
                    Console.WriteLine(dt.Rows[0][0]);
                    sqlBulkCopy.BatchSize = 1000;
                    sqlBulkCopy.BulkCopyTimeout = 1;

                    sqlBulkCopy.DestinationTableName = "test";
                    sqlBulkCopy.WriteToServer(dt);
                    Console.WriteLine($"成功筆數{dt.Rows.Count}");

                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    //transaction.Rollback();
                    Console.WriteLine($"寫入失敗");
                }
            }

            //}

            _db.Close();


        }

      
    }
}
