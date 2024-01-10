using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class ReservoirUpdateDetailsDTO
    {
        public int Id { get; set; }       
        public int UserId { get; set; }
        public string? PublicName { get; set; }
        public string? GridReference { get; set; }

        public string? NearestTown { get; set; }

    }
}
