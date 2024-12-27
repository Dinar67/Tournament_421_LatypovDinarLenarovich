using System.Windows;

namespace TourMe.Components
{
    public partial class UserLogin
    {
        public Visibility GetAdministratorVisibility
        {
            get
            {
                if(Moderator != null && Moderator.RoleId == 1)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public Visibility GetModeratorVisibility
        {
            get
            {
                if (Moderator != null && Moderator.RoleId == 2)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
        public Visibility GetPlayerVisibility
        {
            get
            {
                if (Player != null)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }
    }
}
