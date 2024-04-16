using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class BackendIndividualAddressDetails
    {

        public int frontEndAddressId { get; set; }
        [MaxLength(64)]
        public string addressLine1 { get; set; } = string.Empty;
        [MaxLength(64)]
        public string addressLine2 { get; set; } = string.Empty;
        [MaxLength(64)]
        public string town { get; set; } = string.Empty;
        [MaxLength(64)]
        public string county { get; set; } = string.Empty;
        [MaxLength(64)]
        public string postcode { get; set; } = string.Empty;

        public bool isPrimaryAddress { get; set; }
    }
}
