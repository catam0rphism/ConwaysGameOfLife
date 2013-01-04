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

            // переделать
            switch (Config.Conf.GameRules)
            {
                case Rules.Assimilation:
                    return cells[w, h] ? c == 4 || c == 5 || c == 6 || c == 7 : c == 3 || c == 4 || c == 5;

                case Rules.Default:
                    return cells[w, h] ? c == 2 || c == 3 : c == 3;

                case Rules.HighLife:
                    return cells[w, h] ? c == 2 || c == 3 : c == 3 || c == 6;

                case Rules.Gnarl:
                    return cells[w, h] ? c == 1 : c == 1;
                
                case Rules.Replicator:
                    return cells[w, h] ? c==1||c == 3 || c == 5 || c == 7 : c==1||c == 3 || c == 5 || c == 7;

                case Rules.test:
                    return cells[w, h] ? c == 3 || c == 5 || c == 7 : c == 3 || c == 5 || c == 7;

                default:
                    throw new Exception("правила отсутствуют в конфиге (назначить дефолтное значение)");
            }
        }
    }

    public enum Rules
    {
        Default,
        Gnarl,
        Assimilation,
        HighLife,
        Replicator,
        test
        // ...
    }
}
