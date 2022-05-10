using System;
using System.Collections.Generic;
using System.Linq;
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
    
    public partial class DatedGamesPage : Page
    {
        DatedGames dgs;
        public DatedGamesPage()
        {
            InitializeComponent();
        }

        //Adds Games based on Date to ListBox
        private async void addToList()
        {
            await Task.Run(() => dgs.getData());
            foreach (GameData g in dgs.gameList)
            {
                datedGamesList.Items.Add(g);
                datedGamesList.DisplayMemberPath = "Title";
            }
            if (datedGamesList.Items.Count == 0)
            {
                datedGamesList.Items.Add("There are no live games. Check back later.");
            }
        }

        public void ChangeCalendarVisibility(object sender, RoutedEventArgs e)
        {
            datedGamesCalendar.Visibility = Visibility.Visible;
            backBtn.Visibility = Visibility.Hidden;
            datedGamesList.Visibility = Visibility.Hidden;
        }

        private async void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            datedGamesGrid.Visibility = Visibility.Visible;
            searchGrid.Visibility = Visibility.Hidden;
            DateTime DTdate = (DateTime)datedGamesCalendar.SelectedDate;
            string date = DTdate.ToString("yyyy-MM-dd");
            dgs = new DatedGames(date);
            datedGamesTitle.Content = "Games on " + dgs.Date;
            addToList();
        }

        //Updates view based on data from listbox selection
        private void updateDateGameInfo(object sender, SelectionChangedEventArgs e)
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
            catch (NullReferenceException)
            {
                nameOne.Content = "NONE (HOME)";
                nameTwo.Content = "NONE (AWAY)";
                scoreOne.Content = "0";
                scoreTwo.Content = "0";
                logoOne.Source = new BitmapImage(new Uri(@"../Images/loadIcon.png", UriKind.Relative));
                logoTwo.Source = new BitmapImage(new Uri(@"../Images/loadIcon.png", UriKind.Relative));
                arenaName.Content = "@ NOWHERE";
            }
        }
        private void Return_To_Search(object sender, RoutedEventArgs e)
        {
            datedGamesGrid.Visibility = Visibility.Hidden;
            searchGrid.Visibility = Visibility.Visible;
            datedGamesList.Items.Clear();
        }
    }
}
