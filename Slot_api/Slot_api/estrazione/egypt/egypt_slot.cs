using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slot_api.estrazione.egypt
{
    public class Egypt_slot : Egypt_slot_estraction
    {
        private readonly int[] numeri = new int[] { 0, 1, 1, 1, 4, 3, 3, 3, 3, 4, 4, 2, 2, 2, 2 };

        
        public Egypt_slot_estraction Estrazione_Egypt_Slot()
        {
            var rand = new Random();
            Egypt_slot_estraction estrazione = new Egypt_slot_estraction
            {
                First_line = new int[5] { numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)] },
                Second_line = new int[5] { numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)] },
                Tre_line = new int[5] { numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)], numeri[rand.Next(0, numeri.Length)] }
            };
            // Controllare la vincita
            return estrazione;
                
        }

    }
}
