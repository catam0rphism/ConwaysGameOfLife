using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConwaysGameOfLife;
using System.Threading;

namespace ConwayaGameOfLifeGUI
{
    // блокировка на уровне этого класса вызывает взаимоблокировку О_о
    class Game
    {
        bool imgFlag = false; // костыль :)
        bool[,] buff;

        System.Timers.Timer generationTimer;
        LifeGame lg;
        Thread gameThread;

        public void Random()
        {
            // размер из конфига
            System.Drawing.Size s = Config.Conf.worldSize;
            bool[,] cells = new bool[s.Width, s.Height];

            Random r = new Random();
            for (int i = 0; i < s.Height; i++)
            {
                for (int j = 0; j < s.Width; j++)
                {
                    cells[j, i] = (r.Next() & 0x01) == 1;
                }
            }

            Cells = new CellsImage(cells);
        }

        public Game()
        {
            System.Drawing.Size s = Config.Conf.worldSize;

            lg = new LifeGame(new bool[s.Width, s.Height]);

            gameThread = new Thread(gameThreadMain);
            gameThread.Start();

            buff = new bool[s.Width, s.Height];
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
                if (imgFlag) // убрать этот жуткий косыль (ну или переделать в фичу)
                {
                    upd();
                }

                CellsImage c = lg.NextGeneration();
                // Передача изображения обработчику события
                if (generationUpdate != null)
                {
                    generationUpdate(this, new CellsEventArgs(c));
                }

            }
        }

        public void upd()
        {
            lock (buff)
            {
                for (int i = 0; i < buff.GetLength(0); i++)
                {
                    for (int j = 0; j < buff.GetLength(1); j++)
                    {
                        if (buff[i, j]) lg.Cells[i, j] = !lg.Cells[i, j];
                        buff[i, j] = false;
                    }
                }
                imgFlag = false;
            }
        }

        public void Start()
        {
            //lg.Cells = Cells.Cells;
            lock (this)
            {
                generationTimer.Start();
            }
            
        }
        public void Stop()
        {
            lock (this)
            {
                generationTimer.Stop();
            }
        }

        public CellsImage Cells
        {
            get { return lg.Cells; }
            set
            {
                lock (this)
                {
                    lg.Cells = value;
                }
            }
        }
        public void Update(CellsImage cells)
        {
            lock (this)
            {
                lg = new LifeGame(cells);
            }
        }

        public void SetCell(int w, int h)
        {
            lock (buff)
            {
                imgFlag = true;
                buff[w, h] = !buff[w, h];
            }
        }

        public event EventHandler<CellsEventArgs> generationUpdate;
        public class CellsEventArgs : EventArgs
        {
            public CellsEventArgs(CellsImage c)
            {
                cells = c;
            }
            public CellsImage cells;
        }

        public bool TimerEnabled
        {
            get
            {
                return generationTimer.Enabled;
            }
        }
    }

    interface IGame
    {
         void Start();
        void Stop();

        event EventHandler<Game.CellsEventArgs> GenerationUpdate;

        void Random();

        // обновление игрогого поля (замена существующего поля на переданное в параметре)
        void Update(CellsImage cells /* interface for CellsImage ? */ );

        void SetCell(int w, int h);
    }
}

