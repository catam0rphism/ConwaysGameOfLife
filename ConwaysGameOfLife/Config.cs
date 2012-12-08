using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace ConwaysGameOfLife
{

    public class Config
    {
        #region Каркас класса
        private static object lockFlag = new object();
        private static Config _conf;

        [XmlIgnore]
        public static Config Conf
        {
            get
            {
                lock (lockFlag)
                {
                    if (_conf == null)
                    {
                        try
                        {
                            using (FileStream fs = new FileStream("Config.Xml", FileMode.Open))
                            {
                                XmlSerializer xs = new XmlSerializer(typeof(Config));
                                _conf = (Config)xs.Deserialize(fs);
                            }
                        }
                        catch (Exception)
                        {
                            _conf = new Config();
                        }
                    }
                }
                return _conf;
            }
        }

        public static void Reload()
        {
            _conf = null;
        }
        public void Save()
        {
            using (FileStream fs = new FileStream("Config.Xml", FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                xs.Serialize(fs, _conf);
            }
        }

        public static Config GetClone()
        {
            return (Config)Conf.MemberwiseClone();
        }
        public static void NewConfig(Config conf)
        {
            _conf = conf;
        }
        #endregion

        // TODO: Приличный вид в окне свойств

        public Size worldSize
        {
            get { return _worldSize; }
            set { _worldSize = value; }
        }
        private Size _worldSize = new System.Drawing.Size(40, 40);

        [XmlIgnore]
        public Color AliveColor
        {
            get { return Color.FromArgb(iAlive); }
            set { iAlive = value.ToArgb(); }
        }
        public int iAlive = Color.Black.ToArgb();

        [XmlIgnore]
        public Color DeadColor
        {
            get { return Color.FromArgb(iDead); }
            set { iDead = value.ToArgb(); }
        }
        public int iDead = Color.White.ToArgb();

        public int timerInterval
        {
            get { return timerinterval; }
            set { timerinterval = value; }
        }
        private int timerinterval = 70;

        public int PixToCell
        {
            get { return pixtocell; }
            set { pixtocell = value; }
        }
        private int pixtocell = 10;
    }
}

