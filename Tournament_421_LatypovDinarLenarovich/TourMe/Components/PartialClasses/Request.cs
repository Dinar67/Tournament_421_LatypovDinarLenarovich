using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourMe.Components
{
    public partial class Request
    {
        public string GetStatus
        {
            get
            {
                if (isAccepted == null)
                    return "На рассмотрении";
                else if (isAccepted == true)
                    return "Принята";
                else
                    return "Отказано";
            }
        }
    }
}
