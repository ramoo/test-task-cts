namespace TradeProcessor
{
    public interface ITradeLoaderFactory
    {
        public ITradeLoaderIterator CreateIterator();
    }
}
