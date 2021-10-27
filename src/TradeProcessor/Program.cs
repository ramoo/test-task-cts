using CTSTestApplication;
using System;
using System.Diagnostics;

namespace TradeProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Path to source data file:");
            var path = Console.ReadLine();

            Console.WriteLine("Connection string:");
            var connectionString = Console.ReadLine();

            Console.WriteLine("Transaction identifier:");
            var transactionId = Console.ReadLine();

            Run(path, connectionString, transactionId);
        }

        private static void Run(string path, string connectionString, string transactionId)
        {
            Console.WriteLine($"Processing records");

            var client = new Client(new TradeLoaderFactory(path), new DataAdapter(connectionString));
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                client.Process(transactionId);

                stopWatch.Stop();

                Console.WriteLine($"Processing done. Elapsed time {stopWatch.Elapsed.GetElapsedTimeString()}.");
            }
            catch (Exception e)
            {
                stopWatch.Stop();

                throw new Exception($"Processing failed. Elapsed time {stopWatch.Elapsed.GetElapsedTimeString()}.", e);
            }
        }
    }
}
