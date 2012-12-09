using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using ConwaysGameOfLife;
using System.IO;

namespace ConwayaGameOfLifeGUI
{
    public partial class GameForm : Form
    {
        Game lg = new Game();
        public GameForm()
        {
            InitializeComponent();
            resize();
        }

        const int btnHeigth = 77;
        void resize()
        {
            Size s = Config.Conf.worldSize;
            pictureBox.Width = s.Width * Config.Conf.PixToCell;
            pictureBox.Height = s.Height * Config.Conf.PixToCell;
            panel.Width = s.Width * Config.Conf.PixToCell;
            panel.Height = s.Height * Config.Conf.PixToCell;

            lg.generationUpdate += lg_generationUpdate;
        }

        void setImage(Bitmap img)
        {
            lg.Stop();

            pictureBox.Invoke(
                    new voiddel(
                        delegate
                        {
                            pictureBox.Image = img;
                            pictureBox.Refresh();
                        }));
            lg.Start();
            
        }

        void lg_generationUpdate(object sender, Game.CellsEventArgs e)
        {
            setImage(e.cells.Image);
            // УСТАНОВКА ИЗОБРАЖЕНИЯ ИЗ e.cells
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            setImage(lg.Cells.Image);
        }

        delegate void voiddel();

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm cf = new ConfigForm();
            cf.OnApplySetting += cf_OnApplySetting;

            cf.ShowDialog();
        }

        void cf_OnApplySetting(object sender, EventArgs e)
        {
            // проверка размера (обнуление Cells при несоответствии)
            if (!lg.Cells.IsValid)
            {
                lg.Update(new CellsImage());
            }

            // обновление свойств элементов управления
            resize();
            lg.Refresf();

            // замена изображения
            setImage(lg.Cells.Image);
        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lg.Random();
            setImage(lg.Cells.Image);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lg.Start();
        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lg.Stop();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lg.Stop(); // подстраховка на случай исключений
            this.Close();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("В разработке");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("В разработке");
        }

        private void cleanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lg.Stop();
            lg.Update(new CellsImage());
            setImage(lg.Cells.Image);
        }
    }
}
