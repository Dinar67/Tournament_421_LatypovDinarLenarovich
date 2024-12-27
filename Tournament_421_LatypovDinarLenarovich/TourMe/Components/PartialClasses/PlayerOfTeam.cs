using System.Collections.Generic;
using System.Linq;

namespace TourMe.Components
{
    public partial class PlayerOfTeam
    {
        public List<TeamRole> Roles
        {
            get { return App.db.TeamRole.ToList(); }
        }
    }
}
