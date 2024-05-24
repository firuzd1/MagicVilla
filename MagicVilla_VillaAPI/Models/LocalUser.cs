using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    [Table("local_user")]
    public class LocalUser
    {
        [Column("id")]
        public int id { get; set; }
        [Column("user_name")]
        public string UserName { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("role")]
        public string Role { get; set; }
    }
}
