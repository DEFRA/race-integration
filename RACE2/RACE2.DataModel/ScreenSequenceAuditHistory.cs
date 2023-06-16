﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("ScreenSequenceAuditHistories")]
    public class ScreenSequenceAuditHistory
    {
        [Key, Required]
        public int Id { get; set; }
        [Required]
        public FeatureFunction Service { get; set; }
        [Required]
        public string? ChangeEvent { get; set; }
        [Required]
        public int OldValue { get; set; }
        [Required]
        public int NewValue { get; set; }
        [Required]
        public DateTime ModifiedOn { get; set; }
        [Required]
        public UserDetail ModifiedBy { get; set; } = new UserDetail();
    }
}
