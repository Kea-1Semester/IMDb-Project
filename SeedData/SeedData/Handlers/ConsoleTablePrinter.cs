using System;
using System.Collections.Generic;
using System.Linq;

namespace SeedData.Handlers
{
    public class ConsoleTablePrinter
    {
        private readonly List<string> _headers;
        private readonly List<List<string>> _rows;
        private readonly List<int> _columnWidths;

        public ConsoleTablePrinter(IEnumerable<string> headers)
        {
            _headers = headers.ToList();
            _rows = new List<List<string>>();
            _columnWidths = _headers.Select(h => h.Length).ToList();
        }

        public void AddRow(IEnumerable<string> row)
        {
            var rowList = row.ToList();
            _rows.Add(rowList);
            for (int i = 0; i < rowList.Count; i++)
            {
                if (rowList[i].Length > _columnWidths[i])
                    _columnWidths[i] = rowList[i].Length;
            }
        }

        public void Print()
        {
            string border = "+" + string.Join("+", _columnWidths.Select(w => new string('-', w + 2))) + "+";
            // Print header
            Console.WriteLine(border);
            Console.WriteLine("| " + string.Join(" | ", _headers.Select((h, i) => h.PadRight(_columnWidths[i]))) + " |");
            Console.WriteLine(border);
            // Print rows
            foreach (var row in _rows)
            {
                Console.WriteLine("| " + string.Join(" | ", row.Select((cell, i) => cell.PadRight(_columnWidths[i]))) + " |");
            }
            Console.WriteLine(border);
        }
    }
}