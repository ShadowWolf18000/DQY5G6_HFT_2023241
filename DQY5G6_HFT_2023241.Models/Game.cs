using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DQY5G6_HFT_2023241.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GameID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [ForeignKey("DeveloperID")]
        public int DeveloperID { get; set; }

        [ForeignKey("LauncherID")]
        public int LauncherID { get; set; }

        [Range(0, 10)]
        public double Rating { get; set; }
        
        [JsonIgnore]
        public virtual Developer Developer { get; set; }
        
        [JsonIgnore]
        public virtual Launcher Launcher { get; set; }

        public Game(string line)
        {
            var x = line.Split('#');
            GameID = Convert.ToInt32(x[0]);
            Title = x[1];
            DeveloperID = Convert.ToInt32(x[2]);
            LauncherID = Convert.ToInt32(x[3]);
            var y = x[4].Split('.');
            Rating = Convert.ToInt32(y[0]) + Convert.ToDouble($"0,{y[1]}");
        }
        public Game()
        {
            
        }
        public Game(int gameID, string title, int developerID, int launcherID, double rating, Developer developer, Launcher launcher)
        {
            GameID = gameID;
            Title = title;
            DeveloperID = developerID;
            LauncherID = launcherID;
            Rating = rating;
            Developer = developer;
            Launcher = launcher;
        }
    }
}
