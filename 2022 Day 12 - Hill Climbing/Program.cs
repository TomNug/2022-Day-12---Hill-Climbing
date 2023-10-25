

using System.Text;

class Dijkstra
{
    // Populates an array with the integer max value (representing infinity)
    public static int[,] MaxValueArray(int[,] arr)
    {
        for (int i = 0; i < arr.GetLength(0); i++)
        {
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                arr[i, j] = Int32.MaxValue;
            }
        }
        return arr;
    }

    // Choose the coordinate with the shortest distance from available nodes
    public static (int,int) ChooseCoordWithShortestDistance(int[,] distances, bool[,] visited)
    {
        int shortestDistance = Int32.MaxValue;
        (int, int) coordOfShortestDistance = (-1, -1);
        for (int row=0; row< distances.GetLength(0); row++)
        {
            for (int col=0; col<distances.GetLength(1); col++)
            {
                if (distances[row,col] < shortestDistance && !visited[row,col])
                {
                    shortestDistance = distances[row,col];
                    coordOfShortestDistance = (row, col);
                }
            }
        }
        return coordOfShortestDistance;
    }

    // Compares two coordinates on the grid, check if their characters are within one of each other
    public static bool Reachable(char char1, char char2)
    {
        bool reachable = false;
        if (Math.Abs(char1 - char2) <= 1)
        {
            reachable = true;
        }
        return reachable;
    }

    // Returns the list of nodes which can be reached from the current node
    public static List<(int, int)> CalculateVisitableNodes((int,int) currentCoord, bool[,] visited, char[,] map)
    {
        List<(int, int)> visitable = new List<(int, int)> ();

        // North (and current is not on the N edge)
        if (currentCoord.Item1 > 0)
        {
            if (Reachable(map[currentCoord.Item1, currentCoord.Item2], map[currentCoord.Item1-1, currentCoord.Item2]))
            {
                // add to list
                visitable.Add((currentCoord.Item1 - 1, currentCoord.Item2));
            }
        }
        // East (and current is not on the E edge)
        if (currentCoord.Item2 < map.GetLength(1)-1)
        {
            if (Reachable(map[currentCoord.Item1, currentCoord.Item2], map[currentCoord.Item1, currentCoord.Item2 + 1]))
            {
                // add to list
                visitable.Add((currentCoord.Item1, currentCoord.Item2 + 1));
            }
        }
        // South (and current is not on the S edge)
        if (currentCoord.Item1 < map.GetLength(0) - 1)
        {
            if (Reachable(map[currentCoord.Item1, currentCoord.Item2], map[currentCoord.Item1 + 1, currentCoord.Item2]))
            {
                // add to list
                visitable.Add((currentCoord.Item1 + 1, currentCoord.Item2));
            }
        }
        // West (and current is not on the W edge)
        if (currentCoord.Item2 > 0)
        {
            if (Reachable(map[currentCoord.Item1, currentCoord.Item2], map[currentCoord.Item1, currentCoord.Item2 - 1]))
            {
                // add to list
                visitable.Add((currentCoord.Item1, currentCoord.Item2 - 1));
            }
        }
        return visitable;
    }


    public static int FindShortestPath(char[,] map, (int,int) start, (int,int) end)
    {
        // Setup variables
        bool[,] visited = new bool[map.GetLength(0), map.GetLength(1)]; // array of falses, recording whether coords have been visited
        int[,] distance = MaxValueArray(new int[map.GetLength(0), map.GetLength(1)]); // Create array of infities, distance from start to that coord
        distance[start.Item1, start.Item2] = 0;
        (int, int)[,] previous = new (int, int)[map.GetLength(0), map.GetLength(1)]; // to record previous node in quickest route to node

        // Algorithm

        
        // if -1,-1, no visitable nodes
        (int, int) currentCoord = ChooseCoordWithShortestDistance(distance, visited);
        while (currentCoord.Item1 != -1)
        {
            int currentDistance = distance[currentCoord.Item1, currentCoord.Item2];    
            
            // Identify visitable nodes
            List <(int, int)> visitable = CalculateVisitableNodes(currentCoord, visited, map);
            foreach((int,int) nodeToVisit in visitable)
            {
                // Adjust distance
                distance[nodeToVisit.Item1, nodeToVisit.Item2] = currentDistance + 1;
                // Adjust previous
                previous[nodeToVisit.Item1, nodeToVisit.Item2] = currentCoord;
            }
            // Mark visited
            visited[currentCoord.Item1, currentCoord.Item2] = true;

            // choose coordwithshortestdistance again
            currentCoord = ChooseCoordWithShortestDistance(distance, visited);
        }


        return distance[end.Item1,end.Item2];
    }
}
class Program
{
    private static int CreateMapAndSolve(string[] instructions)
    {
        char[,] map = new char[5,8];
        (int, int) start = (0, 0);
        (int, int) end = (0, 0);

        for (int row=0; row<instructions.Length; row++)
        {
            for (int col=0; col < instructions[0].Length; col++)
            {
                map[row, col] = instructions[row][col];

                // Record start, but have it in the map as a, and End as Z
                if (instructions[row][col] == 'S')
                {
                    start = (row, col);
                    map[row, col] = 'a';
                }
                if (instructions[row][col] == 'E')
                {
                    end = (row, col);
                    map[row, col] = 'z';
                }
                
            }
        }
        return Dijkstra.FindShortestPath(map, start, end);
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

        int shortestDistance = CreateMapAndSolve(instructions);
        //PrintMap(map);
        Console.WriteLine(shortestDistance);
    }
}