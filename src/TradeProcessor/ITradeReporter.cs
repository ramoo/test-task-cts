using CTSTestApplication;

namespace TradeProcessor
{
    public interface ITradeReporter
    {
        public void AddTrade(Trade trade);

        public decimal GetBestBuysQuantity(string isin);

        public decimal GetBestSellsQuantity(string isin);
    }
}
