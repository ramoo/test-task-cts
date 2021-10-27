namespace TradeProcessor
{
    public class TradeLoaderFactory : ITradeLoaderFactory
    {
        private readonly string _sourceFilePath;

        public TradeLoaderFactory(string sourceFilePath)
        {
            _sourceFilePath = sourceFilePath.GuardNotEmpty(nameof(sourceFilePath));
        }

        public ITradeLoaderIterator CreateIterator()
        {
            return new TradeLoaderIterator(_sourceFilePath);
        }
    }
}
