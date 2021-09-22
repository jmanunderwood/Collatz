using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.CommandLineUtils;

namespace Collatz
{
    class Collatz
    {
        public static List<int> LeadingDigits(int[] numbers)
        {
            List<int> digits = new List<int>();
            for(int i = 0; i < numbers.Length; i++)
            {
                digits.Add((int)(numbers[i] / Math.Pow(10, (int)Math.Floor(Math.Log10(numbers[i]))))); //pain
            }
            return digits;
        }
        public static List<int> CollatzNumbers(int initial)
        {
            List<int> collatzNumbers = new List<int>();
            int c = initial;
            while (c != 1)
            {
                if (c % 2 == 0) //even
                {
                    c /= 2;
                }
                else //odd
                {
                    c = (c * 3) + 1;
                }
                collatzNumbers.Add(c);
            }

            return collatzNumbers;
        }

    }
}
