using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Models
{
    [Table("Users")]
    public class User
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Enter a username")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Incorrect length")]
        public string Username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter a password")]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Incorrect length")]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Password Confirmation")]
        //[Required(ErrorMessage = "Incorrect password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords are not equal")]
        public string PasswordConfirmation { get; set; }
    }
}
