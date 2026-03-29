Profit / Loss Calculation (FIFO)

This is a console application that calculates realized profit or loss (PnL) using the FIFO (First-In, First-Out) method for a given client and date, based on data from a CSV file.

The task is based on the requirement to:

read trading data from a file;
handle dynamic column order;
filter by client and date;
calculate PnL using FIFO;
output results to a file.
Technologies
C#
.NET Console Application
How It Works

The application:

asks the user to input:
client name
date
path to data.csv
reads and parses the file;
filters trades by client and date;
groups trades by Security;
calculates PnL using FIFO logic;
writes results to out.txt.
Input File Format
First line: column headers (order can vary)
Data is separated by ;
From the second line: trade records

Example:

TradeId;Type;Date;Client;Security;Amount;Price;Fee
1;BUY;2024-01-02;Jonas;TSLA;10;35,25;17,62
2;SELL;2024-01-06;Jonas;TSLA;5;40;10
Running the Application

After starting the program, provide:

client name (e.g. Jonas)
date (e.g. 2024-01-06)
full path to data.csv
Output

Results are written to out.txt and include:

Client and Security
Individual FIFO calculation results
Total PnL

Example output:

Client, Security: Jonas, TSLA
9.8800
-25.8200
Total PnL: -15.9400
-----------------------------
Notes
Buy fee increases cost basis
Sell fee reduces profit
FIFO method is used for matching trades
