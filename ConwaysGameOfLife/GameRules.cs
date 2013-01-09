using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConwaysGameOfLife
{
    public class GameRules
    {
        public static GameRules Parse(string Rules)
        {
            List<int> Alive = new List<int>(10);
            List<int> Dead = new List<int>(10);
            // parse "B1357/S1357"
            string[] starr = Rules.Split('/', 'B', 'S');
            foreach (char c in starr[3])
            {
                Alive.Add(int.Parse(c.ToString()));
            }

            foreach (char c in starr[1])
            {
                Dead.Add(int.Parse(c.ToString()));
            }
            return new GameRules(Alive.ToArray(), Dead.ToArray());
        }
        public GameRules(int[] Alive, int[] Dead)
        {
            this.Dead = Dead;
            this.Alive = Alive;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(16);
            sb.Append("B");
            foreach (int i in Dead)
            {
                sb.Append(i);
            }
            sb.Append(@"/"+"S");
            foreach (int i in Alive)
            {
                sb.Append(i);
            }


            return sb.ToString();
        }
        public string Code
        {
            get
            {
                return ToString();
            }
            set
            {
                GameRules gr = GameRules.Parse(value); // что я сделал О_о
                this.Alive = gr.Alive;
                this.Dead = gr.Dead;
            }
        }
        private int[] Alive;
        private int[] Dead;

        public bool CellState(int LifeCount, bool CellState)
        {
            return CellState ? CellStateIfAlive(LifeCount, Alive) : CellStateIfDead(LifeCount, Dead);
        }

        static bool CellStateIfAlive(int lc, params int[] rules)
        {
            return rules.Contains(lc);
        }
        static bool CellStateIfDead(int lc, params int[] rules)
        {
            return rules.Contains(lc);
        }

        //TODO: Const's

        public static readonly GameRules Conways = GameRules.Parse("B3/S23");
        public static readonly GameRules Replicator = GameRules.Parse("B1357/S1357");
        public static readonly GameRules Seeds = GameRules.Parse("B2/S");
        public static readonly GameRules r1 = GameRules.Parse("B25/S4");
    }
}
