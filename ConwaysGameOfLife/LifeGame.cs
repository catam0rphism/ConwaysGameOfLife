using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ConwaysGameOfLife
{
    public class LifeGame
    {
        // Constructors
        public LifeGame(bool[,] cells)
        {
            // инициализация полей
            width = cells.GetLength(0);
            heigth = cells.GetLength(1);
            this.cells = cells;
        }

        // TODO: переписать на CellImage !
        bool[,] cells;

        int width;
        int heigth;

        public bool[,] Cells

        {
            get { return cells; }
            /* private */
            set
            {
                width = cells.GetLength(0);
                heigth = cells.GetLength(1);
                cells = value; 
            }
        }

        public bool[,] NextGeneration()
        {
            // buffer
            bool[,] buff = new bool[width, heigth];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    buff[i, j] = Analize(i, j);
                }
            }
            cells = buff;
            return Cells;
        }

        //public void ShowConsole()
        //{
        //    for (int i = 0; i < width; i++)
        //    {
        //        for (int j = 0; j < heigth; j++)
        //        {
        //            Console.Write(cells[i, j] ? '#' : '.');
        //        }
        //        Console.Write("\n");
        //    }
        //}
        
        int livecount(int w, int h)
        {
            int c = 0;
            // ТУТ ВСЕ РАБОТАЕТ ТАК КАК НАДО!
            #region livecount
            if (w == 0)
            {
                #region left border
                if (h == 0)
                {
                    if (cells[w + 1, h + 1]) c++;
                    if (cells[w + 1, h]) c++;
                    if (cells[w + 1, heigth - 1]) c++;
                    if (cells[w, heigth - 1]) c++;
                    if (cells[w, h + 1]) c++;
                    if (cells[width - 1, h + 1]) c++;
                    if (cells[width - 1, h]) c++;
                    if (cells[width - 1, heigth - 1]) c++;
                    // top-left border
                }
                else if (h == heigth - 1)
                {
                    if (cells[w + 1, 0]) c++;
                    if (cells[w + 1, h]) c++;
                    if (cells[w + 1, h - 1]) c++;
                    if (cells[w, h - 1]) c++;
                    if (cells[w, 0]) c++;
                    if (cells[width - 1, 0]) c++;
                    if (cells[width - 1, h]) c++;
                    if (cells[width - 1, h - 1]) c++;
                    // down-left border
                }
                else
                {
                    // left border
                    if (cells[w + 1, h + 1]) c++;
                    if (cells[w + 1, h]) c++;
                    if (cells[w + 1, h - 1]) c++;
                    if (cells[w, h - 1]) c++;
                    if (cells[w, h + 1]) c++;
                    if (cells[width - 1, h + 1]) c++;
                    if (cells[width - 1, h]) c++;
                    if (cells[width - 1, h - 1]) c++;
                }
                #endregion
            }
            else if (w == width - 1)
            {
                #region rigth border
                if (h == 0)
                {
                    // top-rigth border
                    if (cells[ 0, h + 1]) c++;
                    if (cells[ 0, h]) c++;
                    if (cells[ 0, heigth - 1]) c++;
                    if (cells[w, heigth - 1]) c++;
                    if (cells[w, h + 1]) c++;
                    if (cells[w - 1, h + 1]) c++;
                    if (cells[w - 1, h]) c++;
                    if (cells[w - 1, heigth - 1]) c++;
                }
                else if (h == heigth - 1)
                {
                    if (cells[ 0,  0]) c++;
                    if (cells[ 0, h]) c++;
                    if (cells[ 0, h - 1]) c++;
                    if (cells[w, h - 1]) c++;
                    if (cells[w,  0]) c++;
                    if (cells[w - 1,  0]) c++;
                    if (cells[w - 1, h]) c++;
                    if (cells[w - 1, h - 1]) c++;
                    // down-rigth border                    
                }
                else
                {
                    // rigth border
                    if (cells[ 0, h + 1]) c++;
                    if (cells[ 0, h]) c++;
                    if (cells[ 0, h - 1]) c++;
                    if (cells[w, h - 1]) c++;
                    if (cells[w, h + 1]) c++;
                    if (cells[w - 1, h + 1]) c++;
                    if (cells[w - 1, h]) c++;
                    if (cells[w - 1, h - 1]) c++;
                }
                #endregion
            }
            else
            {
                #region center region
                if (h == 0)
                {
                    // top border
                    if (cells[w + 1, h + 1]) c++;
                    if (cells[w + 1, h]) c++;
                    if (cells[w + 1, heigth-1]) c++;
                    if (cells[w, heigth - 1]) c++;
                    if (cells[w, h + 1]) c++;
                    if (cells[w - 1, h + 1]) c++;
                    if (cells[w - 1, h]) c++;
                    if (cells[w - 1, heigth - 1]) c++;
                }
                else if (h == heigth - 1)
                {
                    // down border
                    if (cells[w + 1, 0]) c++;
                    if (cells[w + 1, h]) c++;
                    if (cells[w + 1, h - 1]) c++;
                    if (cells[w, h - 1]) c++;
                    if (cells[w,  0]) c++;
                    if (cells[w - 1,  0]) c++;
                    if (cells[w - 1, h]) c++;
                    if (cells[w - 1, h - 1]) c++;
                }
                else
                {
                    #region normal
                    if (cells[w + 1, h + 1]) c++;
                    if (cells[w + 1, h]) c++;
                    if (cells[w + 1, h - 1]) c++;
                    if (cells[w, h - 1]) c++;
                    if (cells[w, h + 1]) c++;
                    if (cells[w - 1, h + 1]) c++;
                    if (cells[w - 1, h]) c++;
                    if (cells[w - 1, h - 1]) c++;
                    #endregion
                }
                #endregion
            }
            #endregion
            return c;
        }
        bool Analize(int w,int h)
        {
            int c = livecount(w, h);
            // гениально :)
            return cells[w, h] ? (c == 2) | (c == 3) : (c == 3);
            // игровые правила
            // TODO: поддежка других автоматов (правила как один Enum)
        }
    }
}
