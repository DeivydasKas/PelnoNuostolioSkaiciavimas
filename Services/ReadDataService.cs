using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace PelnoNuostolioSkaiciavimas.Services
{
    public class ReadDataService
    {
        public Dictionary<string, int> GetColumnIndexes(string firstLine)
        {
            Dictionary<string, int> columnIndexes = new Dictionary<string, int>();
            var names = firstLine.Split(';');
            for (int i = 0; i < names.Length; i++)
            {
                columnIndexes.Add(names[i], i);
            }
            return columnIndexes;
        }
        public List<Trades> GetTradesList(Dictionary<string, int> columnIndexes, IEnumerable<string> data)
        {
            List<Trades> trades = new List<Trades>();

            foreach (var item in data)
            {
                var splitLine = item.Split(';');
                Trades trade = new Trades();
                foreach (KeyValuePair<string, int> entry in columnIndexes)
                {

                    switch (entry.Key)
                    {
                        case "TradeId":
                            trade.TradeId = Int32.Parse(splitLine[entry.Value]);
                            break;
                        case "Amount":
                            trade.Amount = Int32.Parse(splitLine[entry.Value]);
                            break;
                        case "Type":
                            trade.Type = (splitLine[entry.Value]).ToString();
                            break;
                        case "Client":
                            trade.Client = (splitLine[entry.Value]);
                            break;
                        case "Security":
                            trade.Security = (splitLine[entry.Value]);
                            break;
                        case "Date":
                            trade.Date = DateTime.Parse(splitLine[entry.Value]);
                            break;
                        case "Price":
                            trade.Price = decimal.Parse(splitLine[entry.Value], CultureInfo.GetCultureInfo("lt-LT"));
                            break;
                        case "Fee":
                            trade.Fee = decimal.Parse(splitLine[entry.Value], CultureInfo.GetCultureInfo("lt-LT"));
                            break;
                    }
                }
                trades.Add(trade);
            }
            return trades;

        }
    }
}
