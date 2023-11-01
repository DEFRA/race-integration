using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("Comments")]
    public class Comment
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64)]
        public string? RaceCommentId { get; set; }
        [StringLength(64)]
        [Required]
        public string? RelatesToObject { get; set; }
       public int RelatesToRecord { get; set; }
        [Required]
        public UserDetail CreatedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }

        [StringLength(1024)]
        [Required]
        public string CommentText { get; set; }

        public Comment? ParentCommentid { get; set; }

        [StringLength(64)]
        [Required]
        public string Status { get; set; }

        public UserDetail? ClosedBy { get; set; }

        public DateTime ClosedOn { get; set; }
    }
}
