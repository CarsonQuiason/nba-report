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
    public partial class MainWindow : Window
    {
        LiveGames lgs;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void addToList()
        {
            await Task.Run(() => lgs.getData());
            foreach (GameData g in lgs.gameList)
            {
                liveGamesList.Items.Add(g);
                liveGamesList.DisplayMemberPath = "Title";
            }
            if (liveGamesList.Items.Count == 0)
            {
                liveGamesList.Items.Add("There are no live games. Check back later.");
            }
         }

        private async void OnLoad(object sender, RoutedEventArgs e)
        {
            lgs = new LiveGames();
            addToList();
        }

        private void updateLiveGameInfo(object sender, SelectionChangedEventArgs e)
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
                quarter.Content = "Q" + g.Quarter;
                
            }
            catch
            {

            }
        }
        private void Refresh(object sender, RoutedEventArgs e)
        {
            lgs = new LiveGames();
            liveGamesList.Items.Clear();
            addToList();
        }


        //Navigation
        private void Search_Game(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new DatedGamesPage();
        }
        private void Search_Player(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new SearchPlayerPage();
        }

        private void Live_Game(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new LiveGamesPage();
        }
    }
}
