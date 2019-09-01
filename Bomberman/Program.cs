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
        
        enum CellAA
        {
            CELL_TYPE_NONE, 
            CELL_TYPE_HARD, 
            CELL_TYPE_SOFT, 
            CELL_TYPE_BOMB, 
            PLAYER,         
            MONSTER         
        }

        static void Main(string[] args)
        {
            int[,] cells = new int[MAX_HEIGHT, MAX_WIDTH];

            int x = 0;
            int y = 0;

            int cursorX = 1;
            int cursorY = 1;

            int bombTimer = 0;
            bool bombFlg = false;

            bool bombSetFlg = false;

            // リセット処理
            ResetCells();

            while (true)
            {
                Console.Clear();
                DrawCells();

            }
            
            // セル配置の初期情報
            void ResetCells()
            {
                Random rY = new Random();
                Random rX = new Random();

                Random randMonstY = new Random();
                Random randMonstX = new Random();
                
                for (y = 0; y < MAX_HEIGHT; y++)
                {
                    for (x = 0; x < MAX_WIDTH; x++)
                    {
                        if (x == cursorX && y == cursorY)
                        {
                            cells[y, x] = (int)CellAA.PLAYER;
                        }
                        else if (y == 0 || y == MAX_HEIGHT - 1)
                        {
                            cells[y, x] = (int)CellAA.CELL_TYPE_HARD;
                        }
                        else if (x == 0 || x == MAX_WIDTH - 1)
                        {
                            cells[y, x] = (int)CellAA.CELL_TYPE_HARD;
                        }
                        else if (y % 2 == 0 && x % 2 == 0)
                        {
                            cells[y, x] = (int)CellAA.CELL_TYPE_HARD;
                        }
                        else
                        {
                            cells[y, x] = (int)CellAA.CELL_TYPE_NONE;
                        }

                        // モンスター配置
                        bool monstFlg = false;

                        int a = randMonstY.Next(2);
                        if (a == 1)
                        {
                            monstFlg = true;
                        }

                        // CELL_TYPE_SOFTをランダム配置
                        bool softCellFlg = false;

                        int val = randMonstY.Next(2);
                        if (val == 1)
                        {
                            softCellFlg = true;
                        }
                        
                        if (cells[y, x] == (int)CellAA.CELL_TYPE_NONE && softCellFlg)
                        {
                            cells[y,x] = (int)CellAA.CELL_TYPE_SOFT;
                        }

                        //モンスターを配置する
                        //if (cells[y, x] == (int)CellAA.CELL_TYPE_NONE && monstFlg)
                        //{

                        //    cells[y, x] = (int)CellAA.MONSTER;
                        //}
                    }
                    Console.Write("\n");
                }
            }

            // 画面を描画
            void DrawCells()
            {
                for (y = 0; y < MAX_HEIGHT; y++)
                {
                    for (x = 0; x < MAX_WIDTH; x++)
                    {
                        if (cells[y, x] != (int)CellAA.CELL_TYPE_BOMB)
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
                                if (cells[y, x] == (int)CellAA.CELL_TYPE_SOFT)
                                {
                                    Console.Write("□");
                                }
                                else if (cells[y,x] == (int)CellAA.MONSTER)
                                {
                                    Console.Write("敵");
                                }
                                else
                                {
                                    Console.Write("　");
                                }
                            }
                        }
                        else if (cells[y, x] == (int)CellAA.CELL_TYPE_BOMB)
                        {
                            bombSetFlg = false;
                            if (bombSetFlg)
                            {
                                DateTime standDate = DateTime.Now;

                                bombTimer = standDate.Second + 3;
                            }
                            // 爆弾の時限を3秒後にセッティング                           
                            bombFlg = true;

                            DateTime end = DateTime.Now;

                            int a = bombTimer - end.Second;

                            if (a <= 0)
                            {
                                bombFlg = false;
                            }

                            if (bombFlg)
                            {
                                Console.Write("●");
                                bombSetFlg = true;
                            }
                            else if (!bombFlg)
                            {
                                cells[y, x] = (int)CellAA.CELL_TYPE_NONE;
                                
                            }
                        }
                    }
                    Console.Write("\n");
                }

                // カーソル移動
                MoveCursor();

            }

            // カーソル操作処理
            void MoveCursor()
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                string key = keyInfo.Key.ToString();

                switch (key)
                {
                    case "W":
                        cursorY--;
                        if (cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_HARD || cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_SOFT)
                        {
                            cursorY++;
                        }
                        break;
                    case "S":
                        cursorY++;
                        if (cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_HARD || cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_SOFT)
                        {
                            cursorY--;
                        }
                        break;
                    case "A":
                        cursorX--;
                        if (cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_HARD || cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_SOFT)
                        {
                            cursorX++;
                        }
                        break;
                    case "D":
                        cursorX++;
                        if (cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_HARD || cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_SOFT)
                        {
                            cursorX--;
                        }
                        break;
                    default:
                        cells[cursorY, cursorX] = (int)CellAA.CELL_TYPE_BOMB;
                       
                        break;
                }

                // 爆風の描画
                void ExplosionBomb()
                {

                }

                // 爆弾の時間をセッティング
                void BombTimer()
                {

                }


            }

        }
    }
}
