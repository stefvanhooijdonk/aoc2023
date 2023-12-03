using System;
using System.Collections;

namespace aoc2023
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2023 - day 3.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n",lines.LongLength);

            List<Element> elements = new List<Element>();
            var rowIndex = 0;
            foreach(var line in lines){
               
                var newElement = string.Empty;
                var columnIndex = 0;
                var lastColumnIndex = line.Length-1;
                foreach(var c in line.ToCharArray()){                    

                    if(Char.IsDigit(c)){
                        newElement = string.Concat(newElement,c);
                    }
                    if(Element.IsSymbol(c.ToString()) || c == '.' || columnIndex >= lastColumnIndex){
                        if(newElement.Length>0) {
                            var calcStartColumn = columnIndex - (newElement.Length);
                            // last column is detected in the current iteration
                            if (columnIndex >= lastColumnIndex) calcStartColumn++;

                            elements.Add(Element.FromInput(newElement, 
                                rowIndex, 
                                calcStartColumn));
                            newElement = string.Empty;
                        }
                    }
                    if(Element.IsSymbol(c.ToString())){
                        elements.Add(Element.FromInput(c.ToString(),rowIndex,columnIndex));
                    }
                    columnIndex++;       
                }
                rowIndex++;
            }

            var partNumbers = new List<Element>();
            var parts = elements.Where(e => !e.IsElemntSymbol); 
            var symbols = elements.Where(e => e.IsElemntSymbol); 

            foreach(var e in elements){
                Console.WriteLine("p:{0} r:{1} c:{2} l:{3}", e.Part, e.Row, e.Column, e.Part.Length);
            }

            foreach(var element in parts){
                var symbolsAdjecent = symbols.Count(
                    s => (s.Row >= element.Row - 1 && s.Row <= element.Row + 1) &&
                        (s.Column >= element.Column - 1 && s.Column <= (element.Column + element.Part.Length -1 ) + 1  ) 

                );
                Console.WriteLine("For p:{0} found {1} symbols adjecent.",element.Part, symbolsAdjecent);
                if(symbolsAdjecent >= 1){
                    partNumbers.Add(element);
                }
            }

            Console.WriteLine("Count of Elemnts: {0}",elements.Count());
            Console.WriteLine("Count of partNumbers: {0}",partNumbers.Count());
            Console.WriteLine("Count of Symbols: {0}",symbols.Count());

            Console.WriteLine("Sum of Partnumbers: {0}",partNumbers.Select(e => e.PartNumber).Distinct().Sum());
        }
    }

    internal class Element{

        internal string Part = string.Empty;

        internal int Row = 0;
        internal int Column = 0;

        internal int PartNumber = -1;

        internal bool IsElemntSymbol = false;
        internal bool IsPartNumber = false;

        internal static Element FromInput(string input, int row, int column){
            var issymbol = IsSymbol(input);
            return new Element(){
                Row = row,
                Column = column,    
                Part = input,
                IsElemntSymbol = issymbol,
                IsPartNumber = !issymbol,
                PartNumber = !issymbol ? int.Parse(input) : -1
            };
        }

        internal static bool IsSymbol(string input){
            return input !=null && input.Length == 1 && !Char.IsDigit(input,0) && input != ".";
        }
    }
}