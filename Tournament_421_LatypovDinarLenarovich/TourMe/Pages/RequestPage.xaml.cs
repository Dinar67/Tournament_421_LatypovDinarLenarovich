using System;
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
            TournirCb.ItemsSource = App.db.Tournament.Where(x => x.StateId == 1).ToList();
            TournirCb.SelectedItem = App.db.Tournament.First(x => x.Id == request.IdTournament);

            if (request.Id == 0)
                StatusSP.Visibility = System.Windows.Visibility.Collapsed;
            else
            {
                PlayerOfTeams = Request.Team.PlayerOfTeam.ToList();
                RequestBtn.Visibility = System.Windows.Visibility.Collapsed;
                MainPanel.IsEnabled = false;
                if(request.Team != null)
                    TeamNameTb.Text = request.Team.Name;
                if (request.RequestPlayer != null)
                    NickNameTb.Text = request.RequestPlayer.NickName;
                StatusTb.Text = request.GetStatus;
                if(request.isAccepted != null)
                {
                    NoBtn.Visibility = System.Windows.Visibility.Collapsed;
                    YesBtn.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
            Refresh();
            RefreshPage();
        }

        private void Refresh()
        {
            IEnumerable<PlayerOfTeam> players = PlayerOfTeams.ToList();
            MyList.ItemsSource = players;
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            PlayerOfTeams.Add(new PlayerOfTeam());
            Refresh();
        }

        private void RequestBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var tour = TournirCb.SelectedItem as Tournament;
            if (tour == null)
                return;

            if (tour.TypeId == 1)
            {
                if(NickNameTb.Text == string.Empty)
                {
                    Methods.TakeWarning("Заполните Никнейм!");
                    return;
                }

                var req = App.db.Request.Add(new Request()
                {
                    DateTimeRequest = DateTime.Now,
                    isAccepted = null,
                    IdTournament = tour.Id,
                    Status = true
                });
                App.db.RequestPlayer.Add(new RequestPlayer()
                {
                    NickName = NickNameTb.Text,
                    IdRequest = req.Id,
                    PlayerId = App.CurrentUser.Player.Id
                });
            }
            else if(tour.TypeId == 2)
            {
                if(PlayerOfTeams.Count != 5)
                {
                    Methods.TakeWarning("В команде должно быть 5 человек!");
                    return;
                }
                if (TeamNameTb.Text == string.Empty)
                {
                    Methods.TakeWarning("Заполните наименование команды!");
                    return;
                }
                if(PlayerOfTeams.Any(x => x.ContactInfo == string.Empty || x.TeamRole == null))
                {
                    Methods.TakeWarning("Заполните все данные команды!");
                    return;
                }

                var req = App.db.Request.Add(new Request()
                {
                    DateTimeRequest = DateTime.Now,
                    isAccepted = null,
                    IdTournament = tour.Id,
                    Status = true
                });
                var team = App.db.Team.Add(new Team()
                {
                    Id = req.Id,
                    Name = TeamNameTb.Text
                });
                foreach (var player in PlayerOfTeams)
                {
                    player.TeamId = team.Id;
                    App.db.PlayerOfTeam.Add(player);
                }
            }
            else
            {
                if (NickNameTb.Text == string.Empty)
                {
                    Methods.TakeWarning("Заполните Никнейм!");
                    return;
                }
                if (PlayerOfTeams.Count != 5)
                {
                    Methods.TakeWarning("В команде должно быть 5 человек!");
                    return;
                }
                if (TeamNameTb.Text == string.Empty)
                {
                    Methods.TakeWarning("Заполните наименование команды!");
                    return;
                }
                if (PlayerOfTeams.Any(x => x.ContactInfo == string.Empty || x.IdRole == 0))
                {
                    Methods.TakeWarning("Заполните все данные команды!");
                    return;
                }

                var req = App.db.Request.Add(new Request()
                {
                    DateTimeRequest = DateTime.Now,
                    isAccepted = null,
                    IdTournament = tour.Id,
                    Status = true
                });
                App.db.RequestPlayer.Add(new RequestPlayer()
                {
                    NickName = NickNameTb.Text,
                    IdRequest = req.Id,
                    PlayerId = App.CurrentUser.Player.Id
                });
                var team = App.db.Team.Add(new Team()
                {
                    Id = req.Id,
                    Name = TeamNameTb.Text
                });
                foreach (var player in PlayerOfTeams)
                {
                    player.TeamId = team.Id;
                    App.db.PlayerOfTeam.Add(player);
                }
            }

            App.db.SaveChanges();
            Methods.TakeInformation("Вы успшно подали заявку на участие!");
            Navigations.Next(new TournirsPage());
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
            if (TournirCb.SelectedIndex == -1)
            {
                ComandStack.Visibility = System.Windows.Visibility.Collapsed;
                NickNameSP.Visibility = System.Windows.Visibility.Collapsed;
                NameCommandSP.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if ((TournirCb.SelectedItem as Tournament).TypeId == 1)
            {
                ComandStack.Visibility = System.Windows.Visibility.Collapsed;
                NickNameSP.Visibility = System.Windows.Visibility.Visible;
                NameCommandSP.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if ((TournirCb.SelectedItem as Tournament).TypeId == 2)
            {
                ComandStack.Visibility = System.Windows.Visibility.Visible;
                NickNameSP.Visibility = System.Windows.Visibility.Collapsed;
                NameCommandSP.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                ComandStack.Visibility = System.Windows.Visibility.Visible;
                NickNameSP.Visibility = System.Windows.Visibility.Visible;
                NameCommandSP.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void BackBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigations.Back();
        }

        private void NoBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Request.isAccepted = false;
            App.db.SaveChanges();
            Navigations.Next(new TournirsPage());
        }

        private void YesBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Request.isAccepted = true;
            App.db.SaveChanges();
            Navigations.Next(new TournirsPage());
        }
    }
}
