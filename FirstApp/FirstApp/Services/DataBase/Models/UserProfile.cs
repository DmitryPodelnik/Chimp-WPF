﻿using FirstApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models.DataBase.Models
{
    /// <summary>
    ///     Class of UserProfile model.
    /// </summary>
    [Table("UserProfiles")]
    public class UserProfile
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public double AverageScore { get; set; }

        [Required]
        public short MaxScore { get; set; }

        [Required]
        public int GameCount { get; set; }

        [Required]
        public double Rate { get; set; }

        [Required]
        public DateTime LastSeen { get; set; }

        [Required]
        public string RegisterDate { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
