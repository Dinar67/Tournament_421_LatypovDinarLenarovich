using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TourMe.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            App.CurrentUser = null;
            InitializeComponent();
        }
        private void RegLink_Click(object sender, RoutedEventArgs e)
        {
            Navigations.Next(new RegPage());
        }
        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = App.db.UserLogin.FirstOrDefault(x => 
                x.Login.Equals(LoginTb.Text) &&
                x.Password.Equals(PasswordPb.Password));
            if(user == null)
            {
                Methods.TakeWarning("Неверный логин или пароль!");
                return;
            }

            App.CurrentUser = user;
            Navigations.Next(new TournirsPage());
            Methods.TakeInformation("Вы успешно вошли в систему!");
        }
        private void TournirsBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigations.Next(new TournirsPage());
        }
    }
}
