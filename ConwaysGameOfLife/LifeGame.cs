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
            this.cells = new CellsImage(cells);
            width = cells.GetLength(0);
            heigth = cells.GetLength(1);
        }

        CellsImage cells;
        int width, heigth;
        
        public CellsImage Cells
            
        {
            get { return cells; }
            set
            {
                cells = value;
                width = cells.Cells.GetLength(0);
                heigth = cells.Cells.GetLength(1);
            }
        }

        public CellsImage NextGeneration()
        {
            // TODO: Оптимизировать (список клеток, которые изменялись)
            bool[,] buff = new bool[width, heigth];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    buff[i, j] = Analize(i, j);
                }
            }
            cells.Cells = buff;
            return Cells;
        }
        
        int livecount(int w, int h)
        {
            int c = 0;

            if (cells[w == 0 ? width - 1 : w - 1, h]) c++;
            if (cells[w == heigth - 1 ? 0 : w + 1, h]) c++;
            if (cells[w, h == 0 ? heigth - 1 : h - 1]) c++;
            if (cells[w, h == heigth - 1 ? 0 : h + 1]) c++;
            if (cells[w == width - 1 ? 0 : w + 1, h == heigth - 1 ? 0 : h + 1]) c++;
            if (cells[w == width - 1 ? 0 : w + 1, h == 0 ? heigth - 1 : h - 1]) c++;
            if (cells[w == 0 ? width - 1 : w - 1, h == heigth - 1 ? 0 : h + 1]) c++;
            if (cells[w == 0 ? width - 1 : w - 1, h == 0 ? heigth - 1 : h - 1]) c++;
            
            return c;
        }
        bool Analize(int w,int h)
        {
            int c = livecount(w, h);
            return cells[w, h] ? c == 2 | c == 3 : c == 3;
            // TODO: поддежка других автоматов (правила как один Enum)
        }
    }
}
