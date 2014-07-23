using System;
using System.Collections.Generic;

namespace BladestormSE.Resources
{
    public class Squad
    {
        public long Adjust;
        public UInt16 Level;
        public UInt32 Points;
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

    public class Slot
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

        public int SlotID;
        public Squad Knives;
        public Squad LongSpears;
        public Squad Magic;

        public UInt32 Money;

        public string Name;
        public Squad Rapier;
        public string SlotString;

        public bool SlotRead;
        public Squad Spears;
        public List<Squad> Squads;

        public long StartingOffset;
        public Squad Swords;

        public Slot()
        {
            Squads = new List<Squad>();
            Squads.Add(Knives = new Squad { Adjust = 842 });
            Squads.Add(Rapier = new Squad { Adjust = 926 });
            Squads.Add(Swords = new Squad { Adjust = 1010 });
            Squads.Add(Spears = new Squad { Adjust = 1094 });
            Squads.Add(LongSpears = new Squad { Adjust = 1178 });
            Squads.Add(Horses = new Squad { Adjust = 1262 });
            Squads.Add(Halberds = new Squad { Adjust = 1346 });
            Squads.Add(Axes = new Squad { Adjust = 1430 });
            Squads.Add(Clubs = new Squad { Adjust = 1514 });
            Squads.Add(Bows = new Squad { Adjust = 1598 });
            Squads.Add(HorseBows = new Squad { Adjust = 1682 });
            Squads.Add(Camels = new Squad { Adjust = 1766 });
            Squads.Add(Elephants = new Squad { Adjust = 1850 });
            Squads.Add(Chariots = new Squad { Adjust = 1934 });
            Squads.Add(Explosives = new Squad { Adjust = 2018 });
            Squads.Add(Magic = new Squad { Adjust = 2102 });
            Squads.Add(Engineers = new Squad { Adjust = 2186 });
        }
    }
}