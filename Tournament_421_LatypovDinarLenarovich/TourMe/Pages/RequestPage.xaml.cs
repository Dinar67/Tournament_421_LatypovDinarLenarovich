using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using TourMe.Components;

namespace TourMe.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestPage.xaml
    /// </summary>
    public partial class RequestPage : Page
    {
        public Request Request { get; set; }
        public List<PlayerOfTeam> PlayerOfTeams { get; set; }
        public RequestPage(Request request)
        {
            InitializeComponent();
            Request = request;
            PlayerOfTeams = new List<PlayerOfTeam>();

            if(request.Id == 0)
            {
                StatusSP.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                TournirCb.SelectedItem = request.Tournament;
                PlayerOfTeams = Request.Team.PlayerOfTeam.ToList();
            }
            Refresh();
        }

        private void Refresh()
        {
            IEnumerable<PlayerOfTeam> players = PlayerOfTeams;
            MyList.ItemsSource = players;
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PlayerOfTeams.Add(new PlayerOfTeam());
            Refresh();
        }

        private void RequestBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MyList.SelectedItem == null)
                return;

            PlayerOfTeams.Remove(MyList.SelectedItem as PlayerOfTeam);
            Refresh();
        }

        private void TournirCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshPage();
        }

        private void RefreshPage()
        {
            if(TournirCb.SelectedIndex == -1)
            {
                ComandStack.Visibility = System.Windows.Visibility.Collapsed;
                NickNameSP.Visibility = System.Windows.Visibility.Collapsed;
                NameCommandSP.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if((TournirCb.SelectedItem as Tournament).TypeId == 1)
            {
                 
            }
            else if ((TournirCb.SelectedItem as Tournament).TypeId == 2)
            {

            }
            else
            {

            }
        }
    }
}
