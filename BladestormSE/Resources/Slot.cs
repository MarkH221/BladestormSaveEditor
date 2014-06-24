using System;

namespace BladestormSE.Resources
{
    public class Squad : Utilities.NotificationObject
    {
        private int level, points;
        public int Level
        {
            get { return level; }
            set { level = value; RaisePropertyChanged(() => Level); }
        }
        public int Points
        {
            get { return points; }
            set { points = value; RaisePropertyChanged(() => Points); }
        }
        public long Adjust;
    }

    public class Slot : Utilities.NotificationObject 
    {
        public bool Used;
        public Squad Knives = new Squad();
        public Squad Rapier = new Squad();
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

        public int? Knivelv;

        public int? Knivepoint;

        public UInt32 Longspearlv,
            Longspearpoint;

        public UInt32 Magiclv,
            Magicpoint;

        private int money;
        public int Money
        {
            get { return money; }
            set
            {
                money = value;
                RaisePropertyChanged(() => Money);
            }
        }

        public string Name, SlotString;

        public UInt32 Rapierlv,
            Rapierpoint;

        public bool Slotedited;

        public UInt32 Spearlv,
            Spearpoint;

        public long StartingOffset;

        public UInt32 Swordlv,
            Swordpoint;
       

    }
}