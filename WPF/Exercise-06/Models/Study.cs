using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exercise_07.Models
{
    [Table("apbd.Studies")]
    public partial class Study
    {
        [Key]
        public int IdStudies { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
