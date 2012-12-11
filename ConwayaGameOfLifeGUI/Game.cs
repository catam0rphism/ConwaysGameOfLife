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
#warning убрать public!
        public Thread gameThread; 

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
            System.Drawing.Size s = Config.Conf.worldSize;

            lg = new LifeGame(new bool[s.Width,s.Height]);

            gameThread = new Thread(gameThreadMain);
            gameThread.Start();
            
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

        private void gameThreadMain()
        {
            generationTimer = new System.Timers.Timer(Config.Conf.timerInterval);
            generationTimer.Elapsed += gameMethod;
        }
        private void gameMethod(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (lg)
            {
                //try
                //{
                    CellsImage c = lg.NextGeneration();
                    // Передача изображения обработчику события
                    if (generationUpdate != null)
                    {
                        generationUpdate(this, new CellsEventArgs(c));
                    }

                //}
                //finally
                //{
                //    lg.Cells.Dispose();
                //}
            }
        }

        public void Start()
        {
            //lg.Cells = Cells.Cells;
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
            get { return lg.Cells; }
            set
            {
                lock (lg)
                {
                    lg.Cells = value;
                }
            }
        }
        public void Update(CellsImage cells,bool start = false)
        {
            lock (lg)
            {
                lg = new LifeGame(cells);
            }

            if (start)
            {
                 Start();
            }
        }

        public event EventHandler<CellsEventArgs> generationUpdate;
        public class CellsEventArgs: EventArgs
        {
            public CellsEventArgs(CellsImage c)
            {
                cells = c;
            }
            public CellsImage cells;
        }
    }
}
