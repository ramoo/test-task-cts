using CTSTestApplication;
using System;

namespace DataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generate test data");         
            Console.WriteLine("Target file folder:");
            var path = Console.ReadLine();

            Console.WriteLine("Number of records to generate:");
            var count = int.Parse(Console.ReadLine());

            var tester = new Tester();
            tester.CreateTestFile(path, count);

            Console.WriteLine($"File created in {path} with {count} records.");
        }
    }
}
