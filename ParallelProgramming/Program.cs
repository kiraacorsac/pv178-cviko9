using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            BeatRecordTest();
        }

        /// <summary>
        /// Napíšte PLINQ dotaz, ktorý bude trvať kratšie ako tento LINQ dotaz na
        /// kolekcií table a vráti ten istý výsledok.
        /// </summary>
        private static void BeatRecordTest()
        {
            //crazy init
            MD5 md5Hasher = MD5.Create();
            var table = new List<Tuple<int, int>>();
            for (var i = 0; i < 10000; i++)
            {
                table.Add(
                    new Tuple<int, int>(
                        Math.Abs(BitConverter.ToInt32(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(i.ToString())), 0) % 100),
                        Math.Abs(BitConverter.ToInt32(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes((i + 2798).ToString())), 0) % 100)));
            }

            var sw = Stopwatch.StartNew();

            var linqQuery = table
                .Where(n => Enumerable.Range(2, (int)Math.Sqrt(n.Item1)).All(i => n.Item1 % i > 0))
                .Take(1000)
                .ToList();

            sw.Stop();
            var linqTime = sw.ElapsedMilliseconds;

            sw = Stopwatch.StartNew();

            var plinqQuery = table.ToList(); // TODO

            sw.Stop();
            var plinqTime = sw.ElapsedMilliseconds;

            if (!plinqQuery.SequenceEqual(linqQuery))
            {
                Console.WriteLine("They are not the same results!");
                return;
            }
            if (linqTime > plinqTime)
            {
                Console.WriteLine("Linq is faster!");
                return;
            }
            Console.WriteLine("Congratulations :)");
        }
    }
}
