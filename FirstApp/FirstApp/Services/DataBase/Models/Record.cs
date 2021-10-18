using FirstApp.Models;
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
    ///     Class of Record model.
    /// </summary>
    [Table("Records")]
    public class Record
    {
        [Column("Id")]  // Можно было не указывать потому, что так было бы по умолчанию, благодаря соглашению о наименованиях EF
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public short Score { get; set; }

        [Required]
        public string Date { get; set; }

        private string _timeAgoMessage;
        [NotMapped]
        public string TimeAgoMessage
        {
            get
            {
                TimeSpan diff = DateTime.Now - Convert.ToDateTime(Date);

                if (diff.Days > 0)
                {
                    _timeAgoMessage = diff.Days.ToString() + " days ago";
                }
                else if (diff.Hours > 0)
                {
                    _timeAgoMessage = diff.Hours.ToString() + " hours ago";
                }
                else if (diff.Minutes > 0)
                {
                    _timeAgoMessage = diff.Minutes.ToString() + " minutes ago";
                }
                else
                {
                    _timeAgoMessage = diff.Seconds.ToString() + " seconds ago";
                }
                return _timeAgoMessage;
            }
            set => _timeAgoMessage = value;
        }

        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
