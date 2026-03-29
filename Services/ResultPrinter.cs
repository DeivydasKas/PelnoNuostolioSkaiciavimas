using System;
using System.Collections.Generic;
using System.Text;

namespace PelnoNuostolioSkaiciavimas.Services
{
    public static class ResultPrinter
    {
        public static void PrintResults(Dictionary<string, List<decimal>> results)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var kvp in results)
            {
                sb.AppendLine($"Client, Security: {kvp.Key}");
                var totalSum = kvp.Value.Sum();
                foreach(var item in kvp.Value)
                {
                    sb.AppendLine($"{item.ToString("F4")}");
                }
                sb.AppendLine($"Total PnL: {totalSum.ToString("F4")}");
                sb.AppendLine("-----------------------------");
            }
            File.AppendAllText("out.txt", sb.ToString());

        }
    }
}
