using PelnoNuostolioSkaiciavimas;
using System.Runtime.CompilerServices;

ReadDataService readDataService = new ReadDataService();

var firstLine = File.ReadLines("data.csv").First();
var columnNumbers = readDataService.getColumnNumbers(firstLine);

var data = File.ReadAllLines("data.csv").Skip(1);

Console.WriteLine("Enter client:");
Console.ReadLine();
Console.WriteLine("Enter date:");
Console.ReadLine();
Console.WriteLine("Enter data file path:");
Console.ReadLine();


List<Trades> fullTrades = readDataService.getTradesList(columnNumbers, data);

Console.WriteLine("End of program");
