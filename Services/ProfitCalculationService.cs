using System;
using System.Collections.Generic;
using System.Text;

namespace PelnoNuostolioSkaiciavimas.Services
{
    public class ProfitCalculationService
    {
        public string ProfitLossCalculation(List<Trades> trades, string client, DateTime date)
        {
            List<Trades> clientTrades = trades.Where(x => x.Client == client && x.Date <= date).ToList();
            #region not used
            ////Get a list of all possible Securities
            //List<string> securityList = new List<string>();

            //foreach (var item in clientTrades)
            //{
            //    if(!securityList.Contains(item.Security))
            //    {
            //        securityList.Add(item.Security);
            //    }
            //}

            //List<List<string>> allLists = new List<List<string>>();

            //Separate client transactions by Securities

            //for (int i = 0; i < securityList.Count(); i++)
            //{
            //    allLists.Add()
            //}
            #endregion
            var ListTsla = clientTrades.Where(x => x.Security == "TSLA").OrderBy(t => t.Date).ToList();
            var ListAppl = clientTrades.Where(x => x.Security == "APPL").OrderBy(t => t.Date).ToList();

            Calculate(ListTsla);

            return string.Empty;
        }
        public string Calculate(List<Trades> fullTradesList)
        {
            List<Trades> buyTranstactions = fullTradesList.Where(x => x.Type.Equals("BUY", StringComparison.OrdinalIgnoreCase)).ToList();
            List<Trades> sellTranstactions = fullTradesList.Where(x => x.Type.Equals("BUY", StringComparison.OrdinalIgnoreCase)).ToList();

            //Pachekinti jei sell transakcija pirma.
            foreach (var item in sellTranstactions)
            {
                var buyTrans = buyTranstactions.First();
                buyTranstactions.Remove(buyTrans);

                if (buyTrans.Amount < item.Amount)
                {
                    var result = ((buyTrans.Amount * item.Price) - (item.Fee / item.Amount * buyTrans.Amount)) - ((buyTrans.Amount * buyTrans.Price) + buyTrans.Fee);
                    var amountLeft = item.Amount - buyTrans.Amount;
                    if (amountLeft > 0)
                    {
                        item.Amount = item.Amount - buyTrans.Amount;
                    }
                    else
                    {
                        item.Amount = 0;
                    }
                }
                else
                {
                    var result = ((item.Amount * item.Price) - item.Fee) - ((buyTrans.Amount * buyTrans.Price) + buyTrans.Fee);
                }

            }

            return string.Empty;
        }
    }
}
