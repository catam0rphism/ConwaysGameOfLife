using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ConwaysGameOfLife;

namespace ConwayaGameOfLifeGUI
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        Config cf = Config.Conf;
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = Config.GetClone();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Config.NewConfig((Config)propertyGrid.SelectedObject);
            cf.Save();
            applySetting(new EventArgs());
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void applySetting(EventArgs e)
        {
            if (OnApplySetting != null)
                OnApplySetting(this, e);
        }
        public event EventHandler<EventArgs> OnApplySetting;

        private void button1_Click(object sender, EventArgs e)
        {
            Config.Reload();
        }

    }
}
