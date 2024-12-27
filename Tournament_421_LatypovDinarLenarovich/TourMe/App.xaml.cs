using System.Windows;
using TourMe.Components;

namespace TourMe
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ExamenEntities db = new ExamenEntities();
        public static UserLogin CurrentUser {  get; set; }
    }
}
