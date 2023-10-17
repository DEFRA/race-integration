using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("UserReservoir")]
    public class UserReservoir
    {
        public int Id { get; set; }
        [StringLength(64)]
        public string? RaceAppointmentId { get; set; }
        public UserDetail UserDetail { get; set; } = new UserDetail();

        public Reservoir Reservoir { get; set; } = new Reservoir();       

        public string? AppointmentType { get; set; }

        public DateTime AppointmentStartDate { get; set; }

        public DateTime AppointmentEndDate { get; set; }

     
    }
}
