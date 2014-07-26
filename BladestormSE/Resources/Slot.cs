using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BladestormSE.Resources
{
    public class Squad : INotifyPropertyChanged
    {
        public long Adjust;

        private UInt16 _level;

        private UInt32 _points;

        public UInt16 Level
        {
            get { return _level; }
            set
            {
                _level = value;
                OnPropertyChanged("Level");
            }
        }

        public UInt32 Points
        {
            get { return _points; }
            set
            {
                _points = value;
                OnPropertyChanged("Points");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    public enum Squaddies
    {
        Knives,
        Rapier,
        Swords,
        Spears,
        LongSpears,
        Horses,
        Halberds,
        Axes,
        Clubs,
        Bows,
        HorseBows,
        Camels,
        Elephants,
        Chariots,
        Explosives,
        Magic,
        Engineers
    }

    public class Slot : INotifyPropertyChanged
    {
        public Squad Axes;
        public Squad Bows;
        public Squad Camels;

        public Squad Chariots;

        public Squad Clubs;

        public Squad Elephants;
        public Squad Engineers;

        public Squad Explosives;

        public Squad Halberds;
        public Squad HorseBows;
        public Squad Horses;

        public Squad Knives;
        public Squad LongSpears;
        public Squad Magic;

        public Squad Rapier;
        public int SlotID;

        public bool SlotRead;
        public string SlotString;
        public Squad Spears;
        public List<Squad> Squads;

        public long StartingOffset;
        public Squad Swords;
        private UInt32 _money;
        private string _name;

        public Slot()
        {
            Squads = new List<Squad>();
            Squads.Add(Knives = new Squad {Adjust = 842});
            Squads.Add(Rapier = new Squad {Adjust = 926});
            Squads.Add(Swords = new Squad {Adjust = 1010});
            Squads.Add(Spears = new Squad {Adjust = 1094});
            Squads.Add(LongSpears = new Squad {Adjust = 1178});
            Squads.Add(Horses = new Squad {Adjust = 1262});
            Squads.Add(Halberds = new Squad {Adjust = 1346});
            Squads.Add(Axes = new Squad {Adjust = 1430});
            Squads.Add(Clubs = new Squad {Adjust = 1514});
            Squads.Add(Bows = new Squad {Adjust = 1598});
            Squads.Add(HorseBows = new Squad {Adjust = 1682});
            Squads.Add(Camels = new Squad {Adjust = 1766});
            Squads.Add(Elephants = new Squad {Adjust = 1850});
            Squads.Add(Chariots = new Squad {Adjust = 1934});
            Squads.Add(Explosives = new Squad {Adjust = 2018});
            Squads.Add(Magic = new Squad {Adjust = 2102});
            Squads.Add(Engineers = new Squad {Adjust = 2186});
        }

        public UInt32 Money
        {
            get { return _money; }
            set
            {
                _money = value;
                OnPropertyChanged("Money");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value.Length > 16 ? value.Substring(0, 16) : value;
                OnPropertyChanged("Name");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}