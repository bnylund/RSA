using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Math;

namespace RSA
{
    public class Utils
    {
        public static List<long> FindFactors(long num)
        {
            List<long> result = new List<long>();

            // Take out the 2s.
            while (num % 2 == 0)
            {
                result.Add(2);
                num /= 2;
            }

            // Take out other primes.
            long factor = 3;
            while (factor * factor <= num)
            {
                if (num % factor == 0)
                {
                    // This is a factor.
                    result.Add(factor);
                    num /= factor;
                }
                else
                {
                    // Go to the next odd number.
                    factor += 2;
                }
            }

            // If num is not 1, then whatever is left is prime.
            if (num > 1) result.Add(num);

            return result;
        }

        public static long PhiN(long p, long q)
        {
            return (p - 1) * (q - 1);
        }

        public static long N(long p, long q)
        {
            return p * q;
        }

        public static BigInteger D(double e, double phiN)
        {
            return new BigInteger(e + "").ModPow(new BigInteger("-1"), new BigInteger(phiN + ""));
        }

        public static BigInteger Decrypt(double cipher, double d, double n) => new BigInteger(cipher + "").ModPow(new BigInteger(d + ""), new BigInteger(n + ""));

        public static BigInteger PowerMod(double _base, BigInteger exponent, double modOf) => new BigInteger(_base + "").ModPow(new BigInteger(exponent + ""), new BigInteger(modOf + ""));
    }
}