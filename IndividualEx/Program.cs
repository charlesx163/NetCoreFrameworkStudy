using System;
using System.Linq;

namespace IndividualEx
{
    class Program
    {
        public static void Main()
        {
            string[] fruits = { "apple", "mango", "passionfruit", "grape" };
            int i = 0;
            string longestName = fruits.Aggregate("banana",
                (longest, next) =>
                {
                    Console.WriteLine($"longest{i}:{ longest}");
                    Console.WriteLine($"next{i}:{ next}");
                    i++;
                    return next.Length > longest.Length ? next : longest;
                },
                fruit => fruit.ToUpper());
            Console.WriteLine($"The fruit with the longest name is {longestName}.");
        }
    }
}
