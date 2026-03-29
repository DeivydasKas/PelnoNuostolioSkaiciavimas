using System;
using System.Collections.Generic;
using System.Text;

namespace PelnoNuostolioSkaiciavimas.Services
{
    public static class ResultPrinter
    {
        public static void PrintResults(Dictionary<string, List<decimal>> results, string client, string date, string filePath)
        {
            //var total = results.Sum();
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in results)
            {
                sb.AppendLine($"Client, Security: {kvp.Key}");
                var totalSum = kvp.Value.Sum();
                foreach(var item in kvp.Value)
                {
                    sb.AppendLine($"{item}");
                }
                sb.AppendLine($"Total PnL: {totalSum}");
                sb.AppendLine("-----------------------------");
            }
            File.AppendAllText("C:\\Users\\Deivydas\\result.txt", sb.ToString());

        }
    }
}
