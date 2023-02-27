using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("UserReservoirs")]
    public class UserReservoir
    {
        public int Id { get; set; }

        public UserDetail UserDetail { get; set; }

        public Reservoir Reservoir { get; set; }

        public string? appointment_type { get; set; }

        public DateTime appointment_start_date { get; set; }

        public DateTime appointment_end_date { get; set; }

      }
}
