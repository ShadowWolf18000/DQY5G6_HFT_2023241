using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DQY5G6_HFT_2023241.Models
{
    public class Launcher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LauncherID { get; set; }

        [Required]
        [StringLength(100)]
        public string LauncherName { get; set; }
        
        [StringLength(240)]
        public string Owner { get; set; }

        public Launcher(string line)
        {
            var x = line.Split('#');
            LauncherID = Convert.ToInt32(x[0]);
            LauncherName = x[1];
            Owner = x[2];
        }
        public Launcher()
        {
            
        }


    }
}
