using System;
using System.Collections.Generic;
using System.Linq;

namespace RailwayReservation.Views
{
    public static class TablePrinter
    {
        public static void PrintTable(List<string[]> rows, string[] headers)
        {
            if (rows == null) rows = new List<string[]>();
            int cols = headers.Length;
            int[] widths = new int[cols];
            for (int c = 0; c < cols; c++) widths[c] = headers[c].Length;

            foreach (var row in rows)
            {
                for (int c = 0; c < cols; c++)
                {
                    widths[c] = Math.Max(widths[c], (row[c] ?? "").Length);
                }
            }

            string sep = "+" + string.Join("+", widths.Select(w => new string('-', w + 2))) + "+";
            Console.WriteLine(sep);
            Console.WriteLine("| " + string.Join(" | ", headers.Select((h, i) => h.PadRight(widths[i]))) + " |");
            Console.WriteLine(sep);
            foreach (var row in rows)
            {
                Console.WriteLine("| " + string.Join(" | ", row.Select((c, i) => (c ?? "").PadRight(widths[i]))) + " |");
            }
            Console.WriteLine(sep);
        }
    }
}
