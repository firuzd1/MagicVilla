using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Web.Models.DTO
{
    public class VillaNumberCreateDTO
    {
        public int VillaNo { get; set; }
        public int VillaID { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
