
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DL.Entities
{
    [Table("client")]
    public class Client
    {
        [Key]
        [Required]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }
    }
}
