using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Synchronization
{
    /// <summary>
    /// Za úlohu bude implementovať obsluhu pacientov v nemocnici.
    /// V nemocnici máme troch všeobecných doktorov.
    /// Poradie pacientov nie je deterministické (kto si prvý odchytí sestričku, ten ide)
    /// Každý sa zdrží u doktora náhodný čas medzi 1000 až 2000 milisekúnd
    /// (máme fakt šikovných doktorov).
    /// </summary>
    public class Hospital
    {
        /// <summary>
        /// Vytvorí 10 threadov, kde každý thread reprezentuje pacienta.
        /// Vytvorenie, odchytenie sestričky a úspešné obslúženie pacienta
        /// sú sprevádzané informatívnym výpisom do konzoly.
        /// </summary>
        public void Process10Patients()
        {
            
        }
    }
}
