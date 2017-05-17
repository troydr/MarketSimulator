using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    public class Randomizer
    {
        //Function to get random number
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        
        public static int Generate(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }

        public static int Generate(int max)
        {
            lock (syncLock)
            {
                return random.Next(max);
            }
        }
        public static int Generate()
        {
            lock (syncLock)
            {
                return random.Next();
            }
        }

        public static double GenerateDouble()
        {
            lock (syncLock)
            {
                return random.NextDouble();
            }
        }



    }
}
