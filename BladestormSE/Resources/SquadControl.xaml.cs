using BladestormSE.Resources;
using System;
using System.Windows;

namespace BladestormSE
{
    /// <summary>
    ///     Interaction logic for SquadControl.xaml
    /// </summary>
    public partial class SquadControl
    {
        public SquadControl(Squad squad)
        {
            InitializeComponent();
        }

        public SquadControl()
        {
            InitializeComponent();
        }

        public UInt32 Points
        {
            get { return (uint)PointBox.Value; }
            set
            {
                PointBox.Dispatcher.Invoke(new Action(delegate
                                                 {
                                                     PointBox.Value =
                                                         value;
                                                 }));
            }
        }

        public UInt16 Level
        {
            get { return (ushort)Levelbox.Value; }
            set { Levelbox.Dispatcher.Invoke(new Action(delegate { Levelbox.Value = (short?)value; })); }
        }

        public string Squad
        {
            get { return SquadName.Content as string; }
            set
            {
                SquadName.Dispatcher.Invoke(new Action(delegate { SquadName.Content = value; })
                    );
            }
        }

        public void MaxLevel(object sender, RoutedEventArgs e)
        {
            Levelbox.Value = 99;
        }

        public void MaxPoints(object sender, RoutedEventArgs e)
        {
            PointBox.Value = 99999;
        }
    }
}