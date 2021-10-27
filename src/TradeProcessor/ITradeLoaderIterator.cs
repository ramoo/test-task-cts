using CTSTestApplication;
using System;

namespace TradeProcessor
{
    public interface ITradeLoaderIterator : IDisposable
    {
        bool Next();

        Trade Current { get; }
    }
}
