using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using GameOfLife;

namespace GameOfLifeConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = StartGameFromFile("oscilator3.txt");


            while (true)
            {
                Console.Write(game.ToString());

                Thread.Sleep(200);
                Console.Clear();
                game.UpdateState();
            }
        }

        private static Game StartGameFromFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                var line = reader.ReadLine();
                var gameSize = line.Length;
                var lineNum = 0;
                var initialState = new List<(int, int)>();

                while (line != null)
                {
                    for (var i = 0; i < gameSize; i++)
                    {
                        if(line[i] == 'o')
                            initialState.Add((lineNum, i));
                    }

                    line = reader.ReadLine();
                    lineNum++;
                }

                return new Game(gameSize, initialState);
            }
        }
    }
}
