using System.Windows;
using BladestormSE.Resources;

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
            Levelbox.DataContext = squad.Level;
            PointBox.DataContext = squad.Points;
        }

        public SquadControl()
        {
            InitializeComponent();
        }

        public int Points { get; set; }

        public int Level
        {
            get { return (int) Levelbox.Value; }
            set { Levelbox.Dispatcher.Invoke(delegate { Levelbox.Value = value; }); }
        }

        public string Squad
        {
            get { return SquadName.Content as string; }
            set
            {
                SquadName.Dispatcher.Invoke(delegate { SquadName.Content = value; }
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