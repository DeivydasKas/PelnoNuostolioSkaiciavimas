using System;
using System.Collections.Generic;
using System.Text;

namespace PelnoNuostolioSkaiciavimas.Services
{
    public class TradePnLService
    {
        public List<List<Trades>> GetFilteredTrades(List<Trades> trades, string client, DateTime date)
        {
            List<List<Trades>> filteredLists = new List<List<Trades>>();

            List<Trades> clientTrades = trades.Where(x => x.Client.Equals(client, StringComparison.OrdinalIgnoreCase) && x.Date <= date).ToList();

            var securityList = clientTrades.Select(x => x.Security).Distinct().ToList();
            foreach (var security in securityList)
            {
                var filteredSecurityTrades = clientTrades.Where(x => x.Security == security)
                    .OrderBy(t => t.Date)
                    .ToList();

                filteredLists.Add(filteredSecurityTrades);
            }

            return filteredLists;
        }
        public Dictionary<string, List<decimal>> CalculatePnL(List<Trades> fullTradesList)
        {
            List<Trades> buyTransactions = fullTradesList.Where(x => x.Type.Equals("BUY", StringComparison.OrdinalIgnoreCase)).ToList();
            List<Trades> sellTransactions = fullTradesList.Where(x => x.Type.Equals("SELL", StringComparison.OrdinalIgnoreCase)).ToList();

            List<decimal> results = new List<Decimal>();
            Dictionary<string, List<decimal>> kvpResults = new Dictionary<string, List<decimal>>();

            foreach (var sellTrans in sellTransactions)
            {
                foreach (var buyTrans in buyTransactions)
                {
                    decimal result = 0;

                    if (buyTrans.Amount == 0) continue;

                    if (buyTrans.Amount <= sellTrans.Amount)
                    {
                        result = ((buyTrans.Amount * sellTrans.Price) - (sellTrans.FeePerUnit * buyTrans.Amount)) - ((buyTrans.Amount * buyTrans.Price) + (buyTrans.FeePerUnit * buyTrans.Amount));

                        sellTrans.Amount = sellTrans.Amount - buyTrans.Amount;
                        buyTrans.Amount = 0;
                        results.Add(result);
                        continue;
                    }
                    else
                    {
                        result = ((sellTrans.Amount * sellTrans.Price) - (sellTrans.FeePerUnit * sellTrans.Amount)) - ((sellTrans.Amount * buyTrans.Price) + (buyTrans.FeePerUnit * sellTrans.Amount));

                        buyTrans.Amount = buyTrans.Amount - sellTrans.Amount;
                        sellTrans.Amount = 0;
                        results.Add(result);
                        break;
                    }
                }
            }
            kvpResults.Add($"{buyTransactions[0].Client}, {buyTransactions[0].Security}", results);

            return kvpResults;
        }

        public List<Trades> CalculateFeePerUnit(List<Trades> fullTradesList)
        {
            foreach (var trade in fullTradesList)
            {
                var feePerUnitResult = trade.Fee / trade.Amount;
                trade.FeePerUnit = feePerUnitResult;
            }
            return fullTradesList;
        }
    }
}
