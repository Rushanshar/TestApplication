
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DL.Entities
{
    [Table("subscriber")]
    public class Subscriber
    {
        [Key]
        [Required]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("subscriber_id")]
        public long SubscriberId{ get; set; }

        [Required]
        [Column("client_id")]
        public long ClientId { get; set; }
    }
}
