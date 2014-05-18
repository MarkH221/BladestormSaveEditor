﻿using System;
using System.Windows;
using System.Windows.Controls;

namespace BladestormSE
{
    /// <summary>
    /// Interaction logic for SquadControl.xaml
    /// </summary>
    public partial class SquadControl : UserControl
    {
        public int Points { get; set; }

        public SquadControl()
        {
            InitializeComponent();
        }

        public int Level
        {
            get { return (int)Levelbox.Value; }
            set { Levelbox.Dispatcher.Invoke(new Action(delegate { Levelbox.Value = value; })); }
        }

        public string Squad
        {
            get { return SquadName.Content as string; }
            set
            {
                SquadName.Dispatcher.Invoke(new Action(delegate
                                                       {
                                                           SquadName.Content = value;
                                                       })
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