using PelnoNuostolioSkaiciavimas;
using PelnoNuostolioSkaiciavimas.Services;
using System.Runtime.CompilerServices;
using System.Text;

Console.InputEncoding = Encoding.Unicode;
Console.OutputEncoding = Encoding.UTF8;

ReadDataService readDataService = new ReadDataService();
TradePnLService tradePnLService = new TradePnLService();


#region User Input validation
Console.WriteLine("Enter client:");
string? client = Console.ReadLine();

if(string.IsNullOrWhiteSpace(client))
{
    Console.WriteLine("Client name was not entered");
    return;
}

Console.WriteLine("Enter date: (e.g. 2024-01-02)");
string? date = Console.ReadLine();

if (!DateTime.TryParse(date, out DateTime validatedDate))
{
    Console.WriteLine($"Wrong date format was entered. Your input: {date}");
    return;
}

Console.WriteLine("Enter data file path:");
string? filePath = Console.ReadLine();
if(!File.Exists(filePath))
{
    Console.WriteLine("File not found");
    return;
}

var lines = File.ReadAllLines(filePath);
if(lines.Length <= 1)
{
    Console.WriteLine("File is empty or has no data");
    return;
}

#endregion

#region Read data
string firstLine = File.ReadLines(filePath).First();
IEnumerable<string> data = File.ReadAllLines(filePath).Skip(1);

Dictionary<string, int> columnIndexes = readDataService.GetColumnIndexes(firstLine);
List<Trades> tradesList = readDataService.GetTradesList(columnIndexes, data);
#endregion

#region Filter
List<List<Trades>> filteredLists = tradePnLService.GetFilteredTrades(tradesList, client, validatedDate);
#endregion

#region Return Results
foreach (var item in filteredLists)
{
    var tradesWithFeePerUnit = tradePnLService.CalculateFeePerUnit(item);
    Dictionary<string, List<decimal>> results = tradePnLService.CalculatePnL(tradesWithFeePerUnit);
    ResultPrinter.PrintResults(results);
}
#endregion

Console.WriteLine($"End of program");
