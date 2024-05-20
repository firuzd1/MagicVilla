using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_VillaAPI.Models
{
    [Table("villa_number")]
    public class VillaNumber
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("villa_No")]
        public int VillaNo { get; set; }

        [ForeignKey("Villa")]
        [Column("villa_id")]
        public int VillaID { get; set; }
        
        [Column("special_detail")]
        public string SpecialDetails { get; set; }
       
        [Column("update_date_time")]
        public DateTime UpdatedDate { get; set; }
        
        [Column("created_date_time")]
        public DateTime CreatedDate { get; set; }

        public Villa Villa { get; set; }
    }
}
