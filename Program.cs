using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("p <row> <column>");
            Console.WriteLine("   pops the mine at index <row> <column>");
            Console.WriteLine("n");
            Console.WriteLine("   creates a new game (10x10, 15 mines)");
            MineField game = new MineField(10, 10, 15);
            String inp = "";
            while (inp != "x")
            {
                inp = Console.ReadLine();
                if (inp[0] == 'p')
                {
                    int x = int.Parse(inp.Split(' ')[1]);
                    int y = int.Parse(inp.Split(' ')[2]);
                    game.PopMine(x, y);
                }
                else if (inp[0] == 'n')
                {
                    game = new MineField(10, 10, 15);
                }
                else if (inp[0] == 's')
                {
                    game.DebugPrint();
                }
                else if (inp == "help")
                {
                    Console.WriteLine("p <row> <column>");
                    Console.WriteLine("   pops the mine at index <row> <column>");
                    Console.WriteLine("n");
                    Console.WriteLine("   creates a new game (10x10, 15 mines)");
                    Console.WriteLine("s");
                    Console.WriteLine("   prints the mine field");
                }
            }
        }
    }
}
