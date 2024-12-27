using System.Windows;
using TourMe.Pages;

namespace TourMe
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigations.MyFrame = MyFrame;
            Navigations.Next(new TournirsPage());
        }
    }
}
