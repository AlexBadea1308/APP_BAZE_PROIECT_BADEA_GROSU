using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    [Serializable]
    public class RoomType
    {
        public int Id { get; set; }

        private string name = string.Empty;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("It's required");
                }

                name = value;
            }
        }
        public override string ToString()
        {
            return Name;
        }

        public RoomType Clone()
        {
            var clone = new RoomType();
            clone.Id = Id;
            clone.Name = Name;
            return clone;
        }
    }
}
