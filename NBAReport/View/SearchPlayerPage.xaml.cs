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
    /// <summary>
    /// Interaction logic for SearchPlayerPage.xaml
    /// </summary>
    public partial class SearchPlayerPage : Page
    {
        SearchPlayer sp;
        public SearchPlayerPage()
        {
            InitializeComponent();
        }

        //
        private async void SearchForPlayers(object sender, RoutedEventArgs e)
        {
            playerList.Items.Clear();
            sp = new SearchPlayer(nameTextBox.Text);
            addToList();
        }

        //Adds players to listbox based on the 
        private async void addToList()
        {
            await Task.Run(() => sp.getData());
            foreach (PlayerData p in sp.playerList)
            {
                playerList.Items.Add(p);
                playerList.DisplayMemberPath = "FullName";
            }
            if (playerList.Items.Count == 0)
            {
                playerList.Items.Add("There are no players with the name " + sp.Name);
            }
        }

        //Displays stats of a player based on selected ListBox item
        private void displayStats(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PlayerData p = ((sender as ListBox).SelectedItem as PlayerData);
                playerName.Content = p.FullName;
                playerHeight.Text = p.HeightFeet.ToString() + "'" + p.HeightInch.ToString() + "\"";
                playerWeight.Text = p.Ibs + "Ibs";
                playerPos.Text = p.Position;
                playerNum.Text = p.JerseyNum.ToString();
                playerBirth.Text = p.Birth;
            }
            catch
            {

            }
        }
    }
}
