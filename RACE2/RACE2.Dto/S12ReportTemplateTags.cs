﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.Dto
{
    public class S12ReportTemplateTags
    {
        public List<String> Tags { get; set; }
        public S12ReportTemplateTags() {
            Tags.Add("Reservoir Name");
            Tags.Add("Supervising Engineer Name");

        }
    }
}
