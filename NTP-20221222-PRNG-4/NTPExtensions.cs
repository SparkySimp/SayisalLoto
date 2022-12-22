using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTP_20221222_PRNG_4
{
    public static class NTPExtensions
    {
        /// <summary>
        /// Stuffs the array with random numbers
        /// </summary>
        /// <param name="self">The array to stuff.</param>
        /// <param name="lowerBound">Lower bound of the numbers (inclusive). </param>
        /// <param name="upperBound">Upper bound of the numbers (exclusive).</param>
        /// <param name="unique">Wheter the values should be unique. Defaults to false.</param>
        public static void StuffWithRandomNumbers(this int[] self, int lowerBound, int upperBound, bool unique = false)
        {
            Random prng = new Random(DateTime.UtcNow.Ticks.GetHashCode());
            for (int i = 0; i < self.Length; i++)
            {
                if(!unique)
                {
                    self[i] = prng.Next(lowerBound, upperBound);
                }
                else
                {
                    int num;
                    do
                    {
                       num = prng.Next(lowerBound, upperBound);
                    } while (Array.IndexOf(self, num) != -1);
                    self[i] = num;
                }
            }
        }

        /// <summary>
        /// Joins an array to a one-line string.
        /// </summary>
        /// <typeparam name="T">Type of the elements in the array.</typeparam>
        /// <param name="self">The array to join.</param>
        /// <param name="seperator">The seperator between values.</param>
        /// <returns>Single-line <see cref="string"/> representation of the array.</returns>
        public static string ToLineString<T>(this IEnumerable<T> self, string seperator)
        {
            string[] strings = self.Select(i => i.ToString()).ToArray();

            return string.Join(seperator, strings);
        }

        /// <summary>
        /// Returns to the previous iteration of a loop.
        /// </summary>
        /// <param name="i">Loop counter variable.</param>
        /// <returns>Previous value of the number.</returns>
        public static int ReturnToPreviousIteration(ref int i) => i--;
    }
}
