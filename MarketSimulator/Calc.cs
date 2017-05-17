using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;  

namespace MarketSimulator
{
    static class Calc
    {
        static MersenneTwister mersenneTwister = new MersenneTwister();
        //static Random random = new Random();
        //static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        static public double PercentChange(double newVal, double intialVal)
        {
            return (newVal - intialVal) / intialVal;
        }

        // RPD (relative percent difference)
        // assuming x1 x2 both positive then
        // rpd = abs(x1 - x2) / ((x1+x2)/2)
        static public double RelativePercentDifference(double x1, double x2)
        {
            return (Math.Abs(x1 - x2)) / ((x1 + x2) / 2);
        }

        // percent error: Exp(Experimental value) Acpt(Accepted Value)
        // = (Exp - Acpt)/ Acpt



        // Fisher-Yates shuffle     
        // To shuffle an array a of n elements:
        // for i from n − 1 downto 1 do
        //   j ← random integer with 0 ≤ j ≤ i
        //   exchange a[j] and a[i]
        public static void Shuffle1<T>(this IList<T> list)
        {
            try
            {
                int n = list.Count;

                for (int i = n - 1; i > 0; i--)
                {
                    int j = RandomInt(i); //random integer with 0 ≤ j ≤ i  NOTE: can swap with itself

                    //swap values
                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            try
            {
                int n = list.Count;

                for (int i = n - 1; i > 0; i--)
                {
                    int j = RandomInt(i); //random integer with 0 ≤ j ≤ i  NOTE: can swap with itself

                    //swap values
                    T temp = list[i];
                    list[i] = list[j];
                    list[j] = temp;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }


        public static void RandomSeed(ulong seed)
        {
            mersenneTwister.init_genrand(seed);
        }
        public static int RandomInt(int min, int max)
        {
            int result = (int)mersenneTwister.genrand_uint32();
            result = Math.Abs(result);
            result = (result % (1 + max - min)) + min;

            return result;
        }

        public static int RandomInt(int max)
        {
            int result = (int)mersenneTwister.genrand_uint32();
            result = Math.Abs(result);
            result = result % (1 + max);

            return result;
        }

        /*
          public static int RandomInt(int max)
          {
              //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
              byte[] buffer = new byte[4];

              rng.GetBytes(buffer);
              int result = BitConverter.ToInt32(buffer, 0);
              result = Math.Abs(result);
              result = result % (1 + max);

              return result;
          }

        
          public static int RandomInt()
          {
              //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
              byte[] buffer = new byte[4];

              rng.GetBytes(buffer);
              int result = BitConverter.ToInt32(buffer, 0);
              result = Math.Abs(result);

              return result;
          }
       
          public static int RandomInt(int min, int max)
          {
              //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
              byte[] buffer = new byte[4];

              rng.GetBytes(buffer);
              int result = BitConverter.ToInt32(buffer, 0);
              result = Math.Abs(result);
              result = (result % (1 + max - min)) + min;

              return result;
          }
        
         public static int RandomInt(int max)
          {
              //RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
              byte[] buffer = new byte[4];

              rng.GetBytes(buffer);
              int result = BitConverter.ToInt32(buffer, 0);
              result = Math.Abs(result);
              result = result % (1 + max);

              return result;
          }
           *  */


    }
}
