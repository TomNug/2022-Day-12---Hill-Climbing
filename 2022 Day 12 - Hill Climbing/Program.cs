

using System.Text;

class Program
{
    private static char[,] CreateMap(string[] instructions)
    {
        char[,] map = new char[5,8];
        
        for (int row=0; row<instructions.Length; row++)
        {
            for (int col=0; col < instructions[0].Length; col++)
            {
                map[row, col] = instructions[row][col];
            }
        }
        return map;
    }

    private static void PrintMap(char[,] map)
    {
        
        for (int row = 0; row < map.GetLength(0); row++)
        {
            StringBuilder sb = new StringBuilder();
            for (int col = 0; col < map.GetLength(1); col++)
            {
                sb.Append(map[row, col]);
            }
            Console.WriteLine(sb.ToString());
        }
    }
    public static void Main(string[] args)
    {
        
        string pathTest = @"C:\Users\Tom\Documents\Advent\2022 Day 12 - Hill Climbing\2022 Day 12 - Hill Climbing\data_test.txt";
        //string pathFull = @"C:\Users\Tom\Documents\ASPNET Projects\2022- Day 11 - Monkeys\2022- Day 11 - Monkeys\data_full.txt";

        string[] instructions = System.IO.File.ReadAllLines(pathTest);

        char[,] map = CreateMap(instructions);
        PrintMap(map);
    }
}