using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class BackendUserInfoDTO
    {
        public BackendUserDetailsDTO BackendUserDetailsDTO { get; set; } = new BackendUserDetailsDTO();
        public List<BackendOrganisationDetails> BackendOrganisationDetails { get; set; } = new List<BackendOrganisationDetails>();

        public List<BackendIndividualAddressDetails> BackendIndividualAddressDetails { get; set; } = new List<BackendIndividualAddressDetails>();

        public List<BackendOrganisationAddressDetails> BackendOrganisationAddressDetails { get; set; } = new List<BackendOrganisationAddressDetails>();

        public List<BackendMembershipDetails> BackendMembershipDetails { get; set; } = new List<BackendMembershipDetails>();
    }
}
