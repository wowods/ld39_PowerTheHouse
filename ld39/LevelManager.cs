using ld39.Entities;
using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace ld39
{
    static class LevelManager
    {
        /// <summary>
        /// 0 = Empty tiles
        /// 1 = Grass tiles
        /// 2 = Blocked
        /// 3 = House
        /// 4 = Poles
        /// 5 = Power Plant
        /// 6 = Wall
        /// 7 = Water
        /// </summary>
        public static string[][] CurrentLevel;
        public static int[,] ElectrifiedLevel = new int[10, 10];

        public static void LoadLevel(int level)
        {
            try
            {
                Stream stream = TitleContainer.OpenStream("Content/level" + level + ".txt");
                StreamReader reader = new StreamReader(stream);

                string[][] levelData = new string[10][];
                int row = 0;
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    levelData[row] = line.Split(',');
                    row += 1;
                }

                CurrentLevel = levelData;
                preparingLevel(levelData);
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        public static void ChangeLevelData(int data, int x, int y)
        {
            // Data yang boleh diinput hanya 1-7
            if (data < 1 || data > 7) return;

            if (CurrentLevel[x][y].Equals("0"))
                return;
            else
                CurrentLevel[x][y] = data.ToString();
        }

        private static void preparingLevel(string[][] data)
        {
            bool cursorIsSet = false;
            for (int j = 0; j < data.Length; j++)
            {
                for (int i = 0; i < data[j].Length; i++)
                {
                    if (!cursorIsSet && !String.Equals(data[j][i], "0"))
                    {
                        Cursor.Instance.Position = new Point(i, j);
                        cursorIsSet = true;
                    }

                    switch (data[j][i])
                    {
                        case "1":
                            EntityManager.Add(new Grass(i, j));
                            break;
                        case "2":
                            EntityManager.Add(new Grass(i, j));
                            EntityManager.Add(new Blocked(i, j));
                            break;
                        case "3":
                            EntityManager.Add(new Grass(i, j));
                            EntityManager.Add(new House(i, j));
                            break;
                        case "4":
                            EntityManager.Add(new Grass(i, j));
                            EntityManager.Add(new Pole(i, j));
                            break;
                        case "5":
                            EntityManager.Add(new Grass(i, j));
                            EntityManager.Add(new PowerPlant(i, j));
                            break;
                        case "6":
                            EntityManager.Add(new Grass(i, j));
                            EntityManager.Add(new Wall(i, j));
                            break;
                        case "7":
                            EntityManager.Add(new Water(i, j));
                            break;
                        default:
                            break;
                    }
                }
            }

            GameMain.State = GameMain.GAMESTATE.Playing;
        }

        public static void ClearAll()
        {
            CurrentLevel = new string[10][];
            ElectrifiedLevel = new int[10, 10];
        }
    }
}
