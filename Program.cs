using PelnoNuostolioSkaiciavimas;
using PelnoNuostolioSkaiciavimas.Services;
using System.Runtime.CompilerServices;

//Console.InputEncoding = System.Text.Encoding.UTF8;
//Console.OutputEncoding = System.Text.Encoding.UTF8;

ReadDataService readDataService = new ReadDataService();
ProfitCalculationService profitCalculation = new ProfitCalculationService();

Console.WriteLine("Enter client:");
string? client = Console.ReadLine();

Console.WriteLine("Enter date: (e.g. 2024-01-02)");
string date = Console.ReadLine();

if (!DateTime.TryParse(date, out DateTime validatedDate))
{
    Console.WriteLine($"Wrong date format was entered. Your input: {date}");
    return;
}

Console.WriteLine("Enter data file path:");
string? dataFilePath = Console.ReadLine();

string firstLine = File.ReadLines("data.csv").First();
IEnumerable<string> data = File.ReadAllLines("data.csv").Skip(1);

Dictionary<string, int> columnNumbers = readDataService.getColumnNumbers(firstLine);






List<Trades> fullTrades = readDataService.getTradesList(columnNumbers, data);

var result = profitCalculation.ProfitLossCalculation(fullTrades, client, validatedDate);

Console.WriteLine("End of program");
