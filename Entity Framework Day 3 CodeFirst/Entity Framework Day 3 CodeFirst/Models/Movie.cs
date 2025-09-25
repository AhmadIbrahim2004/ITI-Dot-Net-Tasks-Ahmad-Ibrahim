using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Entity_Framework_Day_3_CodeFirst.Models
{
    public class Movie
    {
        [Key] 
        public int MovieId { get; set; }

        [Required] 
        [StringLength(150)] 
        public string Title { get; set; }

        public short? DurationMin { get; set; } 

        [StringLength(1)]
        public string IsActive { get; set; } = "Y"; 
    }
}
