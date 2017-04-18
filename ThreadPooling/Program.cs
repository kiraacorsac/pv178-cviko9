using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPooling
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPoolingTest();
            TaskTest();
        }

        private static void ThreadPoolingTest()
        {
            ThreadPool.QueueUserWorkItem(Write, 123);
            ThreadPool.QueueUserWorkItem(Write);

            Thread.Sleep(1000);
        }

        private static void Write(object data)
        {
            Console.WriteLine($"I write this {data}");
        }

        private static void TaskTest()
        {
            Task<string> task1 = Task.Factory.StartNew(() => DownloadString("http://stackoverflow.com/"));
            Task<string> task2 = Task.Factory.StartNew(() => DownloadString("http://is.muni.cz/"));

            for (var i = 0; i < 10; i++)
            {
                Thread.Sleep(100);
            }

            //task2.Wait();
            Console.WriteLine(task2.Result);
        }

        private static string DownloadString(string uri)
        {
            using (var wc = new System.Net.WebClient())
            {
                return wc.DownloadString(uri);
            }
        }
    }
}
