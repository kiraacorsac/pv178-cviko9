using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization
{
    /// <summary>
    /// Za úlohu bude implementovať obsluhu pacientov v nemocnici.
    /// V nemocnici máme troch všeobecných doktorov.
    /// Poradie pacientov nie je deterministické (kto si prvý odchytí sestričku, ten ide)
    /// Každý sa zdrží u doktora náhodný čas medzi 1000 až 2000 milisekúnd
    /// (máme fakt šikovných doktorov).
    /// 
    /// </summary>
    public class Hospital
    {
        private readonly SemaphoreSlim sem = new SemaphoreSlim(3);
        private readonly Random rnd = new Random();

        public void Process10Patients()
        {
            var patients = new List<Task>();

            for (var order = 1; order <= 10; order++)
            {
                //don't close over the loop variable
                var orderCopy = order;
                var patient = new Task(() => Enter(orderCopy));
                patient.Start();
                patients.Add(patient);
            }
            Task.WhenAll(patients).Wait();

        }
        private void Enter(object id)
        {
            Console.WriteLine(id + ". patient wants to enter");
            sem.Wait();
            Console.WriteLine(id + ". patient is in!");
            Thread.Sleep(rnd.Next(1000, 2001));
            Console.WriteLine(id + ". patient is fixed");
            sem.Release();
        }
    }
    /*
    public class Hospital
    {
        private readonly object lockObject = new object();
        private const int maxPatientsAllowed = 3;
        private int actualPacientsCount;
        private readonly Random rnd = new Random();

        public void Process10Patients()
        {
            for (var i = 1; i <= 10; i++)
            {
                new Thread(Enter).Start(i);
            }
        }

        private void Enter(object id)
        {
            Console.WriteLine(id + ". wants to enter");
            while (true)
            {
                lock (lockObject)
                {
                    if (actualPacientsCount < maxPatientsAllowed)
                    {
                        actualPacientsCount++;
                        break;
                    }
                }
                Thread.Sleep(100);
            }

            Console.WriteLine(id + ". is in!");
            Thread.Sleep(rnd.Next(1000, 2001));
            Console.WriteLine(id + ". is fixed");

            lock (lockObject)
            {
                actualPacientsCount--;
            }
        }
    }*/
}