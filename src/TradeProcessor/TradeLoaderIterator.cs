using CTSTestApplication;
using System;
using System.Xml;

namespace TradeProcessor
{
    public class TradeLoaderIterator : ITradeLoaderIterator
    {
        private readonly XmlReader _reader;
        private bool _started = false;
        private Trade _current;

        public TradeLoaderIterator(string sourceFilePath)
        {
            sourceFilePath.GuardNotEmpty(nameof(sourceFilePath));

            _reader = XmlReader.Create(sourceFilePath);
        }

        public Trade Current
        {
            get
            {
                if (!_started)
                {
                    throw new InvalidOperationException("Iteration did not start, move pointer to next item using Next() method.");
                }

                return _current;
            }
        }

        public void Dispose()
        {
            _reader.Dispose();
        }

        public bool Next()
        {
            if (!_started)
            {
                _started = true;
                if (!_reader.ReadToDescendant("Trade"))
                {
                    return false;
                }

                LoadCurrent();
            }
            else
            {
                if (!_reader.ReadToNextSibling("Trade"))
                {
                    return false;
                }

                LoadCurrent();
            }

            return true;
        }

        private void LoadCurrent()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(_reader.ReadOuterXml());
            XmlNode item = doc.DocumentElement;

            _current = new Trade(
                item["Id"] == null ? 0 : int.Parse(item["Id"].InnerText),
                item["ISIN"].InnerText,
                decimal.Parse(item["Quantity"].InnerText),
                decimal.Parse(item["Price"].InnerText),
                item["Direction"].InnerText.GetDirection());
        }
    }
}
