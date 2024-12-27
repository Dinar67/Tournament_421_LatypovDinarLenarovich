using System.Windows.Controls;

namespace TourMe
{
    public static class Navigations
    {
        public static Frame MyFrame { get; set; }
        public static void Next(Page page)
        {
            MyFrame?.Navigate(page);
        }
        public static void Back()
        {
            if(MyFrame?.CanGoBack == true)
                MyFrame?.GoBack();
        }
    }
}
