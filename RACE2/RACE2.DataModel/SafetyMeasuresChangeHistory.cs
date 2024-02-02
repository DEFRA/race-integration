﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("SafetyMeasuresChangeHistory")]
    public class SafetyMeasuresChangeHistory
    {

        [Key, Required]
        public int Id { get; set; }
        [Required]
        public int ReservoirId { get; set; }
        [ForeignKey("ReservoirId")]
        public virtual Reservoir Reservoir { get; set; }

        public int SourceSubmissionId { get; set; }

        [ForeignKey("SourceSubmissionId")]
        public virtual SubmissionStatus SourceSubmission { get; set; }

        public int MeasureId { get; set; }

        [ForeignKey("MeasureId")]

        public virtual SafetyMeasure SafetyMeasure { get; set; }

        [StringLength(64), Required]
        public string FieldName { get; set; }
        [StringLength(1024), Required]
        public string OldValue { get; set; }

        [StringLength(1024), Required]
        public string NewValue { get; set; }

        [Required]
        public DateTime ChangeDateTime { get; set; }

        [Required]
        public bool IsBackEndChange { get; set; }

        public int ChangeByUserId { get; set; }
        [ForeignKey("ChangeByUserId")]
        public virtual UserDetail UserDetail { get; set; }

    }
}
