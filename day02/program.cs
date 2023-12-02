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

            var games = new List<Game>();
            foreach(var g in lines){
                games.Add(Game.FromInput(g));
            }
            
            var validGames = games.Where(
                game => game.IsValidGameGiven(12, 13, 14));
            
            Console.WriteLine("Total of IDs of possible games ( {0} ) \r\n", validGames.Sum(g => g.GameID));
            Console.WriteLine("Total of Powers of games ( {0} ) \r\n", games.Sum(g => g.Power()));
        }

        internal class Game{

            internal int GameID = -1;
            internal List<Draw> Draws = new List<Draw>();
            
            internal static Game FromInput(string input){
                var splits = input.Split(":");
                var game = new Game();
                game.GameID = int.Parse(splits[0].Split(" ")[1]);

                var draws = splits[1].Split(";");
                foreach(var draw in draws){
                    game.Draws.Add(Draw.FromInput(draw));
                }
                return game;
            }

            internal bool IsValidGameGiven( int red, int green, int blue){
                return Draws.All(draw => 
                    draw.IsValidDrawGiven(red, green, blue));
            }

            internal int Power(){
                var minRed = Draws.Max( d => d.Red );
                var minGreen = Draws.Max( d => d.Green );
                var minBlue = Draws.Max( d => d.Blue );

                return minRed * minGreen * minBlue;
            }
        }

        internal class Draw{
            internal int Red = -1;
            internal int Green = -1;
            internal int Blue = -1;

            internal static Draw FromInput(string input){
                var draws = input.Trim().Split(",");
                var draw = new Draw();
                foreach(var d in draws){
                    var splitdraw = d.Trim().ToLowerInvariant().Split(" ");
                    var amountDrawn = int.Parse(splitdraw[0]);
                    var color = splitdraw[1].Trim();
                    if(color == "red") draw.Red = amountDrawn;
                    if(color == "green") draw.Green = amountDrawn;
                    if(color == "blue") draw.Blue = amountDrawn;
                }
                return draw;
            }

            internal bool IsValidDrawGiven(int red, int green, int blue){
                return 
                    (Red <= red || Red < 0)&& 
                    (Green <= green || Green < 0)&& 
                    (Blue <= blue || Blue < 0);
            }
        }
    }
}