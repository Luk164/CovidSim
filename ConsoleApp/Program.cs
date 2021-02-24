using System;
using System.Collections.Generic;
using CovidSim;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var sim = new Simulator(100, 20, 10, 10, 10);

            while (true)
            {
                sim.Day();
                Console.ReadLine();
            }
        }
    }
}
