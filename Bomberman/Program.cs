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
            CELL_TYPE_NONE, // "　"
            CELL_TYPE_HARD, // "■"
            CELL_TYPE_SOFT, // "□"
            CELL_TYPE_BOMB, // "●"
            PLAYER,         // "＠"
            MONSTER         // "敵"
        }
       
        static void Main(string[] args)
        {
            int[,] cells = new int[MAX_HEIGHT, MAX_WIDTH];

            int x = 0;
            int y = 0;

            int cursorY = 1;
            int cursorX = 1;

            ResetCells();

            while (true)
            {
                Console.Clear();
                DrawCells();

            }
            
            // セル情報
            void ResetCells()
            {  
                Random rY = new Random();
                Random rX = new Random();

                for (y = 0; y < MAX_HEIGHT; y++)
                {
                    for (x = 0; x < MAX_WIDTH; x++)
                    {
                        if (x == cursorX && y == cursorY)
                        {
                            cells[y, x] = (int)cellAA.PLAYER;
                        }
                        else if (y == 0 || y == MAX_HEIGHT - 1)
                        {
                            cells[y, x] = (int)cellAA.CELL_TYPE_HARD;
                        }
                        else if (x == 0 || x == MAX_WIDTH - 1)
                        {
                            cells[y, x] = (int)cellAA.CELL_TYPE_HARD;
                        }
                        else if (y % 2 == 0 && x % 2 == 0)
                        {
                            cells[y, x] = (int)cellAA.CELL_TYPE_HARD;
                        }
                        else
                        {
                            cells[y, x] = (int)cellAA.CELL_TYPE_NONE;
                        }

                        if (cells[y, x] == (int)cellAA.CELL_TYPE_NONE)
                        {
                            cells[rY.Next(13), rX.Next(31)] = (int)cellAA.CELL_TYPE_SOFT;
                        }
                    }
                    Console.Write("\n");
                }
            }

            // 画面を作成
            void DrawCells()
            {
                for (y = 0; y < MAX_HEIGHT; y++)
                {
                    for (x = 0; x < MAX_WIDTH; x++)
                    {
                        if (cells[y, x] != (int)cellAA.CELL_TYPE_BOMB)
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
                            else if (y % 2 == 0 && x % 2 == 0)
                            {
                                Console.Write("■");
                            }
                            else
                            {
                                if (cells[y, x] == (int)cellAA.CELL_TYPE_SOFT)
                                {
                                    Console.Write("□");
                                }
                                else
                                {
                                    Console.Write("　");
                                }
                            }
                        }
                        else if(cells[y, x] == (int)cellAA.CELL_TYPE_BOMB)
                        {
                            Console.Write("●");
                        }
                    }
                    Console.Write("\n");
                }

                MoveCursor();

            }
            // カーソル移動
            void MoveCursor()
            {

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                string key = keyInfo.Key.ToString();

                switch (key)
                {
                    case "W":
                        cursorY--;
                        if (cells[cursorY, cursorX] == (int)cellAA.CELL_TYPE_HARD)
                        {
                            cursorY++;
                        }
                        break;
                    case "S":
                        cursorY++;
                        if (cells[cursorY, cursorX] == (int)cellAA.CELL_TYPE_HARD)
                        {
                            cursorY--;
                        }
                        break;
                    case "A":
                        cursorX--;
                        if (cells[cursorY, cursorX] == (int)cellAA.CELL_TYPE_HARD)
                        {
                            cursorX++;
                        }
                        break;
                    case "D":
                        cursorX++;
                        if (cells[cursorY, cursorX] == (int)cellAA.CELL_TYPE_HARD)
                        {
                            cursorX--;
                        }
                        break;
                    default:
                        cells[cursorY, cursorX] = (int)cellAA.CELL_TYPE_BOMB;
                        break;
                }


            }

        }
    }
}
