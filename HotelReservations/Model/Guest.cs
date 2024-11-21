using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservations.Model
{
    public class Guest
    {
        private string name = string.Empty;
        private string surname = string.Empty;
        private string jmbg = string.Empty;
        public bool IsActive { get; set; } = true;
        public int Id { get; set; }
        public int ReservationId { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Name is required");
                }

                name = value;
            }
        }
        public string Surname
        {
            get { return surname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Surname is required");
                }
                surname = value;
            }
        }
        public string JMBG
        {
            get { return jmbg; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("JMBG is required");
                }
                jmbg = value;
            }
        }

        public Guest Clone()
        {
            var clone = new Guest();
            clone.Id = Id;
            clone.Name = Name;
            clone.Surname = Surname;
            clone.JMBG = JMBG;
            return clone;
        }
    }
}
