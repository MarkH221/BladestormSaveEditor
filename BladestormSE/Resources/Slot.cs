using System;
using System.Collections.Generic;

namespace BladestormSE.Resources
{
    public class Squad
    {
        public int Level;
        public int Points;
        public long Adjust;
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
        public Slot()
        {
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

        public List<Squad> Squads;
        public Squad Knives, Rapier, Swords, Spears, LongSpears, Horses, Halberds, Axes, Clubs, Bows, HorseBows, Camels, Elephants, Chariots, Explosives, Magic, Engineers;
        public int ID;

        public UInt32 Bowlv;

        public UInt32 Bowpoint;

        public UInt32 Camellv,
            Camelpoint;

        public UInt32 Chariotlv,
            Chariotpoint;

        public UInt32 Clublv;
        public UInt32 Clubpoint;

        public UInt32 Elephantlv,
            Elephantpoint;

        public UInt32 Explosivelv;

        public UInt32 Explosivepoint;
        public UInt32 Halberdlv;
        public UInt32 Halberdpoint;
        public UInt32 Horsebowlv;
        public UInt32 Horsebowpoint;

        public UInt32 Horseslv,
            Horsespoint;

        public UInt32 Magiclv,
            Magicpoint;

        public int Money;

        public string Name, SlotString;

        public bool Slotedited;

        public long StartingOffset;
    }
}