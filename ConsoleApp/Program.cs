using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var test = new HashSet<int>();
            test.Add(1);
            test.Add(1);
            test.Add(2);
            test.Add(2);
            test.Add(2);

            Console.WriteLine($"Hello World! {test.Count}");
        }
    }
}
