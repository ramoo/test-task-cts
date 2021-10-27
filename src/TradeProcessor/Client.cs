using CTSTestApplication;
using System;

namespace TradeProcessor
{
    public class Client
    {
        private readonly ITradeLoaderFactory _tradeLoaderFactory;
        private readonly IDataAdapter _dataAdapter;
        private readonly ITradeReporter _tradeReporter;

        public Client(ITradeLoaderFactory tradeLoaderFactory, IDataAdapter dataAdapter, ITradeReporter tradeReporter)
        {
            _tradeLoaderFactory = tradeLoaderFactory.GuardNotNull(nameof(tradeLoaderFactory));
            _dataAdapter = dataAdapter.GuardNotNull(nameof(dataAdapter));
            _tradeReporter = tradeReporter.GuardNotNull(nameof(tradeReporter));
        }

        public void Process(string transactionId)
        {
            transactionId.GuardNotEmpty(nameof(transactionId));

            try
            {
                _dataAdapter.BeginTransaction(transactionId);

                using (var iterator = _tradeLoaderFactory.CreateIterator())
                {
                    while (iterator.Next())
                    {
                        Console.WriteLine($"id: {iterator.Current.Id}, isin: {iterator.Current.Isin}, direction: {iterator.Current.Direction}, price: {iterator.Current.Price}, quantity: {iterator.Current.Quantity}");

                        // iterator.Current.Create(_dataAdapter);

                        _tradeReporter.AddTrade(iterator.Current);
                    }
                }

                _dataAdapter.CommitTransaction(transactionId);
            }
            catch (Exception e)
            {
                _dataAdapter.RollbackTransaction(transactionId);

                throw new Exception("Processing failed, transaction rollbacked.", e);
            }
        }
    }
}
