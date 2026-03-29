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
        public List<Trades> GetTradesList(Dictionary<string, int> columnNumbers, IEnumerable<string> data)
        {
            List<Trades> trades = new List<Trades>();

            foreach (var item in data)
            {
                var spllitedLine = item.Split(';');
                Trades trade = new Trades();
                foreach (KeyValuePair<string, int> entry in columnNumbers)
                {

                    switch (entry.Key)
                    {
                        case "TradeId":
                            trade.TradeId = Int32.Parse(spllitedLine[entry.Value]);
                            break;
                        case "Amount":
                            trade.Amount = Int32.Parse(spllitedLine[entry.Value]);
                            break;
                        case "Type":
                            trade.Type = (spllitedLine[entry.Value]).ToString();
                            break;
                        case "Client":
                            trade.Client = (spllitedLine[entry.Value]);
                            break;
                        case "Security":
                            trade.Security = (spllitedLine[entry.Value]);
                            break;
                        case "Date":
                            trade.Date = DateTime.Parse(spllitedLine[entry.Value]);
                            break;
                        case "Price":
                            trade.Price = decimal.Parse(spllitedLine[entry.Value], CultureInfo.GetCultureInfo("lt-LT"));
                            break;
                        case "Fee":
                            trade.Fee = decimal.Parse(spllitedLine[entry.Value], CultureInfo.GetCultureInfo("lt-LT"));
                            break;
                    }
                }
                trades.Add(trade);
            }
            return trades;

        }
    }
}
