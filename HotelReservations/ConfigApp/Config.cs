using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.ConfigApp
{
    public static class Config
    {
        public static string CONNECTION_STRING { get; } = "Data Source=.;Initial Catalog=HotelReservations;Integrated Security=True;";
    }
}
