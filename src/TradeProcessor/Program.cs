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
            var path = Console.ReadLine().GuardNotEmpty("path");

            Console.WriteLine("Connection string:");
            var connectionString = Console.ReadLine().GuardNotEmpty("connectionString");

            Console.WriteLine("Transaction identifier:");
            var transactionId = Console.ReadLine().GuardNotEmpty("transactionId");

            Console.WriteLine("Number of best sold/bought assets to check");
            var numberOfAssetsToCheck = int.Parse(Console.ReadLine()).GuardBiggerThan(-1, "numberOfAssetsToCheck");

            Console.WriteLine("Isin to report:");
            var isin = Console.ReadLine();

            Run(path, connectionString, transactionId, numberOfAssetsToCheck, isin);
        }

        private static void Run(string path, string connectionString, string transactionId, int numberOfAssetsToCheck, string isin)
        {
            Console.WriteLine($"Processing records");

            var tradeReporter = new TradeReporter(numberOfAssetsToCheck);
            var client = new Client(new TradeLoaderFactory(path), new DataAdapter(connectionString), tradeReporter);
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {
                client.Process(transactionId);

                stopWatch.Stop();

                Console.WriteLine($"Sum of quantity of best buys for isin: {isin}: {tradeReporter.GetBestBuysQuantity(isin)}");
                Console.WriteLine($"Sum of quantity of best sells for isin: {isin}:{tradeReporter.GetBestSellsQuantity(isin)}");
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
