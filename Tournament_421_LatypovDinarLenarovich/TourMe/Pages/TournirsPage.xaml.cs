using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using TourMe.Components;

namespace TourMe.Pages
{
    /// <summary>
    /// Логика взаимодействия для TournirsPage.xaml
    /// </summary>
    public partial class TournirsPage : Page
    {
        public TournirsPage()
        {
            InitializeComponent();
            if (App.CurrentUser == null)
            {
                ExitBtn.Visibility = System.Windows.Visibility.Collapsed;
                AddBtn.Visibility = System.Windows.Visibility.Collapsed;
                EditBtn.Visibility = System.Windows.Visibility.Collapsed;
                DeleteBtn.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                ExitBtn.Visibility = System.Windows.Visibility.Visible;
                EnterBtn.Visibility = System.Windows.Visibility.Collapsed;

                AddBtn.Visibility = App.CurrentUser.GetAdministratorVisibility;
                EditBtn.Visibility = App.CurrentUser.GetAdministratorVisibility;
                DeleteBtn.Visibility = App.CurrentUser.GetAdministratorVisibility;
            }

            

            FilterCb.SelectedIndex = 0;
        }
        public void Refresh()
        {
            IEnumerable<Tournament> tournaments = App.db.Tournament;

            //Фильтр
            if(FilterCb.SelectedIndex > 0)
            {
                if (FilterCb.SelectedIndex == 1)
                    tournaments = tournaments.Where(x => x.DatetimeTournament >= DateTime.Now && x.State.Id != 6);
                else if(FilterCb.SelectedIndex == 2)
                    tournaments = tournaments.Where(x => x.DatetimeTournament < DateTime.Now);
                else
                    tournaments = tournaments.Where(x => x.DatetimeTournament >= DateTime.Now && x.State.Id == 6);
            }

            //Поиск
            if(SearchTb.Text != string.Empty)
                tournaments = tournaments.Where((x) => x.Title.ToLower().Contains(SearchTb.Text.ToLower()));

            MyList.ItemsSource = tournaments.ToList();
        }
        private void EnterBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigations.Next(new AuthPage());
        }
        private void FilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
        private void ExitBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.CurrentUser = null;
            Navigations.Next(new TournirsPage());
            Methods.TakeInformation("Вы вышли из аккаунта!");
        }

        private void AddBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Navigations.Next(new AddTournirePage(new Tournament()));
        }

        private void EditBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(MyList.SelectedItem != null)
                Navigations.Next(new AddTournirePage(MyList.SelectedItem as Tournament));
        }

        private void DeleteBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (MyList.SelectedItem == null)
                return;

            if (!Methods.TakeChoice("Вы точно хотите удалить турнир?"))
                return;
            App.db.Tournament.Remove(MyList.SelectedItem as Tournament);
            App.db.SaveChanges();
            Refresh();
        }
    }
}
