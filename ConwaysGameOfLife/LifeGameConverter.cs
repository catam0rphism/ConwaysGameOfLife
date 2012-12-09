using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using ConwaysGameOfLife;

namespace ConwaysGameOfLife
{
    public static class LifeGameConverter
    {
        public static void SaveToFile(bool[,] cells,string FileName)
        {
            try
            {                
                StreamWriter sw = new StreamWriter(FileName);
                using (sw)
                {
                    sw.Write(cells.GetLength(0) + " " + cells.GetLength(1)+"\n");
                    for (int i = 0; i < cells.GetLength(1); i++)
                    {
                        for (int j = 0; j < cells.GetLength(0); j++)
                        {
                            sw.Write(cells[j, i] ? '1' : '0');
                        }
                        sw.Write("\n");
                    }
                }
            }
            catch (IOException)
            {
                throw;
            }
        }
        public static bool[,] LoadFromFile(string FileName)
        {
            try
            {
                StreamReader sr = new StreamReader(FileName);
                using (sr)
                {
                    string[] s = sr.ReadLine().Split(' ');
                    int w = int.Parse(s[0]);
                    int h = int.Parse(s[1]);
                    bool[,] cells = new bool[w, h];
                    for (int i = 0; i < h; i++)
                    {
                        string st = sr.ReadLine();
                        for (int j = 0; j < w; j++)
                        {
                            cells[j, i] = st[j] == '1';
                        }
                    }
                    return cells;
                }                
            }
            catch (IOException)
            {                
                throw;
            }
        }

        public static void SaveToImage(bool[,] cells, string FileName)
        {
            CellsImage.GetImg(cells).Save(FileName);
        }
    }
}
