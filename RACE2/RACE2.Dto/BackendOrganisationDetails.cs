using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class BackendOrganisationDetails
    {
        public int frontEndOrganisationId { get; set; }
        [MaxLength(16)]
        public string backEndOrganisationId { get; set; } = string.Empty;
        [MaxLength(12)]
        public string backEndOrganisationRef { get; set; } = string.Empty;
        [MaxLength(70)]
        public string name { get; set; } = string.Empty;
        [MaxLength(100)]
        public string tradingName { get; set; } = string.Empty;
        [MaxLength(64)]
        public string type { get; set; } = string.Empty;
        [MaxLength(64)]
        public string companyNumber { get; set; } = string.Empty;
    }
}
