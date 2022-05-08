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
        DatedGames dgs;
        LiveGames lgs;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void ChangeCalendarVisibility(object sender, RoutedEventArgs e)
        {
            datedGamesCalendar.Visibility = Visibility.Visible;
            backBtn.Visibility = Visibility.Hidden;
            datedGamesList.Visibility = Visibility.Hidden;
        }

        private async void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            datedGamesCalendar.Visibility = Visibility.Hidden;
            backBtn.Visibility = Visibility.Visible;
            datedGamesList.Visibility = Visibility.Visible;
            DateTime DTdate = (DateTime)datedGamesCalendar.SelectedDate;
            string date = DTdate.ToString("yyyy-MM-dd");
            dgs = new DatedGames(date);
            datedGamesTitle.Content = "Games on " + dgs.Date;
            addToList(false);
        }

        private async void addToList(bool isLive)
        {
            if (isLive)
            {

            }
            else
            {
                await Task.Run(() => dgs.getData());
                foreach (GameData g in dgs.gameList)
                {
                    datedGamesList.Items.Add(g);
                    datedGamesList.DisplayMemberPath = "Title";
                }
            }
        }

        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            lgs = new LiveGames();
            //dgs = new DatedGames("2021-02-12");
            //await dgs.getData();
            //await lg.getData();

            //if// (!lgs.containsGames)
            //{
                //ListBoxItem lbi = new ListBoxItem();
                //lbi.Content = "There are no live games. Check back later.";
                //liveGameList.Items.Add(lbi);
            //}
        }

        private void updateGameInfo(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                GameData g = ((sender as ListBox).SelectedItem as GameData);
                nameOne.Content = g.HomeName + " (HOME)";
                nameTwo.Content = g.AwayName + " (AWAY)";
                scoreOne.Content = g.HomeScore.ToString();
                scoreTwo.Content = g.AwayScore.ToString();
                logoOne.Source = new BitmapImage(new Uri(g.HomeLogo, UriKind.RelativeOrAbsolute));
                logoTwo.Source = new BitmapImage(new Uri(g.AwayLogo, UriKind.RelativeOrAbsolute));
                arenaName.Content = "@ " + g.ArenaName;
            }
            catch
            {

            }
        }
	}
}
