using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ParallelProgramming
{
    class Program
    {
        static void Main(string[] args)
        {
            PlinqTest();
            ParallelTest();
            BeatRecordTest();
        }

        private static void PlinqTest()
        {
            var source = Enumerable.Range(0, 3000000).ToList();

            var normalQuery1 = source.Where(n => Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0));
            var plinqQuery1 = source.AsParallel().Where(n => Enumerable.Range(2, (int)Math.Sqrt(n)).All(i => n % i > 0));

            var sw = Stopwatch.StartNew();
            foreach (var n in normalQuery1) { }
            sw.Stop();

            Console.WriteLine($"Total LINQ query time: {sw.ElapsedMilliseconds} ms");

            sw = Stopwatch.StartNew();
            foreach (var n in plinqQuery1) { }
            sw.Stop();

            Console.WriteLine($"Total PLINQ query time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine("Great! :)");

            var normalQuery2 = source.Where(n => n % 7 == 0);
            var plinqQuery2 = source.AsParallel().Where(n => n % 7 == 0);

            sw = Stopwatch.StartNew();
            foreach (var n in normalQuery2) { }
            sw.Stop();

            Console.WriteLine($"Total LINQ query time: {sw.ElapsedMilliseconds} ms");

            sw = Stopwatch.StartNew();
            foreach (var n in plinqQuery2) { }
            sw.Stop();

            Console.WriteLine($"Total PLINQ query time: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine("Not so great :(");
        }

        private static void ParallelTest()
        {
            Parallel.Invoke(
                () => new WebClient().DownloadFile("http://www.google.com", "google.html"),
                () => new WebClient().DownloadFile("http://www.is.muni.cz", "muni.html"));

            var even = 0;
            Parallel.For(0, 100, i =>
            {
                if (i % 2 == 0) even++;
            });
            Console.WriteLine($"Even numbers: {even}");

            Parallel.ForEach("PV178 is great", (c, loopstate) =>
            {
                if (c.Equals('8'))
                {
                    loopstate.Break();
                }
                else
                {
                    Console.Write(c);
                }
            });
            Console.WriteLine();
        }

        /// <summary>
        /// Napíšte PLINQ dotaz, ktorý bude trvať kratšie ako tento LINQ dotaz na
        /// kolekcií table a vráti ten istý výsledok.
        /// 
        /// Pre cviciacich: Uloha na to, aby si studenti uvedomili, ze PLINQ 
        /// nezachovava poradie pri LINQ metodach, ktore ho normalne zachovavaju.
        /// Nech skusia prist na to samy, ze tam treba AsOrdered
        /// </summary>
        private static void BeatRecordTest()
        {
            //crazy init
            MD5 md5Hasher = MD5.Create();
            var table = new List<Tuple<int, int>>();
            for (int i = 0; i < 10000; i++)
            {
                table.Add(
                    new Tuple<int, int>(
                        Math.Abs(BitConverter.ToInt32(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(i.ToString())), 0) % 100),
                        Math.Abs(BitConverter.ToInt32(md5Hasher.ComputeHash(Encoding.UTF8.GetBytes((i + 2798).ToString())), 0) % 100)));
            }

            var sw = Stopwatch.StartNew();

            var linqQuery = table
                .Where(n => Enumerable.Range(2, (int) Math.Sqrt(n.Item1)).All(i => n.Item1 % i > 0))
                .Take(1000)
                .ToList();
            
            sw.Stop();
            var linqTime = sw.ElapsedMilliseconds;

            sw = Stopwatch.StartNew();
            var plinqQuery = table
                .AsParallel()
                .AsOrdered()
                .Where(n => Enumerable.Range(2, (int) Math.Sqrt(n.Item1)).All(i => n.Item1 % i > 0))
                .Take(1000)
                .ToList();
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
