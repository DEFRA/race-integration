using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class IntegrateMembershipDetails
    {
        public int frontEndPanelMembershipId {  get; set; }
        [MaxLength(16)]
        public string backEndPanelMembershipId { get; set; } = string.Empty;
        [MaxLength(64)]
        public string panelName {  get; set; } = string.Empty;
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        [MaxLength(64)]
        public string status { get; set; } = string.Empty ;
    }
}
