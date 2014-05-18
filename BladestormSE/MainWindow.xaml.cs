using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Isolib.STFSPackage;

namespace BladestormSE
{
    public partial class MainWindow
    {
        #region Variables

        private string filepath;
        private Stfs _stfs;
        private byte[] buffer;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
        }

        
        #region Input

        #endregion Input

        #region Output

        #endregion Output

        #region Utilities

        private void MaxLevelBtnClick(object sender, RoutedEventArgs e)
        {
            //Task.Factory.StartNew(() =>
                                  {
                                      foreach (SquadControl squad in (Squads1.Children.OfType<SquadControl>()))
                                      {
                                          squad.MaxLevel(null, null);
                                      }
                                      foreach (SquadControl squad in (Squads2.Children.OfType<SquadControl>()))
                                      {
                                          squad.MaxLevel(null, null);
                                      }
                                      foreach (SquadControl squad in (Squads3.Children.OfType<SquadControl>()))
                                      {
                                          squad.MaxLevel(null, null);
                                      }
                                  }
            //);

        }
private void MaxPointsBtnClick(object sender, RoutedEventArgs e)
        {
            //Task.Factory.StartNew(() =>
            {
                foreach (SquadControl squad in (Squads1.Children.OfType<SquadControl>()))
                {
                    squad.MaxPoints(null, null);
                }
                foreach (SquadControl squad in (Squads2.Children.OfType<SquadControl>()))
                {
                    squad.MaxPoints(null, null);
                }
                foreach (SquadControl squad in (Squads3.Children.OfType<SquadControl>()))
                {
                    squad.MaxPoints(null, null);
                }
            }
    //);

        }
        private void MaxMoneyClick(object sender, RoutedEventArgs e)
        {
        }

        
private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion Utilities
    }
}