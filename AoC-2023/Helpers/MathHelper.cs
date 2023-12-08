using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2023.Helpers
{
    public static class MathHelper
    {
        public static long[] AllPrimeNumberSmallerThen(long number)
        {
            List<long> primes = new List<long>();

            for( long i = 1; i <= number; i++ )
            {
                if (IsPrimeNumber(i))
                {
                    primes.Add(i);
                }
            }

            return primes.ToArray();
        }

        public static bool IsPrimeNumber(long number)
        {
            for(long i = 2; i <= number/2; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        // ro: cel mai mic multiplu comun
        public static long LCM(long[] numbers)
        {
            Dictionary<long,long> fact = new Dictionary<long,long>();

            long rez = 1;
            for(int i =  0; i < numbers.Length; i++)
            {
                long num = numbers[i];

                if (IsPrimeNumber(num))
                {
                    if (!fact.ContainsKey(num))
                    {
                        fact.Add(num, 1);
                    }

                    if (fact[num] < 1)
                    {
                        fact[num] = 1;
                    }
                }
                else
                {
                    long[] primes = AllPrimeNumberSmallerThen(num);
                    long currentUsedPrimPoz = 1;

                    long currentPrimPowerCounter = 0;

                    while (num > 0)
                    {
                        if (num == 1 || num % primes[currentUsedPrimPoz] != 0)
                        {
                            if(currentPrimPowerCounter > 0)
                            {
                                if (!fact.ContainsKey(primes[currentUsedPrimPoz]))
                                {
                                    fact.Add(primes[currentUsedPrimPoz], 1);
                                }

                                if (fact[primes[currentUsedPrimPoz]] < currentPrimPowerCounter)
                                {
                                    fact[primes[currentUsedPrimPoz]] = currentPrimPowerCounter;
                                }
                            }

                            if (num == 1)
                            {
                                break;
                            }

                            currentPrimPowerCounter = 0;
                            currentUsedPrimPoz++;
                        }
                        else
                        {
                            num = num / primes[currentUsedPrimPoz];
                            currentPrimPowerCounter++;
                        }
                    }
                }
            }

            foreach(var f in fact)
            {
                if(f.Value != 0 && f.Key != 0)
                {
                    rez = rez * (Int64)Math.Pow(f.Key, f.Value);
                }
            }

            return rez;
        }

        public static long MaxNumberOfList(long[] numbers)
        {
            throw new NotImplementedException();
        }
    }
}
