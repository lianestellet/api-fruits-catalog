using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Domain
{
    public class Fruit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set;  }

        [ForeignKey("FruitType")]
        [Required]
        
        public long FruitTypeId { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]
        
        public string Description { get; set; }

        [NotMapped]
        public FruitType FruitType { get; set; }

        public Fruit() { }

        public Fruit(long fruitTypeId, string name, string description)
        {            
            FruitTypeId = fruitTypeId;
            Name = name;
            Description = description;
        }
    }
}
