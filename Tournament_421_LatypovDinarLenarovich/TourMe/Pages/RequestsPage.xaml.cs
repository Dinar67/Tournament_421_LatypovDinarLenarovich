using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TourMe.Components;

namespace TourMe.Pages
{
    /// <summary>
    /// Логика взаимодействия для RequestsPage.xaml
    /// </summary>
    public partial class RequestsPage : Page
    {
        private int maxIndex;
        public RequestsPage()
        {
            InitializeComponent();
            List<Tournament> tournaments = App.db.Tournament.ToList();
            tournaments = tournaments.Concat(new List<Tournament>() { new Tournament() { Title = "Все"} }).ToList();
            FilterCb.ItemsSource = tournaments;
            maxIndex = tournaments.Count - 1;
            FilterCb.SelectedIndex = maxIndex;
        }
        public void Refresh()
        {
            IEnumerable<Request> req = App.db.Request;

            //Фильтр
            if (FilterCb.SelectedIndex != -1 && FilterCb.SelectedIndex != maxIndex)
                req = req.Where(x => x.IdTournament == (FilterCb.SelectedItem as Tournament).Id);

            //Поиск
            if (SearchTb.Text != string.Empty)
                req = req.Where((x) => x.Tournament.Title.ToLower().Contains(SearchTb.Text.ToLower()));

            MyList.ItemsSource = req.ToList();
        }

        private void SeeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MyList.SelectedItem != null)
                Navigations.Next(new RequestPage(MyList.SelectedItem as Request));
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigations.Back();
        }

        private void FilterCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
