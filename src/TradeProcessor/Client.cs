using CTSTestApplication;
using System;

namespace TradeProcessor
{
    public class Client
    {
        private readonly ITradeLoaderFactory _tradeLoaderFactory;
        private readonly IDataAdapter _dataAdapter;

        public Client(ITradeLoaderFactory tradeLoaderFactory, IDataAdapter dataAdapter)
        {
            _tradeLoaderFactory = tradeLoaderFactory.GuardNotNull(nameof(tradeLoaderFactory));
            _dataAdapter = dataAdapter.GuardNotNull(nameof(dataAdapter));
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

                        //tradeLoader.Current.Create(db);
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
