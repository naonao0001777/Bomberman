using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Program
    {
        public const int MAX_HEIGHT = 13;
        public const int MAX_WIDTH = 31;

        enum cellAA
        {
            CELL_TYPE_NONE,
            CELL_TYPE_HARD,
            CELL_TYPE_SOFT,
        }

        static void Main(string[] args)
        {
            int[,] cells = new int[MAX_HEIGHT, MAX_WIDTH];

            int x = 0;
            int y = 0;

            int cursorY = 0;
            int cursorX = 0;
            
            while (true)
            {
                Console.Clear();
                DrawCells();
                
            }
            
            // 画面を作成
            void DrawCells()
            {
                for (y = 0; y < MAX_HEIGHT; y++)
                {
                    for (x = 0; x < MAX_WIDTH; x++)
                    {
                        if (x == cursorX && y == cursorY)
                        {
                            Console.Write("＠");
                        }
                        else if (y == 0 || y == MAX_HEIGHT - 1)
                        {

                            Console.Write("■");
                        }
                        else if (x == 0 || x == MAX_WIDTH - 1)
                        {
                            Console.Write("■");
                        }
                        else
                        {
                            Console.Write("");
                        }
                    }
                    Console.Write("\n");
                }

                MoveCursor();

                Console.ReadKey(true);
            }
            // カーソル移動
            void MoveCursor()
            {
                ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
                string key = keyInfo.Key.ToString();

                switch (key)
                {
                    case "W":
                        cursorY--;
                        break;
                    case "S":
                        cursorY++;
                        break;
                    case "A":
                        cursorX--;
                        break;
                    case "D":
                        cursorX++;
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
