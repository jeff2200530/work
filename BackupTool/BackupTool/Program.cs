using DataInput;
using System;
using Systex.Cube.Log.Spec;
using Systex.Cube.Models;

namespace BackupTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Backup process = new Backup();
            process.backup();
            Console.ReadLine();
            //LogFormat l = new LogFormat()
            //{
            //    level = "0",
            //    processName = "A",
            //    functionName = "F",
            //    endTime = "20230614",
            //    key = "key",
            //    startTime = "20230610",
            //    message = "破麻"


            //};
            //FileWriterProcessor f = new FileWriterProcessor();
            //f.Write(l);
        }
    }
}
