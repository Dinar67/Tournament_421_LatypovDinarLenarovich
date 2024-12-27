using System.Windows;
using TourMe.Components;

namespace TourMe
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Tournament_421_LatypovDinarLenarovichEntities db = new Tournament_421_LatypovDinarLenarovichEntities();
        public static UserLogin CurrentUser {  get; set; }
    }
}
