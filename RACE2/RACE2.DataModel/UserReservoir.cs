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
        [StringLength(16)]
        public string? BackEndAppointmentId { get; set; }
        public UserDetail User { get; set; } = new UserDetail();

        public Reservoir Reservoir { get; set; } = new Reservoir();       

        public string? AppointmentType { get; set; }

        public DateTime? AppointmentStartDate { get; set; }

        public DateTime? AppointmentEndDate { get; set; }

     
    }
}
