using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization
{
    class Program
    {
        static void Main(string[] args)
        {
            //MutexTest();
            HospitalTest();
        }

        private static void MutexTest()
        {
            using (var mutex = new Mutex(false, "PV178"))
            {
                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false))
                {
                    Console.WriteLine("Another instance of this app is running.");
                    Console.ReadLine();
                }
                else
                {
                    RunProgram();
                }
            }
        }

        private static void RunProgram()
        {
            Console.WriteLine("I am running");
            Console.ReadLine();
        }

        private static void HospitalTest()
        {
            var hospital = new Hospital();
            hospital.Process10Patients();
        }
    }
}
