using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TourMe
{
    public static class Methods
    {
        public static void TakeWarning(string message)
        {
            MessageBox.Show(message, "Предупреждение!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        public static bool TakeChoice(string message)
        {
            var result = MessageBox.Show(message, "Подтверждение!", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            return result == MessageBoxResult.Yes;
        }
        public static void TakeInformation(string message)
        {
            MessageBox.Show(message, "Информация!", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static BitmapImage GetImageFromBytes(byte[] bytes)
        {
            MemoryStream stream = new MemoryStream(bytes);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();
            return image;
        }
    }
}
