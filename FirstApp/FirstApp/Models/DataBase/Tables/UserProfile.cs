using FirstApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_App.Models
{
    [Table("UserProfiles")]
    public class UserProfile
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }
        public int? Age { get; set; }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
