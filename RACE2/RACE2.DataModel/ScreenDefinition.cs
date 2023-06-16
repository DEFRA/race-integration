using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RACE2.DataModel
{
    [Table("ScreenDefinition")]
    public class ScreenDefinition
    {
        [Key, Required]
        public int Id { get; set; }
        [StringLength(64),Required]
        public string? ScreenName { get; set; }
        [StringLength(64)]
        public string? Title { get; set; }
        [Required]
        public bool? HasSignificantChange { get; set; }
        [Required]
        public DateTime Modified { get; set; }
        [Required]
        public UserDetail ModifiedBy { get; set;} = new UserDetail();


    }
}
