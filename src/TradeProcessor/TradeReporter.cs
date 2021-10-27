using CTSTestApplication;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TradeProcessor
{
    public class TradeReporter : ITradeReporter
    {
        private readonly int _itemCount;
        private readonly Dictionary<string, List<Trade>> _buys;
        private readonly Dictionary<string, List<Trade>> _sells;

        public TradeReporter(int itemCount)
        {
            _itemCount = itemCount.GuardBiggerThan(0, nameof(itemCount));
            _buys = new Dictionary<string, List<Trade>>();
            _sells = new Dictionary<string, List<Trade>>();
        }

        public void AddTrade(Trade trade)
        {
            trade.GuardNotNull(nameof(trade));

            switch(trade.Direction)
            {
                case Direction.Buy:
                    AddBuy(trade);
                    break;
                case Direction.Sell:
                    AddSell(trade);
                    break;
                default:
                    throw new InvalidOperationException("Unexpected trade direction.");
            }
        }

        private void AddSell(Trade trade)
        {
            if(!_sells.ContainsKey(trade.Isin))
            {
                _sells.Add(trade.Isin, new List<Trade>());
            }

            _sells[trade.Isin].Add(trade);

            if (_sells[trade.Isin].Count > _itemCount)
            {
                _sells[trade.Isin] = _sells[trade.Isin].OrderByDescending(s => s.Price).Take(_itemCount).ToList();
            }
        }

        private void AddBuy(Trade trade)
        {
            if (!_buys.ContainsKey(trade.Isin))
            {
                _buys.Add(trade.Isin, new List<Trade>());
            }

            _buys[trade.Isin].Add(trade);

            if (_buys[trade.Isin].Count > _itemCount)
            {
                _buys[trade.Isin] = _buys[trade.Isin].OrderBy(s => s.Price).Take(_itemCount).ToList();
            }
        }

        public decimal GetBestBuysQuantity(string isin)
        {
            if(_buys.ContainsKey(isin))
            {
                return _buys[isin].Sum(i => i.Quantity);
            }

            return 0;
        }

        public decimal GetBestSellsQuantity(string isin)
        {
            if (_sells.ContainsKey(isin))
            {
                return _sells[isin].Sum(i => i.Quantity);
            }

            return 0;
        }
    }
}
