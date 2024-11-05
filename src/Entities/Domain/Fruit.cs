using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;

namespace Entities.Domain
{
    public class Fruit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("FruitTypeMessages")]
        [Required]
        public long FruitTypeId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        public FruitType? FruitType { get; set; }

        public Fruit()
        {
            
        }

        public Fruit(string name)
        {
            Name = name;
        }

        public Fruit(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Fruit(string name, string description, long fruitTypeId)
        {
            Name = name;
            Description = description;
            FruitTypeId = fruitTypeId;
        }
    }
}
