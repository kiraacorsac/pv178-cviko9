using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    namespace Threads
    {
        /// <summary>
        /// Úkolem bude napsat program countdown, tedy nějaký odpočet.
        /// Countdown bude na začátku mít počet tiků, a rozmezí mezi tiky v sekundách.
        /// Za každý tik se zavolá funkce, specifikovaná v konstruktoru.
        /// Na konci se zavolá jiná funkce, také specifikovaná v konstruktoru.
        /// Countdown poběží na samostatném vláknu.
        /// </summary>
        public class Countdown
        {
            /// <summary>
            /// Konstruktor třídy Countdown.
            /// </summary>
            /// <param name="duration">Trvání (v ticích)</param>
            /// <param name="secondsPerTick">Rozmezí mezi tiky (v sekundách)</param>
            /// <param name="endAct">Funkce na konci</param>
            /// <param name="tickAct">Funkce při tiku</param>
            public Countdown(int duration, int secondsPerTick, Action endAct, Action tickAct)
            {

            }

            /// <summary>
            /// Jeden tik
            /// </summary>
            private void Tick()
            {
            }

            /// <summary>
            /// Zapnutí odpočtu.
            /// </summary>
            public void Start()
            {

            }

            /// <summary>
            /// Vypnutí odpočtu.
            /// </summary>
            private void Stop()
            {
            }

            /// <summary>
            /// Vlastnost popisující, zda odpočet běží.
            /// </summary>
            public bool Enabled { get; private set; }
        }
    }
}
