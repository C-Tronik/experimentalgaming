using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Slot_api.estrazione.egypt
{
    public class Egypt_slot : Egypt_slot_estraction
    {
        private readonly int[] numeri = new int[] { 0, 1, 1, 1, 4, 3, 3, 3, 3, 4, 4, 2, 2, 2, 2 };

        

        public Egypt_slot_estraction Estrazione_Egypt_Slot()
        {
            //var rand = new Random();
            var random = RandomNumberGenerator.Create();
            Egypt_slot_estraction estrazione = new Egypt_slot_estraction
            {
                
                First_line = new int[3] { numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)] },
                Second_line = new int[3] { numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)] },
                Tre_line = new int[3] { numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)] },
                Fourty_line = new int[3] { numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)] },
                Fifty_line = new int[3] { numeri[Next(random, 0, numeri.Length)], numeri[Next(random,0, numeri.Length)], numeri[Next(random,0, numeri.Length)] }
                // OR 
                //First_line = new int[3] { numeri[rand.Next(0, numeri.Length)] /////// },
            };
            // Controllare la vincita
            return estrazione;
                
        }

        private int Next( RandomNumberGenerator generator, int min, int max)
        {
            
            max -= 1;

            var bytes = new byte[sizeof(int)]; // 4 bytes
            generator.GetNonZeroBytes(bytes);
            
            var val = BitConverter.ToInt32(bytes);

            var result = ((val - min) % (max - min + 1) + (max - min + 1)) % (max - min + 1) + min;
            return result;

        }

        

    }
}
