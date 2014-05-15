using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Isolib;
namespace BladestormSE
{
    
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}