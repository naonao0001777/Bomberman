using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bomberman
{
    class Program
    {
        
        // 上限値
        public const int MAX_HEIGHT = 13;
        public const int MAX_WIDTH = 31;

        // セルアスキーアート
        enum CellAA
        {
            CELL_TYPE_NONE,
            CELL_TYPE_HARD,
            CELL_TYPE_SOFT,
            CELL_TYPE_BOMB,
            CELL_EXPLOSION,
            PLAYER,
            MONSTER
        }

        // 方向移動
        enum Directions
        {
            DIRECTION_NORTH,
            DIRECTION_WEST,
            DIRECTION_SOUTH,
            DIRECTION_EAST,
            DIRECTION_MAX
        }
        
        static void Main(string[] args)
        {

            int[,] cells = new int[MAX_HEIGHT, MAX_WIDTH];

            int x = 0;
            int y = 0;

            int cursorY = 1;
            int cursorX = 1;

            int bombTimer = 10;

            bool bombFlg = false;

            bool bombSetFlg = false;
            // リセット処理
            ResetCells();

            while (true)
            {
                Console.Clear();
                DrawCells(bombFlg,bombSetFlg);
            }

            // セル配置の初期情報
            void ResetCells()
            {
                Random rSoft = new Random();

                Random rMon = new Random();

                for (y = 0; y < MAX_HEIGHT; y++)
                {
                    for (x = 0; x < MAX_WIDTH; x++)
                    {
                        if (x == cursorX && y == cursorY) // プレイヤー配置
                        {
                            cells[y, x] = (int)CellAA.PLAYER;
                        }
                        else if (y == 0 || y == MAX_HEIGHT - 1) // 上下ハードブロック配置
                        {
                            cells[y, x] = (int)CellAA.CELL_TYPE_HARD;
                        }
                        else if (x == 0 || x == MAX_WIDTH - 1) // 左右ハードブロック配置
                        {
                            cells[y, x] = (int)CellAA.CELL_TYPE_HARD;
                        }
                        else if (y % 2 == 0 && x % 2 == 0) // 中マスハードブロック配置
                        {
                            cells[y, x] = (int)CellAA.CELL_TYPE_HARD;
                        }
                        else // 移動可能マス
                        {
                            cells[y, x] = (int)CellAA.CELL_TYPE_NONE;
                        }

                        // CELL_TYPE_SOFTをランダム作成
                        CreateRandomCells(rSoft);

                        // MONSTERをランダム作成
                        CreateRandomMonster(rMon);

                    }
                    Console.Write("\n");
                }
            }

            // 画面を描画
            void DrawCells(bool bFlg, bool bSetFlg)
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
                            else if (cells[y,x] == (int)CellAA.CELL_EXPLOSION)
                            {
                                Console.Write("※");
                                cells[y, x] = (int)CellAA.CELL_TYPE_NONE;
                            }
                            else
                            {
                                if (cells[y, x] == (int)CellAA.CELL_TYPE_SOFT)
                                {
                                    Console.Write("□");
                                }
                                else if (cells[y, x] == (int)CellAA.MONSTER)
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
                            if (!bFlg)
                            {
                                bSetFlg = PutBomb(bFlg, bSetFlg);
                            }
                            // 爆弾セットフラグが立っていた場合
                            if (bSetFlg)
                            {       
                                // 時間が0秒以下になると爆発を起こす
                                if (bombTimer <= 0)
                                {
                                    bombSetFlg = false;
                                    bombFlg = false;
                                    ExplosionBomb();
                                    bombTimer = 10;
                                }
                                // 爆弾がセットされた後の時限
                                else
                                {
                                    bombTimer--;
                                    switch (bombTimer)
                                    {
                                        case 10:
                                            Console.Write("10");
                                            break;
                                        case 9:
                                            Console.Write("９");
                                            break;
                                        case 8:
                                            Console.Write("８");
                                            break;
                                        case 7:
                                            Console.Write("７");
                                            break;
                                        case 6:
                                            Console.Write("６");
                                            break;
                                        case 5:
                                            Console.Write("５");
                                            break;
                                        case 4:
                                            Console.Write("４");
                                            break;
                                        case 3:
                                            Console.Write("３");
                                            break;
                                        case 2:
                                            Console.Write("２");
                                            break;
                                        case 1:
                                            Console.Write("１");
                                            break;
                                        case 0:
                                            Console.Write("０");
                                            break;

                                    }
                                }
                                ////BombTimer(bombFlg,bombSetFlg);
                                //await Task.Run(async () =>
                                //{
                                //    Task.Delay(300);
                                //    cells[y, x] = (int)CellAA.CELL_TYPE_NONE;
                                //    bFlg = false;
                                //    bSetFlg = false;
                                //});
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
                        if (cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_HARD ||
                            cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_SOFT ||
                            cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_BOMB)
                        {
                            cursorY++;
                        }
                        break;
                    case "S":
                        cursorY++;
                        if (cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_HARD ||
                            cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_SOFT ||
                            cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_BOMB)
                        {
                            cursorY--;
                        }
                        break;
                    case "A":
                        cursorX--;
                        if (cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_HARD ||
                            cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_SOFT ||
                            cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_BOMB)
                        {
                            cursorX++;
                        }
                        break;
                    case "D":
                        cursorX++;
                        if (cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_HARD ||
                            cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_SOFT ||
                            cells[cursorY, cursorX] == (int)CellAA.CELL_TYPE_BOMB)
                        {
                            cursorX--;
                        }
                        break;
                    default:
                        cells[cursorY, cursorX] = (int)CellAA.CELL_TYPE_BOMB;

                        break;
                }

            }

            // 爆風の描画
            void ExplosionBomb()
            {
                for (int i = 0; i < 2; i++)
                {
                    cells[y - i, x] = (int)CellAA.CELL_EXPLOSION;
                    cells[y, x - i] = (int)CellAA.CELL_EXPLOSION;
                    cells[y + i, x] = (int)CellAA.CELL_EXPLOSION;
                    cells[y, x + i] = (int)CellAA.CELL_EXPLOSION;
                }
            }

            // 爆弾を配置
            bool PutBomb(bool bFlg,bool bombstFlg)
            {
                bFlg = false;
                bombstFlg = true;
                
                return bombstFlg;
            }            

            //// 爆弾時限関数
            //void BombTimer(bool bombFlg, bool bombstFlg)
            //{
            //    DateTime standDate = DateTime.Now;

            //    bombTimer = standDate.Second + 3;

            //    // 爆弾の時限を3秒後にセッティング                           
            //    bombFlg = true;

            //    DateTime end = DateTime.Now;

            //    int time = bombTimer - end.Second;
                
            //    if (time <= 0)
            //    {
            //        bombFlg = false;
            //        bombstFlg = false;
            //    }
            //}

            // CELL_TYPE_SOFTをランダム配置
            void CreateRandomCells(Random rSoft)
            {

                bool softCellFlg = false;

                int val = rSoft.Next(2); // 0か1を乱数で作成

                if (val == 1)
                {
                    softCellFlg = true;
                }

                if (cells[y, x] == (int)CellAA.CELL_TYPE_NONE && softCellFlg)
                {
                    cells[y, x] = (int)CellAA.CELL_TYPE_SOFT;
                }


            }

            // MONSTERをランダム配置
            void CreateRandomMonster(Random rMon)
            {
                bool monstFlg = false;

                int a = rMon.Next(2);
                if (a == 1)
                {
                    monstFlg = true;
                }

                // CELL_TYPE_NONEかつフラグが立っていたら配置
                if (cells[y, x] == (int)CellAA.CELL_TYPE_NONE && monstFlg)
                {
                    cells[y, x] = (int)CellAA.MONSTER;
                }
            }

            // MONSTERの人口知能
            void Direction()
            {

            }

        }
    }
}

