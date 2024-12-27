using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourMe.Components;

namespace TourMe.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddTournirePage.xaml
    /// </summary>
    public partial class AddTournirePage : Page
    {
        private Tournament Tournament { get; set; }
        public AddTournirePage(Tournament tournament)
        {
            InitializeComponent();
            Tournament = tournament;

            TypeCb.ItemsSource = App.db.Type.ToList();
            StateCb.ItemsSource = App.db.State.ToList();
            GameCb.ItemsSource = App.db.Game.ToList();

            if (tournament.Id != 0)
            {
                HoursTb.Text = tournament.DatetimeTournament.TimeOfDay.Hours.ToString();
                MinutesTb.Text = tournament.DatetimeTournament.TimeOfDay.Minutes.ToString();
                DateTb.SelectedDate = tournament.DatetimeTournament.Date;
            }
            if (App.CurrentUser != null)
            {
                RequestBtn.Visibility = App.CurrentUser.GetPlayerVisibility;
                CreateBtn.Visibility = App.CurrentUser.GetAdministratorVisibility;
                MainStack.IsEnabled = App.CurrentUser.GetAdministratorVisibility == Visibility.Visible;
            }
            else
            {
                RequestBtn.Visibility = Visibility.Collapsed;
                CreateBtn.Visibility = Visibility.Collapsed;
                MainStack.IsEnabled = false;
            }
            

            DataContext = Tournament;
        }

        private void MinRatingTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void CountPlayerTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void PrizePoolTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigations.Back();
        }
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTb.Text))
            {
                Methods.TakeWarning("Пожалуйста, введите название турнира.");
                return;
            }

            if (TypeCb.SelectedItem == null)
            {
                Methods.TakeWarning("Пожалуйста, выберите тип турнира.");
                return;
            }

            if (string.IsNullOrWhiteSpace(HoursTb.Text) || string.IsNullOrWhiteSpace(MinutesTb.Text) || DateTb.SelectedDate == null)
            {
                Methods.TakeWarning("Пожалуйста, введите корректную дату и время.");
                return;
            }

            if (string.IsNullOrWhiteSpace(PrizePoolTb.Text))
            {
                Methods.TakeWarning("Пожалуйста, введите призовой фонд.");
                return;
            }

            if (StateCb.SelectedItem == null)
            {
                Methods.TakeWarning("Пожалуйста, выберите статус турнира.");
                return;
            }

            if (GameCb.SelectedItem == null)
            {
                Methods.TakeWarning("Пожалуйста, выберите игру.");
                return;
            }

            if (string.IsNullOrWhiteSpace(MinRatingTb.Text))
            {
                Methods.TakeWarning("Пожалуйста, введите минимальный рейтинг.");
                return;
            }

            if (string.IsNullOrWhiteSpace(DescriptionTb.Text))
            {
                Methods.TakeWarning("Пожалуйста, введите описание турнира.");
                return;
            }

            if (string.IsNullOrWhiteSpace(CountPlayerTb.Text))
            {
                Methods.TakeWarning("Пожалуйста, введите количество игроков.");
                return;
            }

            if(int.Parse(HoursTb.Text) > 23 || int.Parse(MinutesTb.Text) > 59)
            {
                Methods.TakeWarning("Пожалуйста, введите корректную дату и время.");
                return;
            }

            Tournament.DatetimeTournament = DateTb.SelectedDate.Value.AddHours(int.Parse(HoursTb.Text)).AddMinutes(int.Parse(MinutesTb.Text));

            if(Tournament.Id == 0)
                App.db.Tournament.Add(Tournament);

            App.db.SaveChanges();
            Navigations.Next(new TournirsPage());
            Methods.TakeInformation("Успешно сохранено!");
        }

        private void RequestBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigations.Next(new RequestPage(new Request() { IdTournament = Tournament.Id}));
        }
    }
}
