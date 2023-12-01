using System;
using System.Collections;

namespace aoc2023
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("AdventOfCode 2023 - day 2.");

            var inputFolder = Environment.CurrentDirectory;

            Console.WriteLine("Reading input.txt from: {0}\r\n",inputFolder);
            var lines = System.IO.File.ReadAllLines(System.IO.Path.Combine(inputFolder,"input.txt"));
            Console.WriteLine("Read input.txt with: {0} lines.\r\n",lines.LongLength);

            var list = new List<CalibrationLine>();
            foreach(var l in lines){
                list.Add(new CalibrationLine(l));
            }

            var result = list.Sum(l => l.GetCalibrationValue());
            Console.WriteLine("Total of all Calibration values ( {0} ) \r\n", result);

            var realresult = list.Sum(l => l.GetRealCalibrationValue());
            Console.WriteLine("Total of all Real Calibration values ( {0} )\r\n", realresult);
        }

        internal static List<string> numbers = ["zero","one","two","three","four","five","six","seven","eight","nine"];
    }

    public class CalibrationLine{
        private string _line;
        public CalibrationLine(string line){
            _line = line;
        }

        public int GetCalibrationValue(){
            var firstDigit = _line.ToCharArray().First(c => char.IsDigit(c));
            var lastDigit = _line.ToCharArray().Last(c => char.IsDigit(c));

            return int.Parse(string.Format("{0}{1}", firstDigit,lastDigit));        
        }

        public int GetRealCalibrationValue(){
            var firstDigit = _line.ToCharArray().First(c => char.IsDigit(c)).ToString();
            var firstDigitIndex = _line.IndexOf(firstDigit);

            for(var n=0; n<10; n++) {
                var number = Program.numbers[n];
                var numberFirstIndex = _line.IndexOf(number);
                if(numberFirstIndex>=0 && numberFirstIndex<firstDigitIndex){
                    firstDigitIndex = numberFirstIndex;
                    firstDigit = n.ToString();
                }
            }

            var lastDigit = _line.ToCharArray().Last(c => char.IsDigit(c)).ToString();
            var lastDigitIndex = _line.LastIndexOf(lastDigit);

            for(var n=0; n<10; n++) {
                var number = Program.numbers[n];
                var lastNumberIndex = _line.LastIndexOf(number);
                if(lastNumberIndex>=0 && lastNumberIndex>lastDigitIndex){
                    lastDigitIndex = lastNumberIndex;
                    lastDigit = n.ToString();
                }
            }

            return int.Parse(string.Format("{0}{1}", firstDigit,lastDigit));        
        }
    }
}