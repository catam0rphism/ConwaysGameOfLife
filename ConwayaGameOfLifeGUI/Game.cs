using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConwaysGameOfLife;
using System.Threading;

namespace ConwayaGameOfLifeGUI
{
    class Game
    {
        System.Timers.Timer generationTimer;
        LifeGame lg;
        CellsImage cells;

        public void Random()
        {
            // размер из конфига
            System.Drawing.Size s = Config.Conf.worldSize;
            bool[,] cells = new bool[s.Width,s.Height];

            Random r = new Random();
            for (int i = 0; i < s.Height; i++)
            {
                for (int j = 0; j < s.Width; j++)
                {
                    cells[j, i] = (r.Next() & 0x01) == 1;
                }
            }
            // последовательная инициализация
            CellsImage c = new CellsImage(cells);

            this.Cells = c;
        }

        public Game()
        {
            generationTimer = new System.Timers.Timer(Config.Conf.timerInterval);
            generationTimer.Elapsed += gameMethod;
            System.Drawing.Size s = Config.Conf.worldSize;
            cells = new CellsImage(new bool[s.Width,s.Height]);

            lg = new LifeGame(cells.Cells);
            
        }
        public Game(CellsImage cells)
        {
            generationTimer = new System.Timers.Timer(Config.Conf.timerInterval);
            generationTimer.Elapsed += gameMethod;
            int w = cells.Cells.GetLength(0);
            int h = cells.Cells.GetLength(1);
            Config.Conf.worldSize = new System.Drawing.Size(w, h);

            lg = new LifeGame(cells.Cells);

        }

        private void gameMethod(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {                
                cells.Cells = lg.NextGeneration();

                lock (cells)
                {
                    // Передача изображения обработчику события
                    if (generationUpdate != null)
                    {
                        generationUpdate(this, new CellsEventArgs(cells));
                    }
                }
            }
            finally
            {
                cells.Dispose();
            }
        }

        public void Start()
        {
            lg.Cells = Cells.Cells;
            lock (generationTimer)
            {
                generationTimer.Start();
            }
        }
        public void Stop()
        {
            lock (generationTimer)
            {
                generationTimer.Stop();
            }
        }

        public CellsImage Cells
        {
            get { return cells; }
            set
            {
                if (generationTimer.Enabled)
                    generationTimer.Stop();

                cells = value;
                lg.Cells = value.Cells;
            }
        }
        public void Update(CellsImage cells,bool start = false)
        {
            // TODO: настройка паузы при редактировании поля
            if (generationTimer.Enabled)
                generationTimer.Stop();
            // инициализация поля(Field) новым полем(игровым), переданным в параметре
            this.cells = cells;

            // обновление lg
            lg.Cells = cells.Cells;

            if (start)
            {
                Start();
            }
        }

        public event EventHandler<CellsEventArgs> generationUpdate;
        public class CellsEventArgs
            :EventArgs
        {
            public CellsEventArgs(CellsImage c)
            {
                cells = c;
            }
            public CellsImage cells;
        }

        public void Refresf()
        {
            cells.Image = CellsImage.GetImg(cells.Cells);
            lg.Cells = Cells.Cells;
        }
    }
}
