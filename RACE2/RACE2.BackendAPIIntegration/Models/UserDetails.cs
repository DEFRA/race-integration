using RACE2.Dto;
using System.ComponentModel.DataAnnotations;

namespace RACE2.BackendAPIIntegration.Models
{
    public class UserDetails : IntegrateUserDTO
    {
      public List<IntegrateOrganisationdto> Organisation { get; set; } = new List<IntegrateOrganisationdto>();
      
        public List<IntegrateIndividualAddressDetails>  userAddresses { get; set; } = new List<IntegrateIndividualAddressDetails>();

        public List<IntegrateOrganisatioAddressdto> OrganisationAddress { get; set; } = new List<IntegrateOrganisatioAddressdto>();
        public List<IntegrateMembershipDetails> userMembership { get; set; } = new List<IntegrateMembershipDetails>();


      
      
    }
}
