using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using ConwaysGameOfLife;
using System.Drawing;

namespace ConwaysGameOfLife
{
    public static class LifeGameConverter
    {
        /// <summary>
        /// Сохраняет текстовое представление поля на диск
        /// </summary>
        /// <param name="cells">Игровое поле</param>
        /// <param name="FileName">Имя конечного файла</param>
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
        /// <summary>
        /// Загружает игровое поле из файла с текстовым представлением
        /// </summary>
        /// <param name="FileName">Имя файла с текстовым представлением поля</param>
        /// <returns></returns>
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

        /// <summary>
        /// Сохраняет текущее сстояние поля в виде изображения
        /// </summary>
        /// <param name="cells">Массив игрового поля</param>
        /// <param name="FileName">Имя конечного файла</param>
        public static void SaveToImage(bool[,] cells, string FileName)
        {
            CellsImage.GetImg(cells).Save(FileName);
        }
        /// <summary>
        /// Загружает игровое поле из файла с изображением (Учитывая текущую цветовую схему!)
        /// </summary>
        /// <param name="FileName">Имя файла с изображением</param>
        /// <returns></returns>
        public static CellsImage LoadFromImage(string FileName)
        {
            using (FileStream fs = new FileStream(FileName, FileMode.Open))
            {
                Bitmap img = new Bitmap(Bitmap.FromStream(fs));
                CellsImage c = new CellsImage();
                c.Image = img;
                return c;
            }
            
        }
    }
}
