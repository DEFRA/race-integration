using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("OrganisationReservoirs")]
    public class OrganisationReservoir
    {
        public int Id { get; set; }
        public Organisation Organisation { get; set; } = new Organisation();

        public Reservoir Reservoir { get; set; } = new Reservoir();

        public UserDetail? PrimaryContact { get; set; }

       public UserDetail? SecondaryContact { get;set; } 

        }
}
