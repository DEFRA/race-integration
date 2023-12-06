using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class OrganisationDTO
    {
        public int Id { get; set; }
        public string? OrgName { get; set; }        
       
        public string? cRaceOganisationId { get; set; }

        public string? cBusinessType { get; set; }

        [StringLength(64)]
        public string? cBranch { get; set; }
         public List<Address> Addresses { get; set; } = new List<Address>();
    }
}
