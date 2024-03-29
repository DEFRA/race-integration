﻿using RACE2.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class OperatorDTO
    {
        
    //    public Address Address { get; set; } = new Address();

       public string? OperatorFirstName { get; set; }

        public string? OperatorLastName { get; set; }

        public string? OrgName { get; set; }
        public string? Email { get; set; }

        
      // public string? mobile { get; set; }

        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? Town { get; set; }
        public string? County { get; set; }

        public string? Postcode { get; set; }

        public string? cMobile { get; set; }

     
        public string? cAlternativeMobile { get; set; }

        public string? cEmergencyPhone { get; set; }


        [StringLength(64)]
        public string? cAlternativePhone { get; set; }

        [StringLength(64)]
        public string? cAlternativeEmergencyPhone { get; set; }


    }
}
