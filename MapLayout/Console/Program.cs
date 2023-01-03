using System;

namespace MapLayout
{
    public class Game
    {
        #region
        public static void Main()
        {
            //variables
            string Name = "Game"; // name
            int X = 18; // x size
            int Y = 9; // y size

            int ChangedN = 1; // number the player is
            int StartX = 1; // players x start position
            int StartY = 1; // players y start position
            int num = 0; // don't change
            int player; // don't change
            int wallN = 2; // number the wall is
            int[] wall = new int[] {
                5 + (X * 2),
                6 + (X * 2),
                7 + (X * 2),
            }; // places for walls to go -- (X + (_ + Y)

            //header
            Console.Write("|");
            for (int i = 0; i < X / 2 - 2; i++)
            {
                Console.Write("  ");
            }
            Console.WriteLine(Name + "\n");

            //map layout
            string layout = "";
            int total = X * Y;
            int[] map = new int[total];
            player = StartX + (StartY * X);
            map[player] = ChangedN;
            foreach (int i in wall)
            {
                map[i] = wallN;
            }

            Numbers();
            Move();

            void Move()
            {

                while (true)
                {
                    Console.Clear();
                    Console.Write(layout);
                    try
                    {
                        for (int i = 0; i < map.Length; i++) map[i] = 0;
                        layout = string.Empty;
                        var r = Console.ReadKey();
                        string input = r.KeyChar.ToString();
                        int O = map.Length;

                        int a = Array.Find<int>(wall, (arr => arr == (player - 1)));
                        int w = Array.Find<int>(wall, (arr => arr == (player - X)));
                        int s = Array.Find<int>(wall, (arr => arr == (player + X)));
                        int d = Array.Find<int>(wall, (arr => arr == (player + 1)));
                        if (input == "a" && (player - 1) >= 0 && (player - 1) != 2 && a == 0) player -= 1;
                        if (input == "w" && (player - X) >= 0 && (player - X) != 2 && w == 0) player -= X;
                        if (input == "s" && (player + X) < O && (player + X) != 2 && s == 0) player += X;
                        if (input == "d" && (player + 1) < O && (player + 1) != 2 && d == 0) player += 1;

                        map[player] = ChangedN;


                        Numbers();
                    }
                    catch
                    {
                        Console.WriteLine("An error occured");
                    }
                }
            }
            void Numbers()
            {
                foreach (int i in wall)
                {
                    map[i] = wallN;
                }
                for (int l = 0; l < Y; l++)
                {
                    for (int j = 0; j < X; j++)
                    {
                        layout += (map[num] + " ");
                        num++;
                        if (num >= map.Length) num = 0;
                    }
                    layout += "\n";
                }
            }
        }
    #endregion
    }
}