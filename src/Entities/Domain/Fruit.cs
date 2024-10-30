using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Domain
{
    public class Fruit(string name)
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("FruitType")]
        [Required]
        public required long FruitTypeId { get; set; }

        [Required]
        public string Name { get; set; } = name;

        [Required]
        public required string Description { get; set; }

        public FruitType? FruitType { get; set; }
    }
}
