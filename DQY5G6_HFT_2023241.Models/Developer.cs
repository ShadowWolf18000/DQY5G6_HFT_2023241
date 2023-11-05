using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Models
{
    public class Developer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DeveloperID { get; set; }

        [Required]
        [StringLength(100)]
        public string DeveloperName { get; set; }

        [Range(1965, 2023)]
        public int FoundingYear { get; set; }

        public virtual ICollection<Game> Games { get; set; } 

        public Developer(string line)
        {
            var x = line.Split('#');
            DeveloperID = Convert.ToInt32(x[0]);
            DeveloperName = x[1];
            FoundingYear = Convert.ToInt32(x[2]);
        }
        public Developer()
        {
            
        }

    }
}
