using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NBAReport
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameDate gd;
        LiveGame lg;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ChangeCalendarVisibility(object sender, RoutedEventArgs e)
        {
            gameDate.Visibility = Visibility.Visible;
            backBtn.Visibility = Visibility.Hidden;
            gameDateList.Visibility = Visibility.Hidden;
        }

        private async void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            gameDate.Visibility = Visibility.Hidden;
            backBtn.Visibility = Visibility.Visible;
            gameDateList.Visibility = Visibility.Visible;
            DateTime DTdate = (DateTime)gameDate.SelectedDate;
            string date = DTdate.ToString("yyyy-MM-dd");
            gd = new GameDate(date);
            gameDateTitle.Content = "Games on " + gd.Date;
            Console.Write(date);
        }

    private async void OnLoad(object sender, RoutedEventArgs e)
        {
            lg = new LiveGame();
            await lg.getData();

            if (!lg.containsGames)
            {
                ListBoxItem lbi = new ListBoxItem();
                lbi.Content = "There are no live games. Check back later.";
                liveGameList.Items.Add(lbi);
            }
        }
	}
}
